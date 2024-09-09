using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace SalesApp.Models
{
    public partial class Region
    {
        public int RegionId { get; set; }
        [DisplayName("Naziv regiona")]
        public string RegionName { get; set; } = null!;
    }
}
