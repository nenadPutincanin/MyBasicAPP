using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SalesApp.Models;

namespace SalesApp.Controllers
{
    [Authorize]

    public class LifletisController : Controller
    {
        private readonly FieldSalesContext _context;

        public LifletisController(FieldSalesContext context)
        {
            _context = context;
        }

        // GET: Lifletis
        
        public async Task<IActionResult> Index()
        {
            var fieldSalesContext = _context.Lifletis.Include(l => l.Client).Include(l => l.Product);
            return View(await fieldSalesContext.ToListAsync());
        }

        // GET: Lifletis/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Lifletis == null)
            {
                return NotFound();
            }

            var lifleti = await _context.Lifletis
                .Include(l => l.Client)
                .Include(l => l.Product)
                .FirstOrDefaultAsync(m => m.LifletId == id);
            if (lifleti == null)
            {
                return NotFound();
            }

            return View(lifleti);
        }

        // GET: Lifletis/Create
        public IActionResult Create()
        {
            ViewData["ClientId"] = new SelectList(_context.Clients, "ClientId", "ClientName");
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductName");
            return View();
        }

        // POST: Lifletis/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LifletId,AdditionalDiscount,DateFrom,DateTo,ProductId,ClientId")] Lifleti lifleti)
        {
            

            if (ModelState.IsValid)
            {
                _context.Add(lifleti);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.ClientId= new SelectList(_context.Clients, "ClientId", "ClientName",lifleti.Client.ClientName);
            //ViewData["ClientId"] = new SelectList(_context.Clients, "ClientName", "ClientName", lifleti.ClientId);
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductName", "ProductName", lifleti.ProductId);
            return View(lifleti);
        }

        // GET: Lifletis/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Lifletis == null)
            {
                return NotFound();
            }

            var lifleti = await _context.Lifletis.FindAsync(id);
            if (lifleti == null)
            {
                return NotFound();
            }
            ViewData["ClientId"] = new SelectList(_context.Clients, "ClientId", "ClientId", lifleti.ClientId);
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId", lifleti.ProductId);
            return View(lifleti);
        }

        // POST: Lifletis/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LifletId,AdditionalDiscount,DateFrom,DateTo,ProductId,ClientId")] Lifleti lifleti)
        {
            if (id != lifleti.LifletId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lifleti);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LifletiExists(lifleti.LifletId))
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
            ViewData["ClientId"] = new SelectList(_context.Clients, "ClientId", "ClientId", lifleti.ClientId);
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId", lifleti.ProductId);
            return View(lifleti);
        }

        // GET: Lifletis/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Lifletis == null)
            {
                return NotFound();
            }

            var lifleti = await _context.Lifletis
                .Include(l => l.Client)
                .Include(l => l.Product)
                .FirstOrDefaultAsync(m => m.LifletId == id);
            if (lifleti == null)
            {
                return NotFound();
            }

            return View(lifleti);
        }

        // POST: Lifletis/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Lifletis == null)
            {
                return Problem("Entity set 'FieldSalesContext.Lifletis'  is null.");
            }
            var lifleti = await _context.Lifletis.FindAsync(id);
            if (lifleti != null)
            {
                _context.Lifletis.Remove(lifleti);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LifletiExists(int id)
        {
          return (_context.Lifletis?.Any(e => e.LifletId == id)).GetValueOrDefault();
        }

        public IActionResult FilterByClient(string ClientName)
        {

            if (ClientName==null)
            {

                var Without = _context.Lifletis
                .Include(c => c.Client)
                .Include(c => c.Product)
                
                .ToList();


                return View(Without);


            }

            

            var dataLif=_context.Lifletis
                .Include(c => c.Client)
                .Include(c => c.Product)
                .Where(o=>o.Client.ClientName.Contains(ClientName) )
                .ToList();



            return View("FilterByClient",dataLif);
        }
    }
}
