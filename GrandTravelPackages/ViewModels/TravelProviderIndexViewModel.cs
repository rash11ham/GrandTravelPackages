using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GrandTravelPackages.Models;

namespace GrandTravelPackages.ViewModels
{
    public class TravelProviderIndexViewModel
    {
        public IEnumerable<Package> GetPackage { get; set; }
        public int total_packages { get; set; }
    }
}
