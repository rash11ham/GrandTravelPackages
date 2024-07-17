using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GrandTravelPackages.ViewModels
{
    public class CustomerRegisterViewModel
    {
        

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Display(Name = "Profile Photo")]
        public string ProfilePhoto { get; set; }

        [Required]
        [Display(Name = "User Name")]
        [MaxLength(100), MinLength(6)]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [MaxLength(100), MinLength(8)]
        public string Password { get; set; }
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }




    }
}
