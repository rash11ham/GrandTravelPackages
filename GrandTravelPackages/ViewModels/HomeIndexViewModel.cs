using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using GrandTravelPackages.Models;

namespace GrandTravelPackages.ViewModels
{
    public class HomeIndexViewModel
    {
        
        public int PackageID { get; set; }
        public string PackName { get; set; }
        public string PackLocation { get; set; }
        public decimal PackPrice { get; set; }
        public string PackDescription { get; set; }
        public string PackImage { get; set; }
        [Required(ErrorMessage = "Enter a Package Location to Search")]
        public string SearchLocation { get; set; }
        public PackageStatusEnum PackageStatus { get; set; }
        public decimal MinPrice { get; set; }

        public decimal MaxPrice { get; set; }

        public int TotalPackageFound { get; set; }

        public IEnumerable<Package> Packages { get; set; }
    }
}
