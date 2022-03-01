using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApplicationLoginExample.Services;

namespace WebApplicationLoginExample.ViewComponents
{
    [ViewComponent(Name = "MenuTreeComponent")]
    public class MenuTreeComponent : ViewComponent
    {
        private readonly MenuService _menuService;
        private readonly AuthService _authService;

        public MenuTreeComponent(MenuService menuService, AuthService authService)
        {
            _menuService = menuService;
            _authService = authService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var roleNames = ((ClaimsIdentity)User.Identity).Claims
                .Where(c => c.Type == ClaimTypes.Role)
                .Select(c => c.Value);

            var isAdmin = roleNames.Contains("Admin");

            if (roleNames.Count() == 0)
                roleNames = roleNames.Concat(new string[] { "Public" });

            var roles = _authService.GetRolesByNames(roleNames);

            var menuTree = _menuService.ListMenuTree(roles.Select(r => r.Id), isAdmin);

            return View("_MenuTree", menuTree);
        }
    }
}
