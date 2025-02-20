using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Auth.WebApp.Identity.Cookie.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<IActionResult> CreateRole(string roleName)
        {
            var roleExist = await _roleManager.RoleExistsAsync(roleName);
            if (!roleExist)
                await _roleManager.CreateAsync(new IdentityRole(roleName));

            return Ok($"{roleName} role created");
        }

    }
}
