using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GrandTravelPackages.ViewModels
{
    public class TravelProviderPasswordUpdateViewModel
    {
        [Required]
        [Display(Name = "User Name")]
        public string UserName { get; set; }


        [Required(ErrorMessage = "Old Password Required")]
        [DataType(DataType.Password)]
        [Display(Name = "Old Password")]
        public string OldPassword { get; set; }


        [Required(ErrorMessage = "New Password Required")]
        [DataType(DataType.Password)]
        [MaxLength(100), MinLength(8)]
        [Display(Name = "New Password")]
        public string NewPassword { get; set; }


        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("NewPassword")]
        public string ConfirmPassword { get; set; }

        public string StatusMessage { get; set; }
    }
}
