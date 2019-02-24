using System;
using System.Collections.Generic;
using System.Text;

namespace OrderSystem.Library.Layouts
{
    public class Location
    {
        public int LocId { get; set; }

        public string StreetAddress { get; set; }

        public string City { get; set; }

        public int Zip { get; set; }

        public string State { get; set; }

        public List<Products> prodList = new List<Products>();
    }
}
