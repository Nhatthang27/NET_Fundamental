using Auth.WebApi.Custom.JWT.Entities;

namespace Auth.WebApi.Custom.JWT.Services
{
    public class UserService : IUserService
    {
        private readonly List<User> _users = new()
    {
        new User { Username = "admin", Password = "admin123", Role = "Admin" },
        new User { Username = "user", Password = "user123", Role = "User" }
    };

        public User? Authenticate(string username, string password)
        {
            return _users.FirstOrDefault(u => u.Username == username && u.Password == password);
        }

        public User Register(string username, string password)
        {
            var user = new User { Username = username, Password = password, Role = "User" };
            _users.Add(user);
            return user;
        }
    }
}
