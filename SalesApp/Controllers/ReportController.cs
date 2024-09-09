using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesApp.Models;
using SalesApp.Models.ViewModel;

namespace SalesApp.Controllers
{
    [Authorize]

    public class ReportController : Controller
    {
        private readonly FieldSalesContext _context;

        public ReportController(FieldSalesContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            decimal? sumSum = 0;
            int? sumWeight=0;
            var weight=_context.OrderDetails
                         .Where(o=>o.Quantity>0)
                         .ToList();
            foreach(var item in weight)
            {
                
                
                    sumWeight += item.Quantity;
                
                
            }

            var sum=_context.OrderDetails
                .Include(o=>o.Product)
                .Where(o=>o.Quantity>0)
                .ToList();
                
            foreach (var item in sum)
            {
                sumSum += item.Quantity * item.Product.ProductPrice;
            }


            Report report=new Report();

            report.OrderNumber=_context.Orders.Count();
            report.OrderWeight =(double) sumWeight*500/1000 ;
            report.OrderSum =(double) sumSum;




            return View("Index",report);
        }
    }
}
