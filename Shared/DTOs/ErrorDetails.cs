namespace Shared.DTOs
{
    public class ErrorDetails
    {
        public int? Status { get; set; }
        public Error Error { get; set; }
    }
    public class Error
    {
        public string Code { get; set; }
        public string Message { get; set; }
    }
}
