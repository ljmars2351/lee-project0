using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using OrderSystem.DataAccess;

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

        }

        public void AddProduct(Products prod)
        {
            _db.Add(OrderMapper.Map(prod));
            _db.Add(OrderMapper.DualMap(prod));
            _db.SaveChanges();
        }

        public void RemoveCustomer(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateInv(Order ord)
        {
            _db.
        }

        public Customer SearchCustomersById(int custId)
        {
            throw new NotImplementedException();
        }

        public Order SearchOrdersById(int ordId)
        {
            throw new NotImplementedException();
        }
    }
}
