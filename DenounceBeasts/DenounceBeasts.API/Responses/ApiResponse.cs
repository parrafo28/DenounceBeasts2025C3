namespace DenounceBeasts.API.Models.Responses
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public T Data { get; set; }
        public string Message { get; set; }
        public int StatusCode { get; set; }

        public static ApiResponse<T> SuccessResponse(T data, int code = 200, string message = "")
        {
            return new ApiResponse<T> { Success = true, Data = data, StatusCode = code, Message = message };
        }
        public static ApiResponse<T> FailResponse(int code = 400, string message = "")
        {
            return new ApiResponse<T> { Success = false, StatusCode = code, Message = message };
        }

    }

}
