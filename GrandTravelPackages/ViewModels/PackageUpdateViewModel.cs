using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using GrandTravelPackages.Models;

namespace GrandTravelPackages.ViewModels
{
    public class PackageUpdateViewModel
    {
        public string UserId { get; set; }
        public int PackageId { get; set; }
        [Display(Name = "Package Name")]
        public string PackName { get; set; }
        [Display(Name = "Package Location")]
        public string PackLocation { get; set; }
        [Display(Name = "Package Price")]
        public decimal PackPrice { get; set; }
        [Display(Name = "Package Description")]
        public string PackDescription { get; set; }
        [Display(Name = "Package Image")]
        public string PackImage { get; set; }
        public PackageStatusEnum PackageStatus { get; set; }
    }
}
