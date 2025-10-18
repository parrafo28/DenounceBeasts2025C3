namespace DenounceBeasts.API.Models.Response
{
    public sealed record ApiResponse<T>(bool success, T? Data, string? Message = null, string? ErrorCode = null)
    {
        public static ApiResponse<T> Success(T data, string? message = null) => new(true, data, message);
        public static ApiResponse<T> Fail(string message, string? code = null) => new(false, default, message, code);
    }
}
