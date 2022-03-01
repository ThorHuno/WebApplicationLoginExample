using System;
using System.Collections.Generic;

namespace WebApplicationLoginExample.ViewModels
{
    public class ApplicationUserViewModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsEnabled { get; set; }
        public string Status => IsEnabled ? "Enabled" : "Disabled";
    }
}
