namespace ClientSide.Helpers
{
    public class ApiResponse<T>
    {
        public bool IsSuccess { get; set; }
        public T? Data { get; set; }
        public ErrorCode? ErrorCode { get; set; }
    }

    public enum ErrorCode
    {
        Unknown = 0
    }
}
