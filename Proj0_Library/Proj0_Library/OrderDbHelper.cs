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

        public void AddOrder(Order ord)
        {
            Order temp = OrderMapper.Map(_db.Cart.Last());
            int newId = temp.OrdId + 1;
            ord.OrdId = newId;
            _db.Add(OrderMapper.Map(ord));
            _db.SaveChanges();
        }

        public void AddProduct(Products prod)
        {
            _db.Add(OrderMapper.Map(prod));
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

        public void UpdateInv(Order ord)
        {
           // _db.
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

        public Customer SearchCustomersById(int custId)
        {
            if (_db.Customer.Any(s => s.Id == custId))
            {
                return OrderMapper.Map(_db.Customer.Find(custId));
            }
            else
                return null;
        }

        public Order SearchOrdersById(int ordId)
        {
            throw new NotImplementedException();
        }
    }
}
