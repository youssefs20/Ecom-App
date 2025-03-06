namespace Ecom.API.Helper
{
    public class ResponseAPI
    {
        public ResponseAPI(int statusCode, string? message=null)
        {
            StatusCode = statusCode;
            //if message is null then get message from status code
            Message = message ?? GetMessageFromStatusCode(StatusCode);
        }
        //16
        private string GetMessageFromStatusCode(int statusCode)
        {
            return statusCode switch
            {
                200 => "Success",
                201 => "Created",
                204 => "No Content",
                400 => "Bad Request",
                401 => "UnAuthorized",
                403 => "Forbidden",
                404 => "Not Found",
                500 => "Internal Server Error",
                _ => "Unknown Status Code"
            };
        }

        public int StatusCode { get; set; }
        //message null because we might get message from status code
        public string? Message { get; set; }
    }
}
