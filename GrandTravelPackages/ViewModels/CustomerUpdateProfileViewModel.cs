using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GrandTravelPackages.ViewModels
{
    public class CustomerUpdateProfileViewModel
    {

        public string UserId { get; set; }
        [Display(Name = "User Name")]
        public string UserName { get; set; }
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        [Display(Name = "Profile Photo")]
        public string ProfilePhoto { get; set; }
    
    }
}
