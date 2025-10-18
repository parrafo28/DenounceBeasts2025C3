namespace DenounceBeasts.API.Models.Responses
{
    public class ApiResponse<T>
    {
        public bool Sucess { get; set; }
        public T Data { get; set; }
        public string Message { get; set; }
        public string StatusCode { get; set; }

        public static ApiResponse<T> Success(T data, string code = "", string message = "")
        {
            return new ApiResponse<T> { Sucess = true, Data = data, StatusCode = code, Message = message };
        }
        public static ApiResponse<T> Fail(string code = "", string message = "")
        {
            return new ApiResponse<T> { Sucess = false, StatusCode = code, Message = message };
        }

    }
}
