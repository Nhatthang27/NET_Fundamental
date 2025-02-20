using System.ComponentModel.DataAnnotations;

namespace Auth.WebApp.Custom.Cookie.DAL.Entities
{
    public class User
    {
        [Key]
        public Guid Guid { get; set; }
        public required string Username { get; set; }
        public required string Password { get; set; }
    }
}
