using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace SalesApp.Models
{
    public partial class OrderDetail
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        [DisplayName("Količina")]
        public int? Quantity { get; set; }

        public virtual Order Order { get; set; } = null!;
        public virtual Product Product { get; set; } = null!;
    }
}
