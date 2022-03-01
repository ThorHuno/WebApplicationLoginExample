using DataAccessLayer;
using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationLoginExample.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace WebApplicationLoginExample.Services
{
    public class MenuService
    {
        private readonly ApplicationDbContext _dbContext;

        public MenuService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Menu> ListParentsMenuItems()
        {
            var menu = _dbContext.Set<Menu>();

            var parents = menu.Where(m => m.SubMenuId == null);

            return parents;
        }

        public IEnumerable<MenuTreeViewModel> ListMenuTree(IEnumerable<string> roleIds, bool isAdmin)
        {
            var menu = _dbContext.Set<Menu>();

            var parents = menu.Include(m => m.Submenus).Include(m => m.MenuRoles).Where(m => m.SubMenuId == null);

            if (!isAdmin)
                parents = parents.Where(m => m.MenuRoles.Any(mr => roleIds.Contains(mr.RoleId)));

            return parents.Select(m => new MenuTreeViewModel
            {
                Name = m.Name,
                URL = m.Url,
                IsParent = m.Submenus.Any(),
                SubItems = m.Submenus.Select(sm => new MenuTreeViewModel
                {
                    Name = sm.Name,
                    URL = sm.Url
                })
            });
        }

        public void AddMenuItem(string name, string url, IEnumerable<string> roles, int? parentid = default)
        {
            var menu = _dbContext.Set<Menu>();
            var menuroles = _dbContext.Set<MenuRoles>();

            var newMenu = new Menu
            {
                Name = name,
                Url = url,
                SubMenuId = parentid
            };

            menu.Add(newMenu);

            menuroles.AddRange(roles.Select(r => new MenuRoles
            {
                Menu = newMenu,
                RoleId = r
            }));

            _dbContext.SaveChanges();
        }
    }
}
