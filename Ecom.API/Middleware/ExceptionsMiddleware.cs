﻿using Ecom.API.Helper;
using Microsoft.Extensions.Caching.Memory;
using System.Net;
using System.Text.Json;

namespace Ecom.API.Middleware
{
    public class ExceptionsMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IHostEnvironment _environment;
        private readonly IMemoryCache _memoryCache;
        private readonly TimeSpan _rateLimitWindow = TimeSpan.FromSeconds(30);
        //29-30-31-32-33
        public ExceptionsMiddleware(RequestDelegate next, IHostEnvironment environment, IMemoryCache memoryCache)
        {
            _next = next;
            _environment = environment;
            _memoryCache = memoryCache;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                ApplySecurity(context);
                if (IsRequestAllowed(context) == false)
                {
                    context.Response.StatusCode = (int)HttpStatusCode.TooManyRequests;
                    context.Response.ContentType = "application/json";
                    var response = new 
                        ApiExceptions((int)HttpStatusCode.TooManyRequests, "Too many requests, please try again later");
                    await context.Response.WriteAsJsonAsync(response);
                }
                await _next(context);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode= (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                var response = _environment.IsDevelopment() ?
                    new ApiExceptions((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace)
                    : new ApiExceptions((int)HttpStatusCode.InternalServerError, ex.Message);

                var json = JsonSerializer.Serialize(response);
                await context.Response.WriteAsync(json);
            }
        }

        private bool IsRequestAllowed(HttpContext context)
        {
            var ip= context.Connection.RemoteIpAddress.ToString();
            //31
            var cachKey = $"RequestCount-{ip}";
            var dateNow = DateTime.Now;

            var (timesTamp,count) = _memoryCache.GetOrCreate(cachKey, entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = _rateLimitWindow;
                return (timesTamp:dateNow,count:0);
            });

            //timestamp is less than rate limit window then its already registered
            if (dateNow - timesTamp < _rateLimitWindow)
            {
                //high request
                if (count >= 8)
                {
                    return false;
                }
                _memoryCache.Set(cachKey, (timesTamp, count += 1), _rateLimitWindow);
            }
            //if not regietered then register
            else
            {
                _memoryCache.Set(cachKey, (timesTamp, count), _rateLimitWindow);
            }
            return true;
        }
        private void ApplySecurity(HttpContext context) 
        {
            //if someone sent js link in the header to dxdos attack
            context.Response.Headers["X-Content-Type-Options"] = "nosniff";
            //if someone sent a spam file contains malware to users
            context.Response.Headers["X-XSS-Protection"]= "1; mode=block";
            context.Response.Headers["X-Frame-Options"] = "DENY";
        }
    }
}
