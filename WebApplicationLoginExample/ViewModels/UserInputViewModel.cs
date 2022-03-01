using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApplicationLoginExample.ViewModels
{
    public class UserInputViewModel
    {
        [DisplayName("User Name")]
        [Required]
        public string UserName { get; set; }
        [Required]
        public IEnumerable<string> RolesSelected { get; set; }
        [DisplayName("Status")]
        public bool IsEnabled { get; set; }
    }
}
