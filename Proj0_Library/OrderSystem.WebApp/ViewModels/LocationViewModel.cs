using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OrderSystem.WebApp.ViewModels
{
    public class LocationViewModel
    {
        [Display(Name = "Location ID")]
        public int LocationId { get; set; }

        [Display(Name = "Address")]
        public string Address { get; set; }

        [Display(Name = "City")]
        public string City { get; set; }

        [Display(Name = "State")]
        public string State { get; set; }

        [Display(Name = "Zip")]
        public int Zip { get; set; }

        public List<Library.Products> Prod { get; set; }

        [Display(Name = "Product Name")]
        public string ProductName;

        public Library.Inventory Inv { get; set; }

        [Display(Name ="Inventory")]
        public List<Library.Inventory> StoreInv { get; set; }

        public List<Library.Inventory> Inventory { get; set; }
    }
}
