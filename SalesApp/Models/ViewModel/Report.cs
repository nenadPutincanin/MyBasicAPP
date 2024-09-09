using System.ComponentModel;

namespace SalesApp.Models.ViewModel
{
    public class Report
    {
        [DisplayName("Broj porudžbenica")]
        public int OrderNumber { get; set; }
        [DisplayName("Ukupna težina")]
        public double OrderWeight { get; set; }
        [DisplayName("Ukupan iznos")]
        public double OrderSum {  get; set; }

    }
}
