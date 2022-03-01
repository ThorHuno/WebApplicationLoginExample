using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplicationLoginExample.Services;
using WebApplicationLoginExample.ViewModels;

namespace WebApplicationLoginExample.Areas.Menu.Controllers
{
    [Area("Menu")]
    [Authorize(Roles = "Admin")]
    public class MenuManagementController : Controller
    {
        private readonly MenuService _menuService;
        private readonly AuthService _authService;

        public MenuManagementController(MenuService menuService, AuthService authService)
        {
            _menuService = menuService;
            _authService = authService;
        }

        [Route("{area}/menu/{create}")]
        public IActionResult Create()
        {
            ViewBag.Parents = _menuService.ListParentsMenuItems().ToList();
            ViewBag.Roles = _authService.ListRoles();

            return View();
        }

        [HttpPost("{area}/menu/{create}")]
        public IActionResult Create(MenuItemInput menu)
        {
            try
            {
                ViewBag.Parents = _menuService.ListParentsMenuItems().ToList();
                ViewBag.Roles = _authService.ListRoles();

                if (!ModelState.IsValid)
                    return View(menu);

                _menuService.AddMenuItem(menu.Name, menu.Url, menu.RolesSelected, menu.ParentId);

                return Redirect("/home/index");
            }
            catch (Exception ex)
            {
                return View(menu);
            }
        }
    }
}
