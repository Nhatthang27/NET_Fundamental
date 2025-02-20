using System.ComponentModel.DataAnnotations;

namespace Auth.WebApp.Custom.Cookie.Models
{
    public class LoginViewModel
    {
        [Required]
        public string Username { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
