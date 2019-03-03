using System;
using Microsoft.EntityFrameworkCore;
using OrderSystem.Library;
using OrderSystem.DataAccess;

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
                    while (cont == false)
                    {
                        Console.WriteLine("Please identify the authorization of the user, enter 99 to quit at anytime.");
                        Console.WriteLine("1. Customer");
                        Console.WriteLine("2. Employee");
                        Console.WriteLine("3. Administrator");
                        entry = Console.ReadLine();
                        cont = int.TryParse(entry, out num);
                        if(cont == false)
                        {
                            Console.WriteLine("Please enter a valid number.");
                        }
                    }
                    cont = false;
                    if (num == 1)
                    {
                        while (cont == false)
                        {
                            Console.WriteLine("1. Create a new account");
                            Console.WriteLine("2. Create a new order");
                            Console.WriteLine("3. Change preferred location");
                            Console.WriteLine("4. View past orders");
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
                                while (cont2 == false)
                                {
                                    Console.WriteLine("Enter the first name.");
                                    entry = Console.ReadLine();
                                    newCust.FName = entry;
                                    Console.WriteLine("Enter the last name");
                                    entry = Console.ReadLine();
                                    newCust.LName = entry;
                                    Console.WriteLine("Enter the birthday");
                                    entry = Console.ReadLine();
                                    cont2 = DateTime.TryParse(entry, out dob);
                                    if (cont2 == false)
                                    {
                                        Console.WriteLine("Please enter a valid birthday.");
                                    }
                                    newCust.DateOfBirth = dob;
                                }
                                Console.WriteLine("Enter the preferred location (default is 1)");
                                entry = Console.ReadLine();
                                cont = int.TryParse(entry, out num);
                                if (cont == false)
                                {
                                    Console.WriteLine("Please enter a valid number.");
                                }
                                newCust.Loc = num;
                                orderRepo.AddCustomer(newCust);
                            }
                            cont = false;
                            cont2 = false;
                        }
                        if (num == 99)
                        {
                            break;
                        }
                    }
                    if(num == 2)
                    {
                        var newOrd = new Order();
                        DateTime time = new DateTime();
                        while(cont == false)
                        {

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
