using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using OrderSystem.DataAccess;
using System.Linq;

namespace OrderSystem.Library
{
    public class OrderDbHelper : IOrderRepo
    {
        private readonly Proj0Context _db;

        public OrderDbHelper(Proj0Context db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public void AddCustomer(Customer cust)
        {
            _db.Add(OrderMapper.Map(cust));
            _db.SaveChanges();
        }

        public void AddLocation(Location loc)
        {
            _db.Add(OrderMapper.Map(loc));
            _db.SaveChanges();
        }

        public void AddOrder(List<Order> ord)
        {
            Order temp = new Order();
            int newId = 0;
            if (_db.Cart.Any())
            {
                temp = OrderMapper.Map(_db.Cart.Last());
                newId = temp.OrdId + 1;
            }
            for (int x = 0; x < ord.Count; x++)
            {
                ord[x].OrdId = newId;
                _db.Add(OrderMapper.Map(ord[x]));
                _db.SaveChanges();
            }
        }

        public void AddProduct(Products prod)
        {
            if (_db.Product.Any(i => i.Id == prod.ProdId))
            {
                _db.Product.First(p => p.Id == prod.ProdId).Price = prod.Price;
            }
            _db.Add(OrderMapper.DualMap(prod));
            _db.SaveChanges();
        }

        public void RemoveCustomer(Customer cust)
        {
            _db.Remove(OrderMapper.Map(cust));
        }

        public IEnumerable<Library.Location> GetLocations()
        {
            return OrderMapper.Map(_db.Location.Include(r => r.Customer));
        }

        public void UpdateLocation(Customer cust)
        {
            _db.Customer.First(c => c.Id == cust.CustId).PrefLoc = cust.Loc;
            _db.SaveChanges();
        }

        public void UpdateCustomer(Customer cust)
        {
            _db.Customer.First(c => c.Id == cust.CustId).PrefLoc = cust.Loc;
            _db.Customer.First(c => c.Id == cust.CustId).FirstName = cust.FName;
            _db.Customer.First(c => c.Id == cust.CustId).LastName = cust.LName;
            _db.Customer.First(c => c.Id == cust.CustId).DateOfBirth = cust.DateOfBirth;
            _db.SaveChanges();
        }

        public void AddInventory(Library.Inventory inv)
        {
            if (_db.Inventory.Any(s => s.LocationId == inv.LocationId && s.ProdId == inv.ProductId))
            {
                _db.Inventory.First(i => i.ProdId == inv.ProductId && i.LocationId == inv.LocationId).Quant = inv.Quantity;
            }
            else
            {
                _db.Add(OrderMapper.Map(inv));
            }
            _db.SaveChanges();
        }

        public IEnumerable<Library.Inventory> GetInventory(int LocId)
        {
            return OrderMapper.Map(_db.Inventory.Where(r => r.LocationId == LocId));
        }

        public IEnumerable<Library.Order> GetOrders(int custId)
        {
            return OrderMapper.Map(_db.Cart.Include(r => r.Cust).Where(r => r.Cust.Id == custId));
        }

        public IEnumerable<Library.Customer> GetCustomers()
        {
            return OrderMapper.Map(_db.Customer);
        }

        public IEnumerable<Library.Products> GetProducts()
        {
            return OrderMapper.Map(_db.Product);
        }

        public IEnumerable<Library.Products> GetProductHistory()
        {
            return OrderMapper.Map(_db.ProdHist);
        }

        public void UpdateInv(List<Inventory> inventories)
        {
            for (int x = 0; x < inventories.Count; x++)
            {
                _db.Inventory.First(i => i.LocationId == inventories[x].LocationId && i.ProdId == inventories[x].ProductId).Quant = inventories[x].Quantity;
            }
            _db.SaveChanges();
        }

        public Products SearchProductsById(int prodId)
        {
            if (_db.ProdHist.Any(s => s.Id == prodId))
            {
                return OrderMapper.Map(_db.ProdHist.Find(prodId));
            }
            else
                return null;
        }

        public Location SearchLocationsById(int locId)
        {
            if (_db.Location.Any(s => s.Id == locId))
            {
                return OrderMapper.Map(_db.Location.Find(locId));
            }
            else
                return null;
        }

        public Customer SearchCustomersById(int custId)
        {
            if (_db.Customer.Any(s => s.Id == custId))
            {
                return OrderMapper.Map(_db.Customer.Find(custId));
            }
            else
                return null;
        }

        public void UpdatePrice(Library.Inventory inv, decimal price)
        {

        }

        public Customer SearchCustomersByName(string fName, string lName)
        {
            if (_db.Customer.Any(s => s.FirstName == fName && s.LastName == lName))
            {
                return OrderMapper.Map(_db.Customer.First(s => s.FirstName == fName && s.LastName == lName));
            }
            else
                return null;
        }

        public IEnumerable<Order> SearchOrdersByStore(int LocId)
        {
            return OrderMapper.Map(_db.Cart.Where(r => r.LocId == LocId));
        }
    }
}
