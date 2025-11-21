using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenounceBeasts.Business.Responses
{
    public sealed class TokenResponse
    {
        public string AccessToken { get; init; } = default!;
        public string TokenType { get; init; } = "Bearer";
        public int ExpiresIn { get; init; }  
    }
}
