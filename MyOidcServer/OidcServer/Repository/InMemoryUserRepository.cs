using OidcServer.Models;

namespace OidcServer.Repository
{
    public class InMemoryUserRepository : IUserRepository
    {
        private readonly List<User> _users = new List<User>
        {
            new User { Username = "alice" },
            new User { Username = "bob" }
        };
        public User? FindByUsername(string username)
        {
            return _users.FirstOrDefault(x => x.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
        }
    }
}
