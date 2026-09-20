namespace API.Errors
{
    public class ApiErrorResponse(
        int statusCode,
        string errorMessage,
        string? details)
    {
        public int StatusCode { get; set; } = statusCode;
        public string ErrorMessage { get; set; } = errorMessage;
        public string? Details { get; set; } = details;
    }
}
