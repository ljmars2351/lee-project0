using System;
using System.Collections.Generic;

namespace OrderSystem.DataAccess
{
    public partial class Customer
    {
        public Customer()
        {
            Cart = new HashSet<Cart>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int? PrefLoc { get; set; }

        public virtual Location PrefLocNavigation { get; set; }
        public virtual ICollection<Cart> Cart { get; set; }
    }
}
