using System;
using Microsoft.EntityFrameworkCore;
using OrderSystem.Library;
using OrderSystem.DataAccess;
using System.Linq;

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
                        if(cont == false || num > 3 && num != 99)
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
                            Console.WriteLine("Exiting will be disabled, do you wish to continue? (y/n)");
                            entry = Console.ReadLine().ToLower();
                            if (entry == "y")
                            {
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
                                Console.WriteLine("Exiting re-enabled\n");
                            }
                            else
                                Console.WriteLine("Exiting customer creation\n");
                            cont = false;
                        }
                        if (num == 2)
                        {
                            var newOrd = new Order();
                            DateTime time = new DateTime();
                            var cust = new Library.Customer();
                            Console.WriteLine("Exiting will be disabled, do you wish to continue? (y/n)");
                            entry = Console.ReadLine().ToLower();
                            if (entry == "y")
                            {
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
                                            cust.DateOfBirth = time;
                                            cust.Loc = 3;
                                            orderRepo.AddCustomer(cust);
                                        }
                                        if (num == 2)
                                        {
                                            cont = false;
                                        }
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
                                cont = false;
                                newOrd.StoreId = num;
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
                                newOrd.Quantity = num;
                                time = DateTime.Now;
                                newOrd.OrdTIme = time;
                                orderRepo.AddOrder(newOrd);
                                Console.WriteLine("Exiting re-enabled");
                            }
                            else
                                Console.WriteLine("Exiting order creation");
                        }
                        if(num == 3)
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
                            while(cont == false)
                            {
                                Console.WriteLine("Please select from the current list of locations");
                                var list = orderRepo.GetLocations().ToList();
                                for(int x = 0; x < list.Count; x++)
                                {
                                    var loc = list[x];

                                    Console.WriteLine($"Location ID: {loc.LocId}\nLocation Address: {loc.StreetAddress} {loc.City} {loc.State}\n");
                                }
                                Console.WriteLine("Enter the new location ID");
                                entry = Console.ReadLine();
                                cont = int.TryParse(entry, out num);
                                if(cont == false)
                                {
                                    Console.WriteLine("Please enter a valid ID");
                                }
                            }
                            cust.Loc = num;
                            orderRepo.UpdateLocation(cust);
                            cont = false;
                        }
                        if(num == 4)
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
                            for (int x = 0; x < list.Count; x++)
                            {
                                var cart = list[x];
                                var name = orderRepo.SearchCustomersById(cart.CustId);
                                var item = orderRepo.SearchProductsById(cart.ProdId);
                                Console.WriteLine($"The customer's name is {name.FName} {name.LName}\n" +
                                    $"The order ID is {cart.OrdId}\nThe ordered product name is {item.Name}\n" +
                                    $"The quantity ordered is {cart.Quantity}\nThe store's ID is {cart.StoreId}\n");
                            }
                        }
                        if(num == 5)
                        {
                            num = 0;
                            while(cont == false || num > 2)
                            {
                                Console.WriteLine("1. View customer information");
                                Console.WriteLine("2. Update customer information");
                                entry = Console.ReadLine();
                                cont = int.TryParse(entry, out num);
                                if(cont == false)
                                {
                                    Console.WriteLine("Please enter a valid option");
                                }
                            }
                            cont = false;
                            if(num == 1)
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
                                    if(cont == false)
                                    {
                                        Console.WriteLine("Please enter a valid ID");
                                    }
                                    if(cont2 == false)
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
                                if(quit != "y")
                                {
                                    var cust = orderRepo.SearchCustomersById(id);
                                    Console.WriteLine($"First Name: {cust.FName}\nLast Name: {cust.LName}\n" +
                                        $"Date of Birth: {cust.DateOfBirth.Month}/{cust.DateOfBirth.Day}/{cust.DateOfBirth.Year}\n" +
                                        $"Preferred Location: {cust.Loc}");
                                }
                            }
                            if(num == 2)
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
                                if (quit != "y")
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
                                    entry = Console.ReadLine();
                                    while(cont == false)
                                    {
                                        cont = DateTime.TryParse(entry, out dateTime);
                                        if (cont == false)
                                        {
                                            Console.WriteLine("Please enter a valid birthday");
                                        }
                                    }
                                    cont = false;
                                    cust.DateOfBirth = dateTime;
                                    Console.WriteLine("Enter new preferred location ID");
                                    entry = Console.ReadLine();
                                    string qui = "";
                                    int i = 0;
                                    while (cont == false || cont2 == false)
                                    {
                                        qui = "";
                                        Console.WriteLine("Please enter the customer ID");
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
                                            quit = Console.ReadLine().ToLower();
                                            if (qui != "y")
                                                cont2 = true;
                                        }
                                        i = num;
                                    }
                                    cont = false;
                                    cont2 = false;
                                    if (qui != "y")
                                    {
                                        cust.Loc = i;
                                        orderRepo.UpdateCustomer(cust);
                                    }
                                }
                            }
                        }
                        if (num == 99)
                        {
                            break;
                        }
                    }
                    if(num == 2)
                    {
                        while(cont == false)
                        {
                            Console.WriteLine("Please select an option");
                            Console.WriteLine("1. Remove a customer");
                            Console.WriteLine("2. Update a price");
                            Console.WriteLine("3. Update onhand quantities");
                            Console.WriteLine("4. View customer list");
                            entry = Console.ReadLine();
                            cont = int.TryParse(entry, out num);
                            if (cont == false)
                            {
                                Console.WriteLine("Please select a valid option");
                            }
                        }
                    }
                    if(num == 3)
                    {
                        while (cont == false)
                        {
                            Console.WriteLine("Greetings Admin");
                            Console.WriteLine("Please select an option");
                            Console.WriteLine("1. Add a new location.");
                            Console.WriteLine("2. Add a new product.");
                            Console.WriteLine("3. Add a new bundle.");
                            entry = Console.ReadLine();
                            cont = int.TryParse(entry, out num);
                            if(cont == false)
                            {
                                Console.WriteLine("Please enter a valid option");
                            }
                        }
                        cont = false;
                        if(num == 1)
                        {
                            var loc = new Library.Location();
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
                        }
                        if(num == 2)
                        {
                            var prod = new Products();
                            decimal dec = 0;
                            Console.WriteLine("Enter the name of the item");
                            entry = Console.ReadLine();
                            prod.Name = entry;
                            while (cont == false)
                            {
                                Console.WriteLine("Enter the price of the item");
                                entry = Console.ReadLine();
                                cont = decimal.TryParse(entry, out dec);
                                if(cont == false)
                                {
                                    Console.WriteLine("Please input a valid price");
                                }
                            }
                            cont = false;
                            prod.Price = dec;
                            orderRepo.AddProduct(prod);
                        }
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
