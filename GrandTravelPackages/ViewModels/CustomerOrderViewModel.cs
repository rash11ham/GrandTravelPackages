using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GrandTravelPackages.Models;

namespace GrandTravelPackages.ViewModels
{
    public class CustomerOrderViewModel
    {
        public IEnumerable<Order> GetOrders { get; set; }
       // public Order OrderedPackage { get; set; }
        public IEnumerable<Package> GetPackage { get; set; }
        // public string ProfilePhoto { get; set; }
        public Customer GetCustomer { get; set; }


    }
}
