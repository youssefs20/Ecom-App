using Ecom.Core.interfaces;
using Ecom.infrastructure.Data;
using Ecom.infrastructure.Repositries;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.infrastructure
{
    public static class infrastructureResgisteration
    {
        public static IServiceCollection infrastructureConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddTransient();
            //services.AddScoped();
            //services.AddSingletone();
            services.AddScoped(typeof(IGenericRepositry<>), typeof(GenericRepositry<>));
            //apply unit of work
            services.AddScoped<IUnitofWork, UnitofWork>();
            //apply Dbcontext
            services.AddDbContext<AppDbContext>(op =>
            {
                op.UseSqlServer(configuration.GetConnectionString("EcomDatabase"));
            });
            return services;
        }
    }
}
