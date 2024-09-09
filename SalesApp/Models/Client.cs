using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SalesApp.Models
{
    public partial class Client
    {
        public Client()
        {
            Lifletis = new HashSet<Lifleti>();
            Orders = new HashSet<Order>();
        }

        public int ClientId { get; set; }

        [DisplayName("Naziv kupca")]
        [Required(ErrorMessage ="Naziv kupca je obavezan")]
        public string ClientName { get; set; } = null!;
        [DisplayName("Adresa kupca")]
        [Required(ErrorMessage = "Adresa kupca je obavezan")]
        public string ClientAdress { get; set; } = null!;
        [DisplayName("Mesto")]
        [Required(ErrorMessage = "Mesto kupca je obavezano")]
        public string ClientPlace { get; set; } = null!;
        [DisplayName("Dan posete")]
        public string VisitDay { get; set; } = null!;
        [DisplayName("Rabat")]
        [Required(ErrorMessage = "Rabat kupca je obavezan")]
        public int RegularDiscount { get; set; }
        [DisplayName("Valuta plaćanja")]
        [Required(ErrorMessage = "Valuta kupca je obavezan")]
        public int PaysInDays { get; set; }
        [DisplayName("Region")]
        public string? RegionName { get; set; }

        public virtual ICollection<Lifleti> Lifletis { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
