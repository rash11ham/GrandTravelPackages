using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GrandTravelPackages.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        public int PackagId { get; set; }
        public decimal Price { get; set; }
        public string Status { get; set; }
        [Required(ErrorMessage = "Order Date is Required")]
        public DateTime Date { get; set; }

        [Required]
        public string UserId { get; set; }

    }
}
