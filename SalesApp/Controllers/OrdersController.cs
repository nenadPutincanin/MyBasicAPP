using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SalesApp.Models;

namespace SalesApp.Controllers
{
    [Authorize]

    public class OrdersController : Controller
    {
        private readonly FieldSalesContext _context;

        public OrdersController(FieldSalesContext context)
        {
            _context = context;
        }

      

        // GET: Orders
        public async Task<IActionResult> Index()
        {


            var fieldSalesContext = _context.Orders.Include(o => o.Client);
                             

            return View(await fieldSalesContext.ToListAsync());



        }
        [Authorize]

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            List<OrderDetail> orderDetails = new List<OrderDetail>();
            foreach (var product in _context.Products.ToList())
            {
                OrderDetail tmpOd = _context.OrderDetails.SingleOrDefault(x => x.OrderId == id && x.ProductId == product.ProductId);
                int tmpQ = 0;
                if (tmpOd != null)
                {
                    tmpQ = tmpOd.Quantity ?? 0;
                }
                OrderDetail orderDetail = new OrderDetail();
                orderDetail.ProductId = product.ProductId;
                orderDetail.Quantity = tmpQ;
                orderDetail.OrderId = -1;
                orderDetail.Product = product;
                orderDetails.Add(orderDetail);
            }
            ViewBag.OrderDetails = orderDetails;
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.Client)
                .Include(d=>d.OrderDetails)
                .FirstOrDefaultAsync(m => m.OrderId == id);

            
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            ViewData["ClientId"] = new SelectList(_context.Clients, "ClientId", "ClientId");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderId,ClientId,RegionName,OrderDate")] Order order)
        {
            if (ModelState.IsValid)
            {
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClientId"] = new SelectList(_context.Clients, "ClientId", "ClientId", order.ClientId);
            return View(order);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            ViewData["ClientId"] = new SelectList(_context.Clients, "ClientId", "ClientId", order.ClientId);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderId,ClientId,RegionName,OrderDate")] Order order)
        {
            if (id != order.OrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.OrderId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClientId"] = new SelectList(_context.Clients, "ClientId", "ClientId", order.ClientId);
            return View(order);
        }

        // GET: Orders/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.Client)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Orders == null)
            {
                return Problem("Entity set 'FieldSalesContext.Orders'  is null.");
            }
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
          return (_context.Orders?.Any(e => e.OrderId == id)).GetValueOrDefault();
        }

        public IActionResult FilterByDateOrders(DateTime from,DateTime to)
        {
            

            if (from == DateTime.MinValue && to == DateTime.MinValue)

            {

                var WithoutDate = _context.Orders
                    .Include(o => o.Client)
                   
                    .ToList();

                return View(WithoutDate);

            }

            var dataOrder = _context.Orders
                .Include (o => o.Client)
                .Where(o=>o.OrderDate>=from && o.OrderDate<=to)
                .ToList();

            return View(dataOrder);

        }
    }
}
