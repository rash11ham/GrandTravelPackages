using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GrandTravelPackages.ViewModels
{
    public class TravelProviderRegisterViewModel
    {
        public string UserId { get; set; }
        [Required]
        [Display(Name = "Company Name")]
        [MaxLength(256), MinLength(3)]
        public string CompanyName { get; set; }
        [Required]
        [Display(Name = "Website")]
        public string WebSite { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string Address { get; set; }
        [Required]
        [MaxLength(100), MinLength(6)]
        [Display(Name = "User Name")]
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
