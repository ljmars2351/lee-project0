using System;
using System.Collections.Generic;
using System.Text;

namespace OrderSystem.Library
{
    public interface IOrderRepo
    {
        void AddCustomer(Customer cust);

        void AddLocation(Location loc);
        void AddOrder(Order ord);
        void AddProduct(Products prod);
        void RemoveCustomer(int id);
        void UpdateInv(Order ord);
        Customer SearchCustomersById(int custId);
        Order SearchOrdersById(int ordId);
    }
}
