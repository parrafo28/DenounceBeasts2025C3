namespace DenounceBeasts.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; } = default!;
        public string PasswordHash { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string Lastname { get; set; } = default!;
        public bool IsActive { get; set; } = true;
        public string Role { get; set; } = "User"; // Admin/Moderator/User
        public string FullName => $"{Name} {Lastname}";
    }
}
