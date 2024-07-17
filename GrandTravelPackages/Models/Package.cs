using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GrandTravelPackages.Models
{
    public enum PackageStatusEnum
    {
        Active,
        Deactive
    }
    public class Package
    {
        [Key]
        public int PackageId { get; set; }
        public string UserId { get; set; }
        public string PackName { get; set; }
        public string PackLocation { get; set; }
        public decimal PackPrice { get; set; }
        public string PackDescription { get; set; }
        public string PackImage { get; set; }
        
        public PackageStatusEnum PackageStatus { get; set; }

    }
}
