namespace WebApplicationLoginExample.ViewModels
{
    public class MenuItemInput
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public int? ParentId { get; set; }
        public string[] RolesSelected { get; set; }
    }
}
