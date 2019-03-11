using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OrderSystem.DataAccess;
using OrderSystem.WebApp.ViewModels;

namespace OrderSystem.WebApp.Controllers
{
    public class OrderController : Controller
    {
        public Library.IOrderRepo Repo { get; }

        public OrderController(Library.IOrderRepo repo)
        {
            Repo = repo;
        }

        // GET: Order
        public ActionResult Index()
        {
            IEnumerable<Library.Order> libOrd = Repo.GetAllOrders();
            var model = libOrd.Select(m => new OrderViewModel
            {
                Customer = Repo.SearchCustomersById(m.CustId),
                Prod = Repo.SearchProductsById(m.ProdId),
                Location = Repo.SearchLocationsById(m.StoreId),
                OrderId = m.OrdId,
                OrdTime = m.OrdTIme,
                Quant = m.Quantity
            });
            return View(model);
        }

        // GET: Order/Details/5
        public ActionResult Details(int id)
        {
            

            Library.Order cart = Repo.GetOrderById((int)id);

            

            OrderViewModel model = new OrderViewModel
            {
                OrderId = cart.OrdId,
                Customer = Repo.SearchCustomersById(cart.CustId),
                Location = Repo.SearchLocationsById(cart.StoreId),
                Prod = Repo.SearchProductsById(cart.ProdId),
                OrdTime = cart.OrdTIme,
                Quant = cart.Quantity
            };

            return View(model);
        }

        // GET: Order/Create
        public ActionResult Create()
        {
            var model = new OrderViewModel();
            return View(model);
        }

        // POST: Order/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(OrderViewModel cart)
        {
            try
            {
                var model = new OrderViewModel();
                if (ModelState.IsValid)
                {
                    Repo.AddOrder(new Library.Order
                    {
                        CustId = cart.CustomerId,
                        StoreId = cart.LocationId,
                        ProdId = cart.ProdId,
                        Quantity = cart.Quant,
                        OrdTIme = DateTime.Now
                    });
                    return RedirectToAction(nameof(Index));
                }

                return View(model);
            }
            catch
            {
                return View();
            }
        }

        // GET: Order/Edit/5
        /*public ActionResult Edit(int? id)
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind("Id,CustId,ProdId,LocId,CurrentTime,Quantity")] Cart cart)
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
        public ActionResult Delete(int? id)
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
        public ActionResult DeleteConfirmed(int id)
        {
            var cart = await _context.Cart.FindAsync(id);
            _context.Cart.Remove(cart);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CartExists(int id)
        {
            return _context.Cart.Any(e => e.Id == id);
        }*/
    }
}
