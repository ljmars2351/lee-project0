using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OrderSystem.WebApp.ViewModels
{
    public class ProductViewModel
    {
        [Display(Name = "Product ID")]
        public int Id { get; set; }

        [Display(Name = "Product Name")]
        public string ProdName { get; set; }

        [Display(Name = "Price")]
        public decimal Price { get; set; }
    }
}
