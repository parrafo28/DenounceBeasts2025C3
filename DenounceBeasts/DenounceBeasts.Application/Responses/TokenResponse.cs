namespace DenounceBeasts.Application.Responses
{
    public class TokenResponse
    {
        public string AccessToken { get; set; } = default!;
        public string TokenType { get; set; } = "Bearer";
        public int ExpiresIn { get; set; }
    }
}
