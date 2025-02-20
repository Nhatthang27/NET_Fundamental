using Auth.WebApp.Custom.Cookie.DAL;
using Auth.WebApp.Custom.Cookie.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Auth.WebApp.Custom.Cookie.BLL
{
    public class AccountService
    {
        private readonly CustomCookieDbContext _context;
        public AccountService(CustomCookieDbContext context)
        {
            _context = context;
        }

        public async Task<User?> Get(string username, string password)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Username == username && x.Password == password);
        }
    }
}
