namespace DenounceBeasts.API.Models.Response
{
    public class ApiResponse<T> 
    {
        public bool IsSuccess { get; set; }
        public T Data { get; set; }
        public string Message { get; set; }
        public int StatusCode { get; set; }

        public static ApiResponse<T> Success(T data, int code = 200, string message = "")
        {
            return new ApiResponse<T> { IsSuccess = true, Data = data, StatusCode = code, Message = message };
        }
        public static ApiResponse<T> Fail(int code = 400, string message = "")
        {
            return new ApiResponse<T> { IsSuccess = false, StatusCode = code, Message = message };
        }

    }
}
