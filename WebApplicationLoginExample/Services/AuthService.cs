using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationLoginExample.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace WebApplicationLoginExample.Services
{
    public class AuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signinManager;

        public AuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signinManager, RoleManager<ApplicationRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signinManager = signinManager;
        }

        public async Task<SignInResult> LogIn(string userName, string password)
        {
            try
            {
                // find user by username
                var user = await _userManager.FindByNameAsync(userName);
                if (user == null)
                    throw new Exception("Cuenta inválida");

                // checks if the user account is blocked for an indefinite period
                if (!user.LockoutEnabled)
                {
                    throw new Exception("La cuenta esta deshabilitada");
                }

                // Check user credentials
                var result = await _signinManager.PasswordSignInAsync(user, password, true, false);

                //if (!await _userManager.CheckPasswordAsync(user, password))
                //    throw new Exception("Usuario o Contraseña invalida.");

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IdentityResult> Register(string userName, string password, IEnumerable<string> roles)
        {
            try
            {
                // find user by username
                var user = await _userManager.FindByNameAsync(userName);
                if (user != null)
                    throw new Exception("Ya existe una cuenta con este nombre de usario");

                user = new ApplicationUser
                {
                    CreatedAt = DateTime.UtcNow,
                    UserName = userName,
                    NormalizedUserName = userName.ToUpper()
                };

                var result = await _userManager.CreateAsync(user, password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRolesAsync(user, roles.Select(r => r.ToUpper()));
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<ApplicationUserViewModel> ListUsers()
        {
            return _userManager.Users.Select(u => new ApplicationUserViewModel
            {
                CreatedAt = u.CreatedAt,
                Id = u.Id,
                IsEnabled = u.LockoutEnabled,
                UserName = u.UserName
            }).ToList();
        }

        public IEnumerable<RolesViewModel> ListRoles()
        {
            return _roleManager.Roles.Select(r => new RolesViewModel
            {
                Name = r.Name,
                Id = r.Id
            }).ToList();
        }

        public async Task<ApplicationUser> GetUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
                throw new Exception($"No existe usuario con id {id}");

            return user;
        }

        public async Task<IdentityResult> UpdateUser(string id, UserInputViewModel userInput)
        {
            try
            {
                // find user by id
                var user = await _userManager.FindByIdAsync(id);
                if (user == null)
                    throw new Exception($"No existe usuario con id {id}");

                user.UserName = userInput.UserName;
                user.NormalizedUserName = userInput.UserName.ToUpper();
                user.LockoutEnabled = userInput.IsEnabled;

                var result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    foreach (var role in userInput.RolesSelected)
                    {
                        if (!await _userManager.IsInRoleAsync(user, role.ToUpper()))
                        {
                            await _userManager.AddToRolesAsync(user, new string[] { role.ToUpper() });
                        }
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IdentityResult> RemoveUser(string id)
        {
            try
            {
                // find user by id
                var user = await _userManager.FindByIdAsync(id);
                if (user == null)
                    throw new Exception($"No existe usuario con id {id}");

                var result = await _userManager.DeleteAsync(user);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<RolesViewModel> GetRolesByNames(IEnumerable<string> roleNames)
        {
            return _roleManager.Roles.Where(r => roleNames.Contains(r.Name)).Select(r => new RolesViewModel
            {
                Name = r.Name,
                Id = r.Id
            }).ToList();
        }

        public async Task Logout()
        {
            try
            {
                await _signinManager.SignOutAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<string>> GetUserRoles(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                throw new Exception($"No existe usuario con id {userId}");

            return await _userManager.GetRolesAsync(user);
        }
    }
}
