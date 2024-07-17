using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using GrandTravelPackages.Models;
using GrandTravelPackages.Services;
using GrandTravelPackages.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GrandTravelPackages.Controllers
{
    #region Constructor & Injectors

    
    public class OrderController : Controller
    {
        private IDataService<TravelProvider> _providerDataService;
        private IDataService<Package> _packageDataService;
        private IHostingEnvironment _environment;
        private UserManager<IdentityUser> _userManagerService;
        private RoleManager<IdentityRole> _roleManagerService;
        private IDataService<Order> _orderDataService;
      
        public OrderController(IDataService<TravelProvider> providerDataService,
            IDataService<Package> packageDataService,
            IHostingEnvironment hostingEnvironment,
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IDataService<Order> orderDataService)
        {
            _providerDataService = providerDataService;
            _packageDataService = packageDataService;
            _environment = hostingEnvironment;
            _userManagerService = userManager;
            _roleManagerService = roleManager;
            _orderDataService = orderDataService;
        }
        #endregion

        #region Index

       
        public IActionResult Index()
        {
            return View();
        }
        #endregion

        #region Book Order


        [HttpGet]
        public async Task<IActionResult> Book(int id)
        {
            IdentityUser user = await _userManagerService.FindByNameAsync(User.Identity.Name);
            Package package = _packageDataService.GetSingle(p => p.PackageId == id);

            // Order order = _orderDataService.GetSingle(o => o.OrderId == id);

            OrderBookViewModel vm = new OrderBookViewModel
            {

                UserId = user.Id,
                OrderDate = DateTime.Now,
                PackagId = package.PackageId,
                PackDescription = package.PackDescription,
                PackImage = package.PackImage,
                PackName = package.PackName,
                PackPrice = package.PackPrice,
                PackLocation = package.PackLocation



            };
            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> Book(OrderBookViewModel vm)
        {
            IdentityUser user = await _userManagerService.FindByNameAsync(User.Identity.Name);
            //Package package = _packageDataService.GetSingle(p => p.PackageId == id);

            // Order ord = _orderDataService.GetSingle(o => o.OrderId == id);
            if (ModelState.IsValid)
            {
                Order order = new Order
                {

                    UserId = user.Id,
                    Status = "confirmed",
                    Price = vm.PackPrice,
                    Date = DateTime.Now,
                    PackagId = vm.PackagId,



                };
                _orderDataService.Create(order);
                return RedirectToAction("Add", "Feedback", new { id = vm.PackagId });
            }

            return View(vm);
        }
        #endregion



    }
}