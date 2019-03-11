using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OrderSystem.WebApp.ViewModels
{
    public class OrderViewModel
    {
        [Display(Name = "Order ID")]
        public int OrderId { get; set; }

        [Display(Name = "Customer ID")]
        public int CustomerId { get; set; }

        public Library.Customer Customer { get; set; }

        [Display(Name = "Location ID")]
        public int LocationId { get; set; }

        public Library.Location Location { get; set; }

        [Display(Name = "Product ID")]
        public int ProdId { get; set; }

        public Library.Products Prod { get; set; }

        [Display(Name = "Order Time")]
        [DataType(DataType.DateTime)]
        public DateTime OrdTime { get; set; }

        [Display(Name = "Quantity Ordered")]
        public int Quant { get; set; }

        [Display(Name = "Location List")]
        public List<Library.Location> LocList { get; set; }


    }
}
