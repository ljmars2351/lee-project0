using System;
using System.Collections.Generic;
using System.Text;

namespace OrderSystem.Library
{
    public interface IOrderRepo
    {
        void AddCustomer(Customer cust);
        IEnumerable<Library.Location> GetLocations();
        void UpdateLocation(Customer cust);
        void UpdateCustomer(Customer cust);
        void AddInventory(Inventory inv);
        IEnumerable<Inventory> GetInventory(int locId);
        IEnumerable<Order> GetCustOrders(int custId);
        IEnumerable<Customer> GetCustomers();
        IEnumerable<Products> GetProducts();
        IEnumerable<Products> GetProductHistory();
        Products SearchProductsById(int prodId);
        Location SearchLocationsById(int locId);
        Customer SearchCustomersByName(string fName, string lName);
        void AddLocation(Location loc);
        void AddOrder(Order ord);
        void DeleteCustomer(int custId);
        void AddProduct(Products prod);
        void RemoveCustomer(Customer cust);
        IEnumerable<Order> GetAllOrders();
        Order GetOrderById(int id);
        void UpdateInv(List<Library.Inventory> inventories);
        Customer SearchCustomersById(int custId);
        IEnumerable<Order> SearchOrdersByStore(int locId);
    }
}
