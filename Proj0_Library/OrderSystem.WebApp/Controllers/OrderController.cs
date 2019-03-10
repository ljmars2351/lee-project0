using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OrderSystem.DataAccess;

namespace OrderSystem.WebApp.Controllers
{
    public class OrderController : Controller
    {
        private readonly Proj0Context _context;

        public OrderController(Proj0Context context)
        {
            _context = context;
        }

        // GET: Order
        public async Task<IActionResult> Index()
        {
            var proj0Context = _context.Cart.Include(c => c.Cust).Include(c => c.Loc).Include(c => c.Prod);
            return View(await proj0Context.ToListAsync());
        }

        // GET: Order/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cart = await _context.Cart
                .Include(c => c.Cust)
                .Include(c => c.Loc)
                .Include(c => c.Prod)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cart == null)
            {
                return NotFound();
            }

            return View(cart);
        }

        // GET: Order/Create
        public IActionResult Create()
        {
            ViewData["CustId"] = new SelectList(_context.Customer, "Id", "Id");
            ViewData["LocId"] = new SelectList(_context.Location, "Id", "City");
            ViewData["ProdId"] = new SelectList(_context.ProdHist, "Id", "Name");
            return View();
        }

        // POST: Order/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CustId,ProdId,LocId,CurrentTime,Quantity")] Cart cart)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cart);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustId"] = new SelectList(_context.Customer, "Id", "Id", cart.CustId);
            ViewData["LocId"] = new SelectList(_context.Location, "Id", "City", cart.LocId);
            ViewData["ProdId"] = new SelectList(_context.ProdHist, "Id", "Name", cart.ProdId);
            return View(cart);
        }

        // GET: Order/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cart = await _context.Cart.FindAsync(id);
            if (cart == null)
            {
                return NotFound();
            }
            ViewData["CustId"] = new SelectList(_context.Customer, "Id", "Id", cart.CustId);
            ViewData["LocId"] = new SelectList(_context.Location, "Id", "City", cart.LocId);
            ViewData["ProdId"] = new SelectList(_context.ProdHist, "Id", "Name", cart.ProdId);
            return View(cart);
        }

        // POST: Order/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CustId,ProdId,LocId,CurrentTime,Quantity")] Cart cart)
        {
            if (id != cart.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cart);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CartExists(cart.Id))
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
            ViewData["CustId"] = new SelectList(_context.Customer, "Id", "Id", cart.CustId);
            ViewData["LocId"] = new SelectList(_context.Location, "Id", "City", cart.LocId);
            ViewData["ProdId"] = new SelectList(_context.ProdHist, "Id", "Name", cart.ProdId);
            return View(cart);
        }

        // GET: Order/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cart = await _context.Cart
                .Include(c => c.Cust)
                .Include(c => c.Loc)
                .Include(c => c.Prod)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cart == null)
            {
                return NotFound();
            }

            return View(cart);
        }

        // POST: Order/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cart = await _context.Cart.FindAsync(id);
            _context.Cart.Remove(cart);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CartExists(int id)
        {
            return _context.Cart.Any(e => e.Id == id);
        }
    }
}
