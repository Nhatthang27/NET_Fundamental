using Microsoft.AspNetCore.Identity;

namespace Auth.WebApp.Identity.Cookie.Data.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; } // Thêm thuộc tính tùy chỉnh
    }
}
