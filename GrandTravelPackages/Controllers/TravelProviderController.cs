using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GrandTravelPackages.Models;
using GrandTravelPackages.Services;
using GrandTravelPackages.ViewModels;
using Microsoft.AspNetCore.Identity;


namespace GrandTravelPackages.Controllers
{
    public class TravelProviderController : Controller
    {
        #region Class, Constructor and Injection

  
 
            private UserManager<IdentityUser> _userManagerService;
            private SignInManager<IdentityUser> _signInManagerService;
            private RoleManager<IdentityRole> _roleManagerService;
            private IDataService<TravelProvider> _providerDataService;
        private IDataService<Package> _packageDataService;


        public TravelProviderController(UserManager<IdentityUser> managerService,
                                     SignInManager<IdentityUser> signInService,
                                     RoleManager<IdentityRole> roleService,
                                     IDataService<TravelProvider> providerService,
                                     IDataService<Package> packageDataService
                                    )
            {
                _userManagerService = managerService;
                _signInManagerService = signInService;
                _roleManagerService = roleService;
                _providerDataService = providerService;
            _packageDataService = packageDataService;

            }
        #endregion


        #region Provider Index & search        
        public async Task<IActionResult> Index(string SearchLocation, decimal MinPrice, decimal MaxPrice)
        {
            //ModelState.Clear();
            IEnumerable<Package> package = null;
            IdentityUser user = await _userManagerService.FindByNameAsync(User.Identity.Name);
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
                    && p.PackPrice >= MinPrice && p.PackPrice <= MaxPrice && p.UserId == user.Id);
                    if (package.Count() <= 0)
                    {
                        ViewBag.MyMessage = "no result found! Please try different location";
                    }
                }
                else //if (MaxPrice == 0 && MinPrice == 0)
                {
                    package = _packageDataService.GetAll()
                   .Where(p => p.PackLocation.ToUpper().Trim()
                   .Contains(SearchLocation.ToUpper().Trim()) && p.UserId == user.Id);
                    if (package.Count() <= 0)
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
                       && p.PackPrice <= MaxPrice && p.UserId == user.Id);
                }
                else if (MaxPrice > 0)
                {
                    package = _packageDataService.GetAll()
                       .Where(p => p.PackPrice <= MaxPrice && p.UserId == user.Id);
                }
                else if (MinPrice > 0)
                {
                    package = _packageDataService.GetAll()
                       .Where(p => p.PackPrice >= MinPrice && p.UserId == user.Id);
                }


                else
                {
                    package = _packageDataService.GetAll().Where(p => p.UserId == user.Id);

                }

            }
            if (MinPrice > MaxPrice && MaxPrice > 0)
            {
                //Add error message to the model stage, THIS METHOD IS PREFERRED !!!.
                ViewBag.MyMessage = "Minimum price must be less than maximum price";

            }

            TravelProviderIndexViewModel vm = new TravelProviderIndexViewModel
            {
                GetPackage = package,
                total_packages = package.Count()
            };


            ViewData["PackLocation"] = "";
            ViewData["MinPrice"] = "";
            ViewData["MaxPrice"] = "";

            return View(vm);

        }
        #endregion


        #region ProviderUpdate



        [HttpGet]
            public async Task<IActionResult> ProviderUpdate()
            {
                //get the user who already logged in
                IdentityUser user = await _userManagerService.FindByNameAsync(User.Identity.Name);
                //get the profile for this user
                TravelProvider provider = _providerDataService.GetSingle(p => p.UserId == user.Id);
                //create vm
                TravelProviderUpdateProfileViewModel vm = new TravelProviderUpdateProfileViewModel
                {

                    CompanyName = provider.CompanyName,
                    Phone = provider.Phone,
                    Address = provider.Address,
                    WebSite = provider.WebSite,
                    Email = user.Email,
                    UserName = user.UserName
                };
                return View(vm);

            }
            [HttpPost]
            public async Task<IActionResult> ProviderUpdate(TravelProviderUpdateProfileViewModel vm)
            {
                if (ModelState.IsValid)
                {
                    //get the user who already logged in
                    IdentityUser user = await _userManagerService.FindByNameAsync(User.Identity.Name);
                    //get the profile for this user
                    TravelProvider provider = _providerDataService.GetSingle(p => p.UserId == user.Id);
                    //map the vm
                  //  provider.UserId = user.Id;
                    provider.CompanyName = vm.CompanyName;
                    provider.Phone = vm.Phone;
                    provider.Address = vm.Address;
                    provider.WebSite = vm.WebSite;
                    user.Email = vm.Email;
                    user.UserName = vm.UserName;

                    //save changes
                    _providerDataService.Update(provider);                   
                    await _userManagerService.UpdateAsync(user);
                    //go home
                    return RedirectToAction("Index", "Home");
                }
                return View(vm);
            }
        #endregion
        #region PasswordUpdate

        [HttpGet]
        public async Task<IActionResult> PasswordUpdate()
        {
            //get the user who already logged in
            IdentityUser user = await _userManagerService.FindByNameAsync(User.Identity.Name);
            var hashPassword = await _userManagerService.HasPasswordAsync(user);
            
            // create vm
            TravelProviderPasswordUpdateViewModel vm = new TravelProviderPasswordUpdateViewModel
            {

            };
            return View(vm);

        }

        [HttpPost]
        public async Task<IActionResult> PasswordUpdate(TravelProviderPasswordUpdateViewModel vm)
        {

            if (ModelState.IsValid)
            {
                IdentityUser user = await _userManagerService.FindByNameAsync(User.Identity.Name);
                TravelProvider profile = _providerDataService.GetSingle(p => p.UserId == user.Id);
                var result = await _userManagerService.ChangePasswordAsync(user, vm.OldPassword, vm.NewPassword);
                if (vm.OldPassword != vm.NewPassword)
                {
                    vm.NewPassword = vm.ConfirmPassword;
                }
                if (!result.Succeeded)
                {
                    ViewBag.notification = "Sorry! Please try again";

                }
                await _signInManagerService.SignInAsync(user, false);

                return RedirectToAction("Login", "Account");

            }
            return View(vm);
        }

        #endregion
    }
}