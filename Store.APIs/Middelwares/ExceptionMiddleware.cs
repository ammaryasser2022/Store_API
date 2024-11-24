using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Store.APIs.Error;

namespace Store.APIs.Middelwares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;

        //delegate/ fun bt7rk to next request , log any exception catched ,   variable by7dd el enveroment 
        public ExceptionMiddleware(RequestDelegate next , ILogger<ExceptionMiddleware> logger , IHostEnvironment env) 
        {
            _next = next;
            _logger = logger;
            _env = env;
        }


        // to make a class Middleware -->MUST HAS FUN INVOKE

        public async Task InvokeAsync(HttpContext context) //context -> variable to be Request and Response 
        {
            // Ineed to know when i Invoke Request and Response need to know i any exception done ? ( by try catch )
            try
            {
                await _next.Invoke(context); //mean wna b3ml invoke ll Request and Response lw 7sl try exception

            }
            catch (Exception ex)
            {
                // before catch it log ot 
                _logger.LogError(ex, ex.Message); //log and print ex and el exMessage

                context.Response.ContentType = "application/json"; //i knew  the content type from postman
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                                                                                
                var response = _env.IsDevelopment() ?                                //stack-> first line mn error kda     
                    new ApiExceptionResponse(StatusCodes.Status500InternalServerError, ex.Message , ex?.StackTrace?.ToString() )
                    : new ApiExceptionResponse(StatusCodes.Status500InternalServerError);

                var options = new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };


                var jsonResponse = JsonSerializer.Serialize(response , options);
                context.Response.WriteAsync(jsonResponse); // htrgy respone ll requset elly mskto --WriteAsync -> fun has the body of response
                                                // this bode i need contain Status code and message and Details So i need new type

                // note:  write --> btrg3 data jsonstring not string

            }
        }

    }
}
