using System.ComponentModel.DataAnnotations;

namespace DenounceBeasts.Application.Requests
{
    public class RegisterRequest
    {
        [Required, EmailAddress] public string Email { get; set; } = default!;
        [Required, MinLength(6)] public string Password { get; set; } = default!;
        [Required, MinLength(2)] public string Name { get; set; } = default!;
        [Required, MinLength(2)] public string LastName { get; set; } = default!;
    }
}
