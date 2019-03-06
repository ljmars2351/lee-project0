using System;
using System.Collections.Generic;
using System.Text;

namespace OrderSystem.Library
{
    public interface IOrderRepo
    {
        void AddCustomer(Customer cust);

        void AddLocation(Location loc);
        void AddOrder(List<Order> ord);
        void AddProduct(Products prod);
        void RemoveCustomer(Customer cust);
        void UpdateInv(List<Library.Inventory> inventories);
        Customer SearchCustomersById(int custId);
        IEnumerable<Order> SearchOrdersByStore(int locId);
    }
}
