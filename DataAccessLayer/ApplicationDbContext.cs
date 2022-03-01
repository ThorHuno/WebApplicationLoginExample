using DataAccessLayer.Entities;
using DataAccessLayer.EntitiesConfiguration;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace DataAccessLayer
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Menu> Menus { get; set; }
        public DbSet<MenuRoles> MenuRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ApplicationUserConfiguration());
            builder.ApplyConfiguration(new ApplicationRoleConfiguration());
            builder.ApplyConfiguration(new MenuConfiguration());
            builder.ApplyConfiguration(new MenuRolesConfiguration());

            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>().ToTable("Users");
            builder.Entity<IdentityRole>().ToTable("Roles");
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");
            builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");
            builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins");

            const string ADMIN_ROLE_ID = "c67e0991-32ec-42c8-8369-b11e8376bf43";
            const string User_ROLE_ID = "c67e0991-32ec-42c8-8369-b11e8376bf44";
            const string Public_ROLE_ID = "c67e0991-32ec-42c8-8369-b11e8376bf45";

            builder.Entity<ApplicationRole>().HasData(new ApplicationRole
            {
                Id = ADMIN_ROLE_ID,
                Name = "Admin",
                NormalizedName = "ADMIN",
                CreatedAt = DateTime.UtcNow
            }, new ApplicationRole
            {
                Id = User_ROLE_ID,
                Name = "User",
                NormalizedName = "USER",
                CreatedAt = DateTime.UtcNow
            }, new ApplicationRole
            {
                Id = Public_ROLE_ID,
                Name = "Public",
                NormalizedName = "PUBLIC",
                CreatedAt = DateTime.UtcNow
            });

            const string ADMIN_USER_ID = "7545aa39-44c6-4792-92c0-c2695fe37507";

            var hasher = new PasswordHasher<ApplicationUser>();

            builder.Entity<ApplicationUser>().HasData(new ApplicationUser
            {
                Id = ADMIN_USER_ID,
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                EmailConfirmed = false,
                PasswordHash = hasher.HashPassword(null, "test123!"),
                SecurityStamp = string.Empty,
                CreatedAt = DateTime.UtcNow
            });

            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = ADMIN_ROLE_ID,
                UserId = ADMIN_USER_ID
            });

            builder.Entity<Menu>().HasData(new Menu
            {
                Id = 1,
                Name = "User Management",
                Url = "#"
            },
            new Menu
            {
                Id = 2,
                Name = "Users",
                Url = "/Auth/UserManagement/Index",
                SubMenuId = 1
            },
            new Menu
            {
                Id = 3,
                Name = "Create user",
                Url = "/Auth/UserManagement/Register",
                SubMenuId = 1
            },
            new Menu
            {
                Id = 4,
                Name = "Menu Management",
                Url = "#"
            },
            new Menu
            {
                Id = 5,
                Name = "Create menu item",
                Url = "/Menu/Menu/Create",
                SubMenuId = 4
            });

            builder.Entity<MenuRoles>().HasData(new MenuRoles
            {
                MenuId = 1,
                RoleId = ADMIN_ROLE_ID
            },
            new MenuRoles
            {
                MenuId = 2,
                RoleId = ADMIN_ROLE_ID
            },
            new MenuRoles
            {
                MenuId = 3,
                RoleId = ADMIN_ROLE_ID
            },
            new MenuRoles
            {
                MenuId = 4,
                RoleId = ADMIN_ROLE_ID
            },
            new MenuRoles
            {
                MenuId = 5,
                RoleId = ADMIN_ROLE_ID
            });
        }
    }
}
