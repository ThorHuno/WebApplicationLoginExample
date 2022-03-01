using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplicationLoginExample.Services;
using WebApplicationLoginExample.ViewModels;

namespace WebApplicationLoginExample.Areas.Auth.Controllers
{
    [Area("Auth")]
    public class LoginController : Controller
    {
        private readonly AuthService _authService;

        public LoginController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpGet("{area}/{controller}/Login")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("{area}/{controller}/Login")]
        public async Task<IActionResult> Index([FromForm] LoginViewModel login, [FromQuery] string ReturnUrl = null)
        {
            if (!ModelState.IsValid)
                return View(login);

            try
            {
                var result = await _authService.LogIn(login.UserName, login.Password);

                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(ReturnUrl))
                        return Redirect(ReturnUrl);

                    return RedirectToAction("Index", "Home", new { area = "" });
                }

                return View();
            }
            catch (Exception ex)
            {
                return View(login);
            }
        }

        [HttpGet("{area}/{controller}/Logout")]
        public async Task<IActionResult> Logout()
        {
            try
            {
                await _authService.Logout();

                return RedirectToAction("Login", "Login");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
        }
    }
}
