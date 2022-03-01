using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApplicationLoginExample.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [DisplayName("User name")]
        public string UserName { get; set; }
        [Required]
        [DisplayName("Password")]
        public string Password { get; set; }
        [Required]
        public string[] RolesSelected { get; set; }
    }
}
