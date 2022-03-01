using System.Collections.Generic;

namespace DataAccessLayer.Entities
{
    public class Menu
    {
        public Menu()
        {
            MenuRoles = new HashSet<MenuRoles>();
            Submenus = new HashSet<Menu>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public int? SubMenuId { get; set; }

        public virtual Menu ParentMenu { get; set; }
        public virtual ICollection<MenuRoles> MenuRoles { get; set; }
        public virtual ICollection<Menu> Submenus { get; set; }
    }
}
