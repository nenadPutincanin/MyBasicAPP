using SalesAPI.Data.Entities;

namespace SalesAPI.Data
{
    public interface IPotrazivanjaRepo
    {
        public List<Potrazivanja> GetPotrazivanja();
    }
}
