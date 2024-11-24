namespace Store.APIs.Error
{
    public class ApiErrorResponse
    {
       


        public int StatusCode { get; set; }
        public string? Message { get; set; }

        //ctor
        public ApiErrorResponse(int statusCode, string? message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageForStatusCode(statusCode);
        }


        // any object from this class send the status code and message 
        // if the message == null 
        // show default message of StatusCode 

        private string? GetDefaultMessageForStatusCode(int statusCode)
        {
            var message = statusCode switch
            {
                400 => "A bad Request U have Made",
                401 => " Authorized U Are Not",
                404 => "Resource Not Found" ,
                500 => " Server Error",
                _ => null // Default
            };
                
            return message;

        }


        
    }
}
