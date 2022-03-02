using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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

                TempData["Message"] = JsonConvert.SerializeObject(new MessageViewModel
                {
                    MessageType = 1,
                    Message = "Menu item created",
                    Tittle = "Success"
                });

                return Redirect("/home/index");
            }
            catch (Exception ex)
            {
                TempData["Message"] = JsonConvert.SerializeObject(new MessageViewModel
                {
                    MessageType = -1,
                    Message = ex.Message,
                    Tittle = "Error"
                });

                return View(menu);
            }
        }
    }
}
