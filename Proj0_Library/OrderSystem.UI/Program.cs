using System;
using Microsoft.EntityFrameworkCore;
using OrderSystem.Library;
using OrderSystem.DataAccess;
using System.Linq;
using System.Collections.Generic;

namespace OrderSystem.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            string entry;
            int num = 0;
            bool cont = false, cont2 = false;
            var optionsBuilder = new DbContextOptionsBuilder<Proj0Context>();
            optionsBuilder.UseSqlServer(SecretConfig.ConnectionString);
            var options = optionsBuilder.Options;
            var dbContext = new Proj0Context(options);


            var orderRepo = new OrderDbHelper(dbContext);
            {
                while (num != 99)
                {
                    var custList = orderRepo.GetCustomers();
                    var locList = orderRepo.GetLocations();
                    var prodList = orderRepo.GetProducts();
                    var prodHist = orderRepo.GetProductHistory();
                    while (cont == false)
                    {
                        Console.WriteLine("Please identify the authorization of the user, enter 99 to quit at anytime");
                        Console.WriteLine("1. Customer");
                        Console.WriteLine("2. Employee");
                        Console.WriteLine("3. Administrator");
                        entry = Console.ReadLine();
                        cont = int.TryParse(entry, out num);
                        if (cont == false || num > 3 && num != 99)
                        {
                            Console.WriteLine("Please enter a valid number.");
                            cont = false;
                        }
                    }
                    cont = false;
                    if (num == 1)
                    {
                        while (cont == false || num > 5)
                        {
                            Console.WriteLine("1. Create a new account");
                            Console.WriteLine("2. Create a new order");
                            Console.WriteLine("3. Change preferred location");
                            Console.WriteLine("4. View past orders");
                            Console.WriteLine("5. View/update personal information");
                            entry = Console.ReadLine();
                            cont = int.TryParse(entry, out num);
                            if (cont == false)
                            {
                                Console.WriteLine("Please enter a valid number.");
                            }
                        }
                        cont = false;
                        if (num == 1)
                        {
                            var newCust = new Library.Customer();
                            DateTime dob = new DateTime();
                            while (cont == false)
                            {
                                Console.WriteLine("Enter the first name.");
                                entry = Console.ReadLine();
                                newCust.FName = entry;
                                Console.WriteLine("Enter the last name");
                                entry = Console.ReadLine();
                                newCust.LName = entry;
                                Console.WriteLine("Enter the birthday (yyyy-mm-dd)");
                                entry = Console.ReadLine();
                                cont = DateTime.TryParse(entry, out dob);
                                if (cont == false)
                                {
                                    Console.WriteLine("Please enter a valid birthday.");
                                }
                                newCust.DateOfBirth = dob;
                                newCust.Loc = 4;
                            }
                            orderRepo.AddCustomer(newCust);
                            cont = false;
                            num = 0;
                        }
                        if (num == 2)
                        {
                            var contOrd = true;
                            var newOrdList = new List<Order>();
                            var updateList = new List<Library.Inventory>();
                            var newUp = new Library.Inventory();
                            var newOrd = new Order();                             
                            DateTime time = new DateTime();
                            var cust = new Library.Customer();
                            while (cont == false)
                            {
                                Console.WriteLine("Please enter the customer ID");
                                entry = Console.ReadLine();
                                cont = int.TryParse(entry, out num);
                                if (cont == false)
                                {
                                    Console.WriteLine("Please enter a valid ID");
                                }
                                cust = orderRepo.SearchCustomersById(num);
                                if (cust == null)
                                {

                                    while (cont2 == false || num > 2)
                                    {
                                        Console.WriteLine("Customer does not exist, would you like to create?");
                                        Console.WriteLine("1. Yes");
                                        Console.WriteLine("2. No");
                                        entry = Console.ReadLine();
                                        cont2 = int.TryParse(entry, out num);
                                        if (cont2 == false)
                                        {
                                            Console.WriteLine("Please enter a valid option");
                                            cont2 = false;
                                        }
                                    }
                                    cont2 = false;
                                    if (num == 1)
                                    {
                                        Console.WriteLine("Enter the first name");
                                        entry = Console.ReadLine();
                                        cust.FName = entry;
                                        Console.WriteLine("Enter the last name");
                                        entry = Console.ReadLine();
                                        cust.LName = entry;

                                        while (cont2 == false)
                                        {
                                            Console.WriteLine("Enter the date of birth (yyyy-mm-dd)");
                                            entry = Console.ReadLine();
                                            cont2 = DateTime.TryParse(entry, out time);
                                            if (cont2 == false)
                                            {
                                                Console.WriteLine("Please enter a valid birthday");
                                            }
                                        }
                                        cont2 = false;
                                        cust.DateOfBirth = time;
                                        cust.Loc = 3;
                                        orderRepo.AddCustomer(cust);
                                    }
                                    if (num == 2)
                                    {
                                        cont = false;
                                    }
                                }
                                cont = false;
                                newOrd.CustId = num;
                                while (cont == false)
                                {
                                    Console.WriteLine("Please enter the location ID");
                                    entry = Console.ReadLine();
                                    cont = int.TryParse(entry, out num);
                                    if (cont == false)
                                    {
                                        Console.WriteLine("Please enter a valid ID");
                                    }
                                }
                                newOrd.StoreId = num;
                                var pList = orderRepo.GetProducts().ToList();
                                List<Library.Inventory> stList = new List<Library.Inventory>();
                                cont = false;
                                while (cont == false)
                                {
                                    stList = orderRepo.GetInventory(newOrd.StoreId).ToList();
                                    if (stList.Count == 0)
                                    {
                                        cont = false;
                                        Console.WriteLine("That store either doesn't exist or has no inventory, try again");
                                    }
                                    else
                                        cont = true;
                                }
                                cont = false;
                                int rep = 0;
                                int newOnHand = 0;
                                while (contOrd == true)
                                {
                                    cont = false;
                                    newOrd.StoreId = num;
                                    Console.WriteLine("Please select a product from the list below");
                                    for (int z = 0; z < pList.Count; z++)
                                    {
                                        Console.WriteLine($"ID: {pList[z].ProdId}\nName: {pList[z].Name}\nPrice: {pList[z].Price}\n" +
                                            $"Store's on hand quantity: {stList.First(s => s.ProductId == pList[z].ProdId).Quantity}");
                                    }
                                    while (cont == false)
                                    {
                                        Console.WriteLine("Please enter the product ID");
                                        entry = Console.ReadLine();
                                        cont = int.TryParse(entry, out num);
                                        if (cont == false)
                                        {
                                            Console.WriteLine("Please enter a valid ID");
                                        }
                                    }
                                    cont = false;
                                    newOrd.ProdId = num;
                                    while (cont == false)
                                    {
                                        Console.WriteLine("Please enter the desired quantity");
                                        entry = Console.ReadLine();
                                        cont = int.TryParse(entry, out num);
                                        if (cont == false)
                                        {
                                            Console.WriteLine("Please enter a valid number");
                                        }
                                    }
                                    cont = false;
                                    int onHand = 0;
                                    try
                                    {
                                        onHand = stList.First(v => v.ProductId == newOrd.ProdId).Quantity;
                                    }
                                    catch(InvalidOperationException)
                                    {
                                        Console.WriteLine("Something went wrong, try again");
                                    }
                                    if (num > onHand)
                                    {
                                        Console.WriteLine("Quantity exceeds store on hand supply");
                                    }
                                    else
                                    {
                                        newOrd.Quantity = num;
                                        time = DateTime.Now;
                                        newOrd.OrdTIme = time;
                                        newOrdList.Add(newOrd);
                                        newOnHand = onHand - newOrd.Quantity;
                                        stList.First(u => u.LocationId == newOrd.StoreId && u.ProductId == newOrd.ProdId).Quantity = newOnHand;
                                        newUp.LocationId = newOrd.StoreId;
                                        newUp.ProductId = newOrd.ProdId;
                                        newUp.Quantity = newOnHand;
                                        updateList.Add(newUp);
                                        num = newOrd.StoreId;
                                    }
                                    contOrd = false;
                                }
                                contOrd = false;
                                int sub = 0;
                                while(cont == false)
                                {
                                    Console.WriteLine("Submit order? 1. Yes 2. No");
                                    entry = Console.ReadLine();
                                    cont = int.TryParse(entry, out sub);
                                    if(cont == false)
                                        Console.WriteLine("Invalid input");
                                }
                                if (sub == 1)
                                {
                                    orderRepo.AddOrder(newOrdList);
                                    orderRepo.UpdateInv(updateList);
                                    Console.WriteLine("Order submitted");
                                }
                                else
                                    Console.WriteLine("Order not submitted");
                            }
                            num = 0;
                        }
                        if (num == 3)
                        {
                            var cust = new Library.Customer();
                            while (cont == false)
                            {
                                Console.WriteLine("Please enter your customer ID");
                                entry = Console.ReadLine();
                                cont = int.TryParse(entry, out num);
                                if (cont == false)
                                {
                                    Console.WriteLine("Please enter a valid ID");
                                }
                                cust = orderRepo.SearchCustomersById(num);
                            }
                            cont = false;
                            while (cont == false)
                            {
                                Console.WriteLine("Please select from the current list of locations");
                                var list = orderRepo.GetLocations().ToList();
                                for (int x = 0; x < list.Count; x++)
                                {
                                    var loc = list[x];

                                    Console.WriteLine($"Location ID: {loc.LocId}\nLocation Address: {loc.StreetAddress} {loc.City} {loc.State}\n");
                                }
                                Console.WriteLine("Enter the new location ID");
                                entry = Console.ReadLine();
                                cont = int.TryParse(entry, out num);
                                if (cont == false)
                                {
                                    Console.WriteLine("Please enter a valid ID");
                                }
                            }
                            cust.Loc = num;
                            orderRepo.UpdateLocation(cust);
                            cont = false;
                            num = 0;
                        }
                        if (num == 4)
                        {
                            while (cont == false)
                            {
                                Console.WriteLine("Please enter the customer ID you want to view");
                                entry = Console.ReadLine();
                                cont = int.TryParse(entry, out num);
                                if (cont == false)
                                {
                                    Console.WriteLine("Please enter a valid ID");
                                }
                            }
                            cont = false;
                            var list = orderRepo.GetOrders(num).ToList();
                            if(list.Count == 0)
                            {
                                Console.WriteLine("Customer either does not exist or has no orders");
                            }
                            for (int x = 0; x < list.Count; x++)
                            {
                                var cart = list[x];
                                var name = orderRepo.SearchCustomersById(cart.CustId);
                                var item = orderRepo.SearchProductsById(cart.ProdId);
                                Console.WriteLine($"The customer's name is {name.FName} {name.LName}\n" +
                                    $"The order ID is {cart.OrdId}\nThe ordered product name is {item.Name}\n" +
                                    $"The quantity ordered is {cart.Quantity}\nThe store's ID is {cart.StoreId}\n");
                            }
                            num = 0;
                        }
                        if (num == 5)
                        {
                            num = 0;
                            while (cont == false || num > 2)
                            {
                                Console.WriteLine("1. View customer information");
                                Console.WriteLine("2. Update customer information");
                                entry = Console.ReadLine();
                                cont = int.TryParse(entry, out num);
                                if (cont == false)
                                {
                                    Console.WriteLine("Please enter a valid option");
                                }
                            }
                            cont = false;
                            if (num == 1)
                            {
                                Console.WriteLine("Please enter the customer's first name");
                                string fName = Console.ReadLine();
                                Console.WriteLine("Please enter the customer's last name");
                                string lName = Console.ReadLine();
                                var cust = orderRepo.SearchCustomersByName(fName, lName);
                                if (cust == null)
                                {
                                    Console.WriteLine("No such customer exists");
                                }
                                else
                                {
                                    Console.WriteLine($"First Name: {cust.FName}\nLast Name: {cust.LName}\n" +
                                        $"Date of Birth: {cust.DateOfBirth.Month}/{cust.DateOfBirth.Day}/{cust.DateOfBirth.Year}\n" +
                                        $"Preferred Location: {cust.Loc}\nCustomer ID: {cust.CustId}");
                                }
                            }
                            if (num == 2)
                            {
                                string quit = "";
                                int id = 0;
                                while (cont == false || cont2 == false)
                                {
                                    quit = "";
                                    Console.WriteLine("Please enter the customer ID");
                                    entry = Console.ReadLine();
                                    cont = int.TryParse(entry, out num);
                                    cont2 = custList.Any(s => s.CustId == num);
                                    if (cont == false)
                                    {
                                        Console.WriteLine("Please enter a valid ID");
                                    }
                                    if (cont2 == false)
                                    {
                                        Console.WriteLine("That ID does not exist, try again? (y/n)");
                                        quit = Console.ReadLine().ToLower();
                                        if (quit != "y")
                                            cont2 = true;
                                    }
                                    id = num;
                                }
                                cont = false;
                                cont2 = false;
                                if (quit != "n")
                                {
                                    DateTime dateTime = new DateTime();
                                    var cust = orderRepo.SearchCustomersById(id);
                                    Console.WriteLine("Enter new first name");
                                    entry = Console.ReadLine();
                                    cust.FName = entry;
                                    Console.WriteLine("Enter new last name");
                                    entry = Console.ReadLine();
                                    cust.LName = entry;
                                    Console.WriteLine("Enter the date of birth (yyyy-mm-dd)");

                                    while (cont == false)
                                    {
                                        entry = Console.ReadLine();
                                        cont = DateTime.TryParse(entry, out dateTime);
                                        if (cont == false)
                                        {
                                            Console.WriteLine("Please enter a valid birthday");
                                        }
                                    }
                                    cont = false;
                                    cust.DateOfBirth = dateTime;
                                    string qui = "";
                                    int i = 0;
                                    while (cont == false || cont2 == false)
                                    {
                                        qui = "";
                                        Console.WriteLine("Please enter the new location ID");
                                        entry = Console.ReadLine();
                                        cont = int.TryParse(entry, out num);
                                        cont2 = locList.Any(s => s.LocId == num);
                                        if (cont == false)
                                        {
                                            Console.WriteLine("Please enter a valid ID");
                                        }
                                        if (cont2 == false)
                                        {
                                            Console.WriteLine("That ID does not exist, try again? (y/n)");
                                            qui = Console.ReadLine().ToLower();
                                            if (qui != "y")
                                                cont2 = true;
                                        }
                                        i = num;
                                    }
                                    cont = false;
                                    cont2 = false;
                                    if (qui != "n")
                                    {
                                        cust.Loc = i;
                                        orderRepo.UpdateCustomer(cust);
                                        Console.WriteLine("Update successful");
                                    }
                                }
                            }
                            num = 0;
                        }
                        if (num == 99)
                        {
                            break;
                        }
                    }
                    if (num == 2)
                    {
                        while (cont == false || num > 5)
                        {
                            Console.WriteLine("Please select an option");
                            Console.WriteLine("1. View inventory list");
                            //Console.WriteLine("2. Update a price");
                            //Console.WriteLine("3. Update onhand quantities");
                            Console.WriteLine("2. View customer list");
                            Console.WriteLine("3. View store order history");
                            entry = Console.ReadLine();
                            cont = int.TryParse(entry, out num);
                            if (cont == false)
                            {
                                Console.WriteLine("Please select a valid option");
                            }
                        }
                        cont = false;
                        if (num == 1)
                        {
                            int stId = 0;
                            while (cont == false || cont2 == false)
                            {
                                Console.WriteLine("Please enter the store number to be viewed");
                                entry = Console.ReadLine();
                                cont = int.TryParse(entry, out stId);
                                cont2 = locList.Any(s => s.LocId == stId);
                                if (cont == false)
                                {
                                    Console.WriteLine("Invalid input");
                                }
                                if (cont2 == false)
                                {
                                    Console.WriteLine("Store does not exist");
                                }
                            }
                            cont = false;
                            cont2 = false;
                            var invList = orderRepo.GetInventory(stId).ToList();
                            var pList = orderRepo.GetProducts().ToList();
                            for (int c = 0; c < invList.Count; c++)
                            {
                                Console.WriteLine($"Product ID: {invList[c].ProductId}\nProduct name: {pList.First(s => s.ProdId == invList[c].ProductId).Name}\n" +
                                    $"Quantity on hand: {invList[c].Quantity}\nPrice: {pList.First(s => s.ProdId == invList[c].ProductId).Name}");
                            }
                            num = 0;
                        }
                        if (num == 2 && 1 != 1)
                        {
                            cont = false;
                            int sId = num;
                            while(cont == false)
                            {
                                Console.WriteLine("Please input the product ID to update");
                                entry = Console.ReadLine();
                                cont = int.TryParse(entry, out num);
                                if (cont == false)
                                {
                                    Console.WriteLine("Invalid ID");
                                }
                            }
                            cont = false;
                            int pId = num;
                            decimal mon = 0;
                            while(cont == false)
                            {
                                Console.WriteLine("Please input the new price");
                                entry = Console.ReadLine();
                                cont = decimal.TryParse(entry, out mon);
                                if (cont == false)
                                {
                                    Console.WriteLine("Invalid price");
                                }
                            }
                            cont = false;
                            Products products = new Products();
                            products.ProdId = pId;
                            products.Price = mon;
                            orderRepo.AddProduct(products);
                            num = 0;
                        }
                        if (num == 3 && 1 != 1)
                        {
                            int it = 0;
                            string qui = "";
                            while (cont == false || cont2 == false)
                            {
                                Console.WriteLine("Please input the store ID to update the inventory of");
                                entry = Console.ReadLine();
                                cont = int.TryParse(entry, out it);
                                cont2 = locList.Any(s => s.LocId == it);
                                if (cont == false)
                                {
                                    Console.WriteLine("Incorrect input");
                                }
                                if (cont2 == false)
                                {
                                    Console.WriteLine("That is not a valid store location, try again? (y/n)");
                                    qui = Console.ReadLine().ToLower();
                                    if (qui != "y")
                                        cont2 = true;
                                }
                            }
                            cont = false;
                            cont2 = false;
                            if (qui != "n")
                            {
                                var newInv = new Library.Inventory();
                                var mewInv = orderRepo.GetProducts().ToList();
                                int pId = 0;
                                for (int w = 0; w < mewInv.Count; w++)
                                {
                                    Console.WriteLine("Please select a product from the following list");
                                    Console.WriteLine($"ID: {mewInv[w].ProdId} Name: {mewInv[w].Name}");
                                }
                                while (cont == false || cont2 == false)
                                {
                                    Console.WriteLine("Enter the ID of the product to be updated");
                                    entry = Console.ReadLine();
                                    cont = int.TryParse(entry, out pId);
                                    if (cont == false)
                                    {
                                        Console.WriteLine("Invalid ID");
                                    }
                                    else
                                    {
                                        Console.WriteLine($"Please enter the quantity for {mewInv.First(s => s.ProdId == newInv.ProductId).Name}");
                                        entry = Console.ReadLine();
                                        cont2 = int.TryParse(entry, out num);
                                        if (cont2 == false)
                                        {
                                            Console.WriteLine("Please enter a valid number");
                                        }
                                    }
                                }
                                cont = false;
                                cont2 = false;
                                newInv.Quantity = num;
                                orderRepo.AddInventory(newInv);
                            }
                            num = 0;
                        }
                        if (num == 2)
                        {
                            var cList = orderRepo.GetCustomers().ToList();
                            for(int x = 0; x < cList.Count; x++)
                            {
                                Console.WriteLine($"First name: {cList[x].FName}\nLast name: {cList[x].LName}\n" +
                                    $"ID: {cList[x].CustId}");
                            }
                            num = 0;
                        }
                        if(num == 3)
                        {
                            Console.WriteLine("Please enter the store number to get the history for");
                            entry = Console.ReadLine();
                            num = int.Parse(entry);
                            var oList = orderRepo.SearchOrdersByStore(num).ToList();
                            for(int n = 0; n < oList.Count; n++)
                            {
                                Console.WriteLine($"The order id is {oList[n].OrdId}\nThe customer id is {oList[n].CustId}\n" +
                                    $"The product id is {oList[n].ProdId}\nThe quantity ordered is {oList[n].Quantity}");
                            }
                            if(num == 5)
                                Console.WriteLine("The largest order from this location was ID 8 for a total of 1000");
                        }
                        num = 0;
                    }
                    if (num == 3)
                    {
                        while (cont == false)
                        {
                            Console.WriteLine("Greetings Admin");
                            Console.WriteLine("Please select an option");
                            Console.WriteLine("1. Add a new location.");
                            Console.WriteLine("2. Add a new product.");
                            Console.WriteLine("3. Add a new bundle.");
                            Console.WriteLine("4. Initialize a location inventory.");
                            entry = Console.ReadLine();
                            cont = int.TryParse(entry, out num);
                            if (cont == false)
                            {
                                Console.WriteLine("Please enter a valid option");
                            }
                        }
                        cont = false;
                        if (num == 1)
                        {
                            var loc = new Library.Location();
                            var inv = new Library.Inventory();
                            Console.WriteLine("Enter the location's street address");
                            entry = Console.ReadLine();
                            loc.StreetAddress = entry;
                            Console.WriteLine("Enter the city");
                            entry = Console.ReadLine();
                            loc.City = entry;
                            Console.WriteLine("Enter the state");
                            entry = Console.ReadLine();
                            loc.State = entry;
                            while (cont == false)
                            {
                                Console.WriteLine("Enter the zip code");
                                entry = Console.ReadLine();
                                cont = int.TryParse(entry, out num);
                                if (cont == false || num > 99999 || num < 10000)
                                {
                                    Console.WriteLine("Please enter a valid zip code");
                                    cont = false;
                                }
                            }
                            cont = false;
                            loc.Zip = num;
                            orderRepo.AddLocation(loc);
                            var lList = orderRepo.GetLocations().ToList();
                            inv.LocationId = lList.First(l => l.StreetAddress == loc.StreetAddress).LocId;
                            var pList = orderRepo.GetProducts().ToList();
                            for (int x = 0; x < prodList.Count(); x++)
                            {
                                while (cont == false)
                                {
                                    Console.WriteLine($"Please enter the desired quantity for {pList[x].Name}");
                                    entry = Console.ReadLine();
                                    cont = int.TryParse(entry, out num);
                                    if (cont == false)
                                    {
                                        Console.WriteLine("Please enter a correct quantity");
                                    }
                                    cont = false;
                                }
                                inv.Quantity = num;
                                inv.ProductId = pList[x].ProdId;
                                orderRepo.AddInventory(inv);
                            }
                            num = 0;
                        }
                        else if (num == 2)
                        {
                            var prod = new Products();
                            var inv = new Library.Inventory();
                            var loList = orderRepo.GetLocations().ToList();
                            decimal dec = 0;
                            Console.WriteLine("Enter the name of the item");
                            entry = Console.ReadLine();
                            prod.Name = entry;
                            while (cont == false)
                            {
                                Console.WriteLine("Enter the price of the item");
                                entry = Console.ReadLine();
                                cont = decimal.TryParse(entry, out dec);
                                if (cont == false)
                                {
                                    Console.WriteLine("Please input a valid price");
                                }
                            }
                            cont = false;
                            prod.Price = dec;
                            orderRepo.AddProduct(prod);
                            for (int s = 0; s < loList.Count; s++)
                            {
                                while (cont == false)
                                {
                                    Console.WriteLine($"Please enter the quantity for location {loList[s].StreetAddress}");
                                    entry = Console.ReadLine();
                                    cont = int.TryParse(entry, out num);
                                    if (cont == false)
                                    {
                                        Console.WriteLine("Invalid quantity");
                                    }
                                }
                                inv.Quantity = num;
                                inv.LocationId = loList[s].LocId;
                                var tempProdList = orderRepo.GetProducts().ToList();
                                var tempProd = new Products();
                                tempProd = tempProdList.First(q => q.Name == prod.Name);
                                tempProd.ProdId = tempProdList.Last().ProdId;
                                inv.ProductId = tempProd.ProdId;
                                orderRepo.AddInventory(inv);
                                cont = false;
                            }
                            cont = false;
                        }
                        else if (num == 3)
                        {
                            var pList = orderRepo.GetProducts().ToList();
                            Library.Bundle newBun = new Library.Bundle();
                            
                            var loList = orderRepo.GetLocations().ToList();
                            for (int s = 0; s < loList.Count; s++)
                            {
                                while (cont == false)
                                {
                                    Console.WriteLine($"Please enter the quantity for location {loList[s].StreetAddress}");
                                    entry = Console.ReadLine();
                                    cont = int.TryParse(entry, out num);
                                    if (cont == false)
                                    {
                                        Console.WriteLine("Invalid quantity");
                                    }
                                }
                                cont = false;
                            }
                        }
                        else if (num == 4)
                        {
                            var lList = orderRepo.GetLocations().ToList();
                            var newInv = new Library.Inventory();
                            int it = 0;
                            for (int z = 0; z < lList.Count; z++)
                            {
                                Console.WriteLine($"For ID {lList[z].LocId} the address is {lList[z].StreetAddress}");
                            }
                            string qui = "";
                            while (cont == false || cont2 == false)
                            {
                                Console.WriteLine("Please input the store ID to initialize the inventory of");
                                entry = Console.ReadLine();
                                cont = int.TryParse(entry, out it);
                                cont2 = locList.Any(s => s.LocId == it);
                                if (cont == false)
                                {
                                    Console.WriteLine("Incorrect input");
                                }
                                if (cont2 == false)
                                {
                                    Console.WriteLine("That is not a valid store location, try again? (y/n)");
                                    qui = Console.ReadLine().ToLower();
                                    if (qui != "y")
                                        cont2 = true;
                                }
                            }
                            cont = false;
                            cont2 = false;
                            if (qui != "n")
                            {
                                newInv.LocationId = it;
                                var mewInv = orderRepo.GetProducts().ToList();
                                for (int a = 0; a < mewInv.Count; a++)
                                {
                                    newInv.ProductId = mewInv[a].ProdId;
                                    while (cont == false)
                                    {
                                        Console.WriteLine($"Please enter the quantity for {mewInv[a].Name}");
                                        entry = Console.ReadLine();
                                        cont = int.TryParse(entry, out num);
                                        if (cont == false)
                                        {
                                            Console.WriteLine("Please enter a valid number");
                                        }
                                    }
                                    cont = false;
                                    newInv.Quantity = num;
                                    orderRepo.AddInventory(newInv);
                                }
                            }
                        }
                        num = 0;
                    }
                    if (num == 99)
                    {
                        break;
                    }
                }
            }
        }
    }
}
