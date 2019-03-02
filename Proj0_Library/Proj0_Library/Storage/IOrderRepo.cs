using OrderSystem.Library.Layouts;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderSystem.Library.Storage
{
    public interface IOrderRepo
    {
        //public List<Customer> customers = new List<Customer>();
        //public List<Location> locs = new List<Location>();
        //public List<Products> prods = new List<Products>();
        //public List<Order> ords = new List<Order>();

        //void AddCustomer(Proj0Context dbContext, string fName, string lName, DateTime dob, int prefLoc);

        //Customer newCust = new Customer();
        //newCust.FName = fName;
        //newCust.LName = lName;
        //newCust.DateOfBirth = dob;
        //newCust.Loc = prefLoc;
        //newCust.CustId = id;
        //customers.Add(newCust);


        //void addLocation(Proj0Context dbContext, string street, string city, int zip, string state, List<Product> inv);

        //Location loc = new Location();
        //loc.LocId = locId;
        //loc.StreetAddress = street;
        //loc.City = city;
        //loc.Zip = zip;
        //loc.State = state;
        //loc.inventory = inv;
        //locs.Add(loc);


        //void addProduct(Proj0Context dbContext, int prodId, string itemName, decimal price);

        //Products prod = new Products();
        //prod.ProdId = prodId;
        //prod.IName = itemName;
        //prod.Quant = 0;
        //prod.Price = price;
        //prods.Add(prod);


        //void addOrder(Proj0Context dbContext, int custId, int storeId, List<Product> cart);

        //Order ord = new Order();
        //ord.CustId = custId;
        //ord.OrdId = ordId;
        //ord.StoreId = storeId;
        //ord.Cart = cart;
        //ords.Add(ord);


        //void removeCustomer(Proj0Context dbContext, int id);

        //for(int x = 0; x < customers.Count; x++)
        //{
        //    if(custo)
        //}
        //customers.Remove()


        //Customer searchCustomersById(Proj0Context dbContext, int custId);

        //int x = 0;
        //while(customers[x].CustId != custId || x != customers.Count)
        //{
        //    x++;
        //}
        //return customers[x];


        //OrderHistory searchOrdersByCustId(Proj0Context dbContext, int custId);
        
            //int x = 0;
            //while (ords[x].OrdId != ordId || x != ords.Count)
            //{
            //    x++;
            //}
            //return ords[x];
        
    }
}
