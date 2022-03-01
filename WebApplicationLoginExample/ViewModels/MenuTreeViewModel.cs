using System.Collections.Generic;

namespace WebApplicationLoginExample.ViewModels
{
    public class MenuTreeViewModel
    {
        public string Name { get; set; }
        public string URL { get; set; }
        public bool IsParent { get; set; }
        public IEnumerable<MenuTreeViewModel> SubItems { get; set; }
    }
}
