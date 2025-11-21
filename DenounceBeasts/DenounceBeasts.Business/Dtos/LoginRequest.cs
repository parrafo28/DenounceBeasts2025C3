using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenounceBeasts.Business.Dtos
{
    public sealed class LoginRequest
    {
        [Required, EmailAddress] public string Email { get; init; } = default!;
        [Required, MinLength(6)] public string Password { get; init; } = default!;
    }
}
