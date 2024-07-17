using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using GrandTravelPackages.Models;

namespace GrandTravelPackages.ViewModels
{
    public class FeedbackAddViewModel
    {
        public int FeedbackId { get; set; }
        public int OrderId { get; set; }
        public int PackageId { get; set; }
        //public string PackName { get; set; }
        //public string PackLocation { get; set; }
        ////public decimal PackPrice { get; set; }
        //public string PackDescription { get; set; }
        //public string PackImage { get; set; }
        public string OrdFeedback { get; set; }
        public string UserId { get; set; }
        public Package GetPackage { get; set; }
    }
}
