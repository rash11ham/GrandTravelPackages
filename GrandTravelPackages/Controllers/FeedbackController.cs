using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GrandTravelPackages.Models;
using GrandTravelPackages.Services;
using GrandTravelPackages.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GrandTravelPackages.Controllers
{
    public class FeedbackController : Controller
    {
        #region Constructor & Injectors
        //private IDataService<TravelProvider> _providerDataService;
        private IDataService<Package> _packageDataService;
        private IHostingEnvironment _environment;
        private UserManager<IdentityUser> _userManagerService;
        //private RoleManager<IdentityRole> _roleManagerService;
        private IDataService<Order> _orderDataService;
        private IDataService<Feedback> _feedbackDataService;

        public FeedbackController(IDataService<Package> packageDataService,
            IHostingEnvironment environment,
            UserManager<IdentityUser> userManagerService,
            IDataService<Order> orderDataService,
            IDataService<Feedback> feedbackDataService)
        {
            _packageDataService = packageDataService;
            _environment = environment;
            _userManagerService = userManagerService;
            _orderDataService = orderDataService;
            _feedbackDataService = feedbackDataService;
        }

        #endregion

        #region Feedback

        [HttpGet]
        public async Task<IActionResult> Add(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                ////get the user who already logged in
                IdentityUser user = await _userManagerService.FindByNameAsync(User.Identity.Name);
                ////get the profile for this user
                Package package = _packageDataService.GetSingle(p => p.PackageId == id);
                ////create vm
                if (package?.UserId == user.Id)
                {
                    FeedbackAddViewModel vm = new FeedbackAddViewModel
                    {
                        PackageId = id,
                        GetPackage = package
                    };
                    return View(vm);
                }
                else
                {
                    return RedirectToAction("Index", "Customer");
                }

            }
                else
            {
                return RedirectToAction("Login", "Account");
            }
            
        }
        [HttpPost]
        //[Authorize("TravelProvider")]
        public async Task<IActionResult> Add(FeedbackAddViewModel vm)
        {

            if (ModelState.IsValid)
            {
                //get the user who already logged in
                IdentityUser user = await _userManagerService.FindByNameAsync(User.Identity.Name);
                //get the profile for this user
                //call the service to check if Package name already exist
                //Order order = _orderDataService.GetSingle(o => o.OrderId == vm.OrderId);
                Feedback feedback = _feedbackDataService.GetSingle(p => p.OrdFeedback == vm.OrdFeedback);
                if (feedback != null)
                {
                    Feedback feedb = new Feedback
                    {
                        //OrderId = vm.OrderId,
                        PackageId = vm.PackageId,
                        UserId = user.Id,
                        OrdFeedback = vm.OrdFeedback
                    };
                    _feedbackDataService.Create(feedb);

                    //go to home page
                    return RedirectToAction("Add", "Feedback");
                }

                else
                {

                    ModelState.AddModelError("", "Something is wrong");
                }


            }
            return View(vm);
        }
        #endregion




    }
}