using System.ComponentModel;

namespace SalesApp.Models.ViewModel
{
    public class Potrazivanja
    {
        public int Id { get; set; }
        [DisplayName("Naziv kupca")]
        public string NameOfClient { get; set; }
        [DisplayName("Iznos potraživanja")]
        public double AmountOfDebt { get; set; }  
    }
}
