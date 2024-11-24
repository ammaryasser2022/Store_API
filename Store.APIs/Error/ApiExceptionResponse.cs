namespace Store.APIs.Error
{
    public class ApiExceptionResponse :ApiErrorResponse
    {
        public string? Details { get; set; }

        public ApiExceptionResponse(int statusCode , string? message = null , string? details = null) :/*base(500)*/ base(statusCode , message)
        {
            Details = details;
        }
    }
}
