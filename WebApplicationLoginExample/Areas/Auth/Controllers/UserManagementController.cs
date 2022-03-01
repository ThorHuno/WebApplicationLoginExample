using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplicationLoginExample.Services;
using WebApplicationLoginExample.ViewModels;

namespace WebApplicationLoginExample.Areas.Auth.Controllers
{
    [Area("Auth")]
    [Authorize(Roles = "Admin")]
    public class UserManagementController : Controller
    {
        private readonly AuthService _authService;

        public UserManagementController(AuthService authService)
        {
            _authService = authService;
        }

        public IActionResult Index()
        {
            var users = _authService.ListUsers();
            return View(users);
        }

        public IActionResult Register()
        {
            ViewBag.Roles = _authService.ListRoles();
            return View();
        }

        [HttpPost("{area}/{controller}/Signup")]
        public async Task<IActionResult> Index([FromForm] RegisterViewModel register)
        {
            ViewBag.Roles = _authService.ListRoles();

            if (!ModelState.IsValid)
                return View(register);

            try
            {
                var result = await _authService.Register(register.UserName, register.Password, register.RolesSelected);

                if (result.Succeeded)
                    return Redirect("/auth/UserManagement");

                return View(register);
            }
            catch (Exception ex)
            {
                return View(register);
            }
        }

        public async Task<IActionResult> Edit(string id)
        {
            ViewBag.Roles = _authService.ListRoles();

            try
            {
                var user = await _authService.GetUser(id);

                ViewBag.Id = id;

                var model = new UserInputViewModel
                {
                    IsEnabled = user.LockoutEnabled,
                    UserName = user.UserName,
                    RolesSelected = await _authService.GetUserRoles(id)
                };

                return View(model);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", new { area = "auth" });
            }
        }

        [HttpPost("{area}/{controller}/edit")]
        public async Task<IActionResult> Edit([FromForm] UserInputViewModel user, string id)
        {
            ViewBag.Id = id;
            ViewBag.Roles = _authService.ListRoles();

            try
            {
                var result = await _authService.UpdateUser(id, user);

                if (result.Succeeded)
                    return Redirect("/auth/UserManagement");

                return View(user);
            }
            catch (Exception ex)
            {
                return View(user);
            }
        }

        [HttpDelete("{area}/{controller}/delete")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                await _authService.RemoveUser(id);

                return Json(new
                {
                    message = "User removed",
                    status = true
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    message = ex.Message,
                    status = false
                });
            }
        }
    }
}
