using SalesAPI.Data.Entities;

namespace SalesAPI.Data
{
    public class PotrazivanjaRepo : IPotrazivanjaRepo
    {
        private readonly PotrazivanjaContext _context;

        public PotrazivanjaRepo(PotrazivanjaContext context)
        {
            _context = context;
        }

        public List<Potrazivanja> GetPotrazivanja()
        {
            return _context.Potrazivanja.ToList();
        }
    }
}
