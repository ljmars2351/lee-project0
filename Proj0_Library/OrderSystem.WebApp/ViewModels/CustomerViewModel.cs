using OrderSystem.Library;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using OrderSystem.DataAccess;

namespace OrderSystem.WebApp.ViewModels
{
    public class CustomerViewModel
    {
        [Display(Name = "Customer ID")]
        public int Id { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Display(Name = "Preferred Location")]
        public Library.Location PrefLocNavigation { get; set; }

        [Display(Name = "Preferred Location")]
        public List<Library.Location> PrefLoc { get; set; }
    }
}
