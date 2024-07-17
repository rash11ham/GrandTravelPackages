using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GrandTravelPackages.Models;
using GrandTravelPackages.Services;
using GrandTravelPackages.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GrandTravelPackages.Controllers
{
    public class HomeController : Controller
    {
        #region Controllers Constructor INjectors


        private IDataService<TravelProvider> _providerDataService;
        private IDataService<Package> _packageDataService;
        private IHostingEnvironment _environment;
        private UserManager<IdentityUser> _userManagerService;
        private RoleManager<IdentityRole> _roleManagerService;

        public HomeController(IDataService<TravelProvider> providerDataService,
            IDataService<Package> packageDataService,
            IHostingEnvironment hostingEnvironment,
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _providerDataService = providerDataService;
            _packageDataService = packageDataService;
            _environment = hostingEnvironment;
            _userManagerService = userManager;
            _roleManagerService = roleManager;
        }
        #endregion
        #region Search


        //[HttpGet]
        //[AllowAnonymous]
        //public IActionResult Index(string SearchLocation, decimal MinPrice, decimal MaxPrice)
        //{
        //    //ModelState.Clear();
        //    IEnumerable<Package> package;
        //    //IdentityUser user = await _userManagerService.FindByNameAsync(User.Identity.Name);
        //    ViewData["PackLocation"] = SearchLocation;
        //    ViewData["MinPrice"] = MinPrice;
        //    ViewData["MaxPrice"] = MaxPrice;

        //    package = _packageDataService.GetAll().Where(p => p.PackageStatus == 0);
        //    // 0 comes from the Enum: Active || to display active Packages for the loged in customer

        //    if (!String.IsNullOrEmpty(SearchLocation))
        //    {

        //        if (MaxPrice > 0 && MinPrice > 0)
        //        {

        //            package = _packageDataService.GetAll()
        //            .Where(p => p.PackLocation.ToUpper().Trim().Contains(SearchLocation.ToUpper().Trim()) 
        //            && p.PackPrice >= MinPrice && p.PackPrice <= MaxPrice);
        //            //Add error message to the model stage, THIS METHOD IS PREFERRED !!!.

        //        }
        //        else if(MaxPrice == 0 && MinPrice == 0)
        //        {
        //            package = _packageDataService.GetAll()
        //           .Where(p => p.PackLocation.ToUpper().Trim().Contains(SearchLocation.ToUpper().Trim()));
        //        }
        //        else
        //        {
        //            return RedirectToAction("Error","Home"); 
        //        }


        //    }
        //    else
        //    {

        //        if (MaxPrice > 0 && MinPrice > 0)
        //        {
        //            package = _packageDataService.GetAll()
        //               .Where(p => p.PackPrice >= MinPrice && p.PackPrice <= MaxPrice);
        //        }
        //        else if (MaxPrice > 0)
        //        {
        //            package = _packageDataService.GetAll()
        //               .Where(p => p.PackPrice <= MaxPrice);
        //        }
        //        else if (MinPrice > 0)
        //        {
        //            package = _packageDataService.GetAll()
        //               .Where(p => p.PackPrice >= MinPrice);
        //        }


        //        else
        //        {
        //            package = _packageDataService.GetAll().Where(p => p.PackageStatus == 0);

        //        }

        //    }
        //    if (MinPrice > MaxPrice)
        //    {
        //        //Add error message to the model stage, THIS METHOD IS PREFERRED !!!.
        //        ViewBag.MyMessage = "PackageSearchError Mininum Price Filter must be less than Maximum Price";

        //    }

        //    HomeIndexViewModel vm = new HomeIndexViewModel
        //    {

        //        Packages = package,
        //        TotalPackageFound = 0


        //    };




        //    return View(vm);

        //}



        [HttpGet]
        [AllowAnonymous]
        public IActionResult Index(string SearchLocation, decimal MinPrice, decimal MaxPrice)
        {
            //ModelState.Clear();
            IEnumerable<Package> package = null;
            //IdentityUser user = await _userManagerService.FindByNameAsync(User.Identity.Name);
            ViewData["PackLocation"] = SearchLocation;
            ViewData["MinPrice"] = MinPrice;
            ViewData["MaxPrice"] = MaxPrice;

            //package = _packageDataService.GetAll().Where(p => p.PackageStatus == 0);
            // 0 comes from the Enum: Active || to display active Packages for the loged in customer

            if (SearchLocation != null)
            {

                if (MaxPrice > 0 && MinPrice > 0)
                {
                   
                    package = _packageDataService.GetAll()
                    .Where(p => p.PackLocation.ToUpper().Trim().Contains(SearchLocation.ToUpper().Trim())
                    && p.PackPrice >= MinPrice && p.PackPrice <= MaxPrice && p.PackageStatus == 0);

                    if (package.Count() <= 0)
                    {
                       
                        ViewBag.MyMessage = "no result found! Please try different location";
                    }
                }
                else
                {
                    package = _packageDataService.GetAll()
                              .Where(p => p.PackLocation.ToUpper().Trim()
                              .Contains(SearchLocation.ToUpper().Trim()) && p.PackageStatus == 0);
                    if (package.Count()<=0)
                    {
                        
                        ViewBag.MyMessage = "no result found! Please try different location";
                    }


                }

            }

            else
            {

                if (MaxPrice > 0 && MinPrice > 0)
                {
                 
                    package = _packageDataService.GetAll()
                       .Where(p => p.PackPrice >= MinPrice
                       && p.PackPrice <= MaxPrice && p.PackageStatus == 0);
                }
                else if (MaxPrice > 0)
                {
                   
                    package = _packageDataService.GetAll()
                       .Where(p => p.PackPrice <= MaxPrice && p.PackageStatus == 0);
                }
                else if (MinPrice > 0)
                {
                    
                    package = _packageDataService.GetAll()
                       .Where(p => p.PackPrice >= MinPrice && p.PackageStatus == 0);
                }


                else
                {
                    
                    package = _packageDataService.GetAll().Where(p => p.PackageStatus == 0);

                }

            }
            if (MinPrice > MaxPrice && MaxPrice > 0)
            {
                
                //Add error message to the model stage, THIS METHOD IS PREFERRED !!!.
                ViewBag.MyMessage = "Minimum price must be less than maximum price";

            }


            HomeIndexViewModel vm = new HomeIndexViewModel
            {

                Packages = package,
                TotalPackageFound = 0


            };


            ViewData["PackLocation"] = "";
            ViewData["MinPrice"] = "";
            ViewData["MaxPrice"] = "";

            return View(vm);

        }
        #endregion
        #region PrivacyPolicy

        public IActionResult PriPolicy()
        {
            return View();
        }
        #endregion

        #region terms of use

        public IActionResult TOfUse()
        {
            return View();
        }
        #endregion


        #region Company Background

        public IActionResult ComBg()
        {
            return View();
        }
        #endregion

        #region contact

        public IActionResult Contact()
        {
            return View();
        }
        #endregion


        #region Error

        public IActionResult Error()
        {

            return View();
        }
        #endregion


    }
}