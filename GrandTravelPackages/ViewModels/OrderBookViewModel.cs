using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GrandTravelPackages.ViewModels
{
    public class OrderBookViewModel
    {
        [Key]
       // public int OrderId { get; set; }
        public int PackagId { get; set; }

        public string PackName { get; set; }
        public string PackLocation { get; set; }
        public decimal PackPrice { get; set; }
        public string PackDescription { get; set; }
        public string PackImage { get; set; }
      //  public string Status { get; set; }

        public DateTime OrderDate { get; set; }

        public string UserId { get; set; }
    }
}
