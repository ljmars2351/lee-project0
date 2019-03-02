using System;
using OrderSystem.Library.Storage;
using OrderSystem.Library.Layouts;
using Microsoft.EntityFrameworkCore;
using OrderSystem.Library;

namespace OrderSystem.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            string entry;
            int num = 0;

            /*var optionsBuilder = new DbContextOptionsBuilder<Proj0Context>();
            optionsBuilder.UseSqlServer(SecretConfig.ConnectionString);
            var options = optionsBuilder.Options;
            var dbContext = new Proj0Context(options);
            OrderDbHelper.ConnectToDb(dbContext);


            using (dbContext)*/
            {
                while (num != 99)
                {
                    Console.WriteLine("Please identify the authorization of the user, enter 99 to quit at anytime.");
                    Console.WriteLine("1. Customer");
                    Console.WriteLine("2. Employee");
                    Console.WriteLine("3. Administrator");
                    entry = Console.ReadLine();
                    if (int.TryParse(entry, out num) == false)
                    {
                        Console.WriteLine("Please enter a valid number.");
                    }
                    if (num == 1)
                    {
                        Console.WriteLine("1. Create a new account");
                        Console.WriteLine("2. Create a new order");
                        Console.WriteLine("3. Change preferred location");
                        Console.WriteLine("4. View a past order");
                        if (int.TryParse(entry, out num) == false)
                        {
                            Console.WriteLine("Please enter a valid number.");
                        }
                        if (num == 1)
                        {
                            Console.WriteLine("Enter the first name.");
                            entry = Console.ReadLine();
                            //cust.FName = entry;
                            Console.WriteLine("Enter the last name");
                            entry = Console.ReadLine();
                            //cust.LName = entry;
                            Console.WriteLine("Enter the birthday");
                            entry = Console.ReadLine();
                            if (int.TryParse(entry, out num) == false)
                            {
                                Console.WriteLine("Please enter a valid number.");
                            }
                        }
                        if (num == 99)
                        {
                            break;
                        }
                    }
                    Console.WriteLine("1.");
                    Console.WriteLine("2. Add something to the system.");
                    Console.WriteLine("3. Edit an existing entry.");
                    Console.WriteLine("4. View previous entries.");
                    if (num == 99)
                    {
                        break;
                    }
                }
            }
        }
    }
}
