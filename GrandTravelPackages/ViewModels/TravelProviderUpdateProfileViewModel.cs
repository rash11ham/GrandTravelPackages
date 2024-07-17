using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GrandTravelPackages.ViewModels
{
    public class TravelProviderUpdateProfileViewModel
    {
       
        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }
       
        [Display(Name = "User Name")]
        public string UserName { get; set; }
       
       
        public string Password { get; set; }
       [Display(Name = "Website")]
        public string WebSite { get; set; }
       
        public string Phone { get; set; }
     
        public string Email { get; set; }
        public string Address { get; set; }
    }
}
