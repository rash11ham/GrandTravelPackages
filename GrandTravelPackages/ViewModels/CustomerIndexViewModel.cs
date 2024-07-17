using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using GrandTravelPackages.Models;

namespace GrandTravelPackages.ViewModels
{
    public class CustomerIndexViewModel
    {

        public string UserId { get; set; }
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
        public int TotalPackageFound { get; set; }
        public PackageStatusEnum PackageStatus { get; set; }

        public IEnumerable<Package> GetPackage { get; set; }
    }
}
