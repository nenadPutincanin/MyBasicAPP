using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SalesApp.Models
{
    public partial class Product
    {
        public Product()
        {
            Lifletis = new HashSet<Lifleti>();
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int ProductId { get; set; }
        [DisplayName("Naziv proizvoda")]
        [Required(ErrorMessage = "Naziv proizvoda je obavezan")]
        public string ProductName { get; set; } = null!;
        [DisplayName("Težina proizvoda")]
        [Required(ErrorMessage = "Težina proizvoda je obavezana")]
        public int ProductWeight { get; set; }
        [DisplayName("Region")]
        public string? RegionName { get; set; }
        [DisplayName("Cena proizvoda")]
        [Required(ErrorMessage = "Cena proizvoda je obavezana")]
        public decimal? ProductPrice { get; set; }

        public virtual ICollection<Lifleti> Lifletis { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
