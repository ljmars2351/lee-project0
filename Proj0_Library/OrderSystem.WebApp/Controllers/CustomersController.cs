using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OrderSystem.DataAccess;
using OrderSystem.WebApp.ViewModels;

namespace OrderSystem.WebApp.Controllers
{
    public class CustomersController : Controller
    {

        public Library.IOrderRepo Repo { get; }

        public CustomersController(Library.IOrderRepo repo)
        {
            Repo = repo;
        }

        // GET: Customers
        public ActionResult Index()
        {
            IEnumerable<Library.Customer> libCusts = Repo.GetCustomers();
            var viewModels = libCusts.Select(m => new CustomerViewModel
            {
                Id = m.CustId,
                FirstName = m.FName,
                LastName = m.LName,
                DateOfBirth = m.DateOfBirth,
                PrefLocNavigation = Repo.SearchLocationsById(m.Loc)
            }).ToList();
            return View(viewModels);
        }

        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Library.Customer customer = Repo.SearchCustomersById((int)id);

            if (customer == null)
            {
                return NotFound();
            }

            CustomerViewModel model = new CustomerViewModel
            {
                Id = customer.CustId,
                FirstName = customer.FName,
                LastName = customer.LName,
                DateOfBirth = customer.DateOfBirth,
                PrefLocNavigation = Repo.SearchLocationsById(customer.Loc)
            };

            return View(model);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            var model = new CustomerViewModel
            {
                PrefLoc = Repo.GetLocations().ToList()
            };
            return View(model);
        }

        // POST: Customers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("FirstName,LastName,DateOfBirth,PrefLoc")] Customer customer)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Repo.AddCustomer(new Library.Customer
                    {
                        FName = customer.FirstName,
                        LName = customer.LastName,
                        DateOfBirth = (DateTime)customer.DateOfBirth,
                        Loc = (int)customer.PrefLoc
                    });
                    return RedirectToAction(nameof(Index));
                }
                return View(customer);
            }
            catch
            {
                return View();
            }
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = Repo.SearchCustomersById((int)id);
            if (customer == null)
            {
                return NotFound();
            }
            CustomerViewModel model = new CustomerViewModel
            {
                Id = customer.CustId,
                FirstName = customer.FName,
                LastName = customer.LName,
                DateOfBirth = customer.DateOfBirth,
                PrefLoc = Repo.GetLocations().ToList()
            };
            return View(model);
        }

        // POST: Customers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind("FirstName,LastName,DateOfBirth,PrefLoc")] Customer customer)
        {
            if (id != customer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Repo.UpdateCustomer(Library.OrderMapper.Map(customer));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customer.Id))
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
            return View();
        }

        // GET: Customers/Delete/5
        /*public ActionResult Delete(int? id)
        {
            Library.Customer customer = Repo.SearchCustomersById((int)id);

            CustomerViewModel model = new CustomerViewModel
            {
                Id = customer.CustId,
                FirstName = customer.FName,
                LastName = customer.LName,
                DateOfBirth = customer.DateOfBirth,
                PrefLocNavigation = Repo.SearchLocationsById(customer.Loc)
            };
            return View(model);
        }

        // POST: Customers/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Deletedb(int id, [BindNever]IFormCollection collection)
        {
            try
            {
                Repo.DeleteCustomer(id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }*/

        private bool CustomerExists(int id)
        {
            bool yes = true;
            if (Repo.SearchCustomersById(id).Equals(null))
                yes = false;
            return yes;
        }
    }
}
