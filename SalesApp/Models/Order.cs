using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace SalesApp.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int OrderId { get; set; }
        public int ClientId { get; set; }
        [DisplayName("Region")]
        public string? RegionName { get; set; }
        [DisplayName("Datum porudžbine")]
        public DateTime? OrderDate { get; set; }

        

        public virtual Client Client { get; set; } = null!;
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
