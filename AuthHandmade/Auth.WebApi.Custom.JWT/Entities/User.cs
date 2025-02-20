namespace Auth.WebApi.Custom.JWT.Entities
{
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; } // Hash trong thực tế
        public string Role { get; set; } // "User" hoặc "Admin"
    }
}
