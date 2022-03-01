namespace DataAccessLayer.Entities
{
    public class MenuRoles
    {
        public int MenuId { get; set; }
        public string RoleId { get; set; }

        public virtual Menu Menu { get; set; }
        public virtual ApplicationRole Role { get; set; }
    }
}
