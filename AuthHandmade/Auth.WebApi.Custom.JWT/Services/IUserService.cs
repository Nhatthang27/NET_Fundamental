using Auth.WebApi.Custom.JWT.Entities;

namespace Auth.WebApi.Custom.JWT.Services
{
    public interface IUserService
    {
        User? Authenticate(string username, string password);
        User Register(string username, string password);
    }
}
