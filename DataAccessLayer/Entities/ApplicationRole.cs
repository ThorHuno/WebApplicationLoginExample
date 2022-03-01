using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace DataAccessLayer.Entities
{
    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole()
        {
            MenuRoles = new HashSet<MenuRoles>();
        }

        public DateTime CreatedAt { get; set; }
        public virtual ICollection<MenuRoles> MenuRoles { get; set; }
    }
}
