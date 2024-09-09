using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace SalesApp.Models
{
    public partial class User
    {
        public int UserId { get; set; }
        [DisplayName("Username")]
        public string UserName { get; set; } = null!;
        [DisplayName("Password")]
        public string Password { get; set; } = null!;
    }
}
