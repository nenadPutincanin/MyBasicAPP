using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SalesApp.Models
{
    public partial class Lifleti
    {
        public int LifletId { get; set; }
        [DisplayName("Dodatni rabat")]
        [Required(ErrorMessage = "Dodatni rabat je obavezan")]
        public int AdditionalDiscount { get; set; }
        [DisplayName("Početak lifleta")]
        [Required(ErrorMessage = "Datum početka lifleta je obavezan")]
        public DateTime DateFrom { get; set; }
        [DisplayName("Kraj lifleta")]
        [Required(ErrorMessage = "Datum kraja lifleta je obavezan")]
        public DateTime DateTo { get; set; }
        [DisplayName("Proizvod")]
        [Required(ErrorMessage = "Proizvod je obavezan")]
        public int? ProductId { get; set; }
        
        public int? ClientId { get; set; }

        public virtual Client? Client { get; set; }
        public virtual Product? Product { get; set; }
    }
}
