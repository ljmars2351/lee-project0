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
    public class LocationsController : Controller
    {
        public Library.IOrderRepo Repo { get; }

        public LocationsController(Library.IOrderRepo repo)
        {
            Repo = repo;
        }

        // GET: Locations
        public ActionResult Index()
        {
            IEnumerable<Library.Location> libLocs = Repo.GetLocations();
            var model = libLocs.Select(m => new LocationViewModel
            {
                LocationId = m.LocId,
                Address = m.StreetAddress,
                City = m.City,
                State = m.State,
                Zip = m.Zip
            });
            return View(model);
        }

        // GET: Locations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Library.Location location = Repo.SearchLocationsById((int)id);
            List<Library.Inventory> StoreInv = new List<Library.Inventory>();

            if (location == null)
            {
                return NotFound();
            }

            LocationViewModel model = new LocationViewModel
            {
                LocationId = location.LocId,
                Address = location.StreetAddress,
                City = location.City,
                State = location.State,
                Zip = location.Zip,
                Prod = Repo.GetProducts().ToList(),
                Inventory = Repo.GetInventory(location.LocId).ToList()
            };
            bool hs = true;
            for(int x = 0; x < model.Prod.Count; x++)
            {
                Library.Inventory Inv = new Library.Inventory();
                Inv.ProductId = model.Prod[x].ProdId;
                hs = model.Inventory.Any(
                    y => y.LocationId == model.LocationId && 
                    y.ProductId == model.Prod[x].ProdId);
                if (hs == true)
                {
                    Inv.Quantity = model.Inventory.First(
                        y => y.LocationId == model.LocationId &&
                        y.ProductId == model.Prod[x].ProdId).Quantity;
                }
                else
                    Inv.Quantity = 0;
                Inv.LocationId = model.LocationId;
                StoreInv.Add(Inv);
            }
            model.StoreInv = StoreInv;

            return View(model);
        }

        // GET: Locations/Create
        public ActionResult Create()
        {
            var model = new LocationViewModel
            {
            };
            return View(model);
        }

        // POST: Locations/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(LocationViewModel location)
        {
            try
            {
                var model = new LocationViewModel();
                if (ModelState.IsValid)
                {
                    Repo.AddLocation(new Library.Location
                    {
                        StreetAddress = location.Address,
                        City = location.City,
                        State = location.State,
                        Zip = location.Zip
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

        // GET: Locations/Edit/5
        /*public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var location = await _context.Location.FindAsync(id);
            if (location == null)
            {
                return NotFound();
            }
            return View(location);
        }

        // POST: Locations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Street,City,State,Zip")] Location location)
        {
            if (id != location.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(location);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LocationExists(location.Id))
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
            return View(location);
        }

        // GET: Locations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var location = await _context.Location
                .FirstOrDefaultAsync(m => m.Id == id);
            if (location == null)
            {
                return NotFound();
            }

            return View(location);
        }

        // POST: Locations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var location = await _context.Location.FindAsync(id);
            _context.Location.Remove(location);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LocationExists(int id)
        {
            return _context.Location.Any(e => e.Id == id);
        }*/
    }
}
