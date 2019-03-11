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
    public class ProductsController : Controller
    {
        public Library.IOrderRepo Repo { get; }

        public ProductsController(Library.IOrderRepo repo)
        {
            Repo = repo;
        }

        // GET: Products
        public ActionResult Index()
        {
            IEnumerable<Library.Products> libProds = Repo.GetProducts();
            var model = libProds.Select(m => new ProductViewModel
            {
                Id = m.ProdId,
                ProdName = m.Name,
                Price = m.Price
            }).ToList();
            return View(model);
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Library.Products product = Repo.SearchProductsById((int)id);

            if (product == null)
            {
                return NotFound();
            }

            ProductViewModel model = new ProductViewModel
            {
                Id = product.ProdId,
                ProdName = product.Name,
                Price = product.Price
            };

            return View(model);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            var model = new ProductViewModel();
            return View(model);
        }

        // POST: Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Repo.AddProduct(new Library.Products
                    {
                        Name = model.ProdName,
                        Price = model.Price,
                    });
                    return RedirectToAction(nameof(Index));
                }
                else
                    return View(model);
            }
            catch
            {
                return View();
            }
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = Repo.SearchProductsById((int)id);
            if (product == null)
            {
                return NotFound();
            }
            ProductViewModel model = new ProductViewModel
            {
                Id = product.ProdId,
                ProdName = product.Name,
                Price = product.Price
            };
            return View(model);
        }

        // POST: Products/Edit/5
        /*[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Price")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
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
            return View(product);
        }

        // GET: Products/Delete/5
        /*public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Product.FindAsync(id);
            _context.Product.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.Id == id);
        }*/
    }
}
