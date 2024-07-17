using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using GrandTravelPackages.Helpers;
using GrandTravelPackages.Models;
using GrandTravelPackages.Services;
using GrandTravelPackages.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Web;
using Microsoft.AspNetCore.Authorization;

namespace GrandTravelPackages.Controllers
{
    #region Class, Constructor and Injection
    //[Authorize("Customer")]
    public class CustomerController : Controller
    {
        private UserManager<IdentityUser> _userManagerService;
        private SignInManager<IdentityUser> _signInManagerService;
        private RoleManager<IdentityRole> _roleManagerService;
        private IDataService<Customer> _customerDataService;
        private IHostingEnvironment _environment;
        private IDataService<Package> _packageDataService;
        private IDataService<Order> _orderDataService;



        public CustomerController(UserManager<IdentityUser> managerService,
                                 SignInManager<IdentityUser> signInService,
                                 RoleManager<IdentityRole> roleService,
                                 IDataService<Customer> customerService,
                                 IHostingEnvironment hostingEnvironment,
                                 IDataService<Package> packageDataService,
                                 IDataService<Order> orderDataService
                                )
        {
            _userManagerService = managerService;
            _signInManagerService = signInService;
            _roleManagerService = roleService;
            _customerDataService = customerService;
            _environment = hostingEnvironment;
            _packageDataService = packageDataService;
            _orderDataService = orderDataService;


        }

        #endregion

        #region Customer Update
       
        [HttpGet]
        public async Task<IActionResult> UpdateProfile()
        {
            if (User.Identity.IsAuthenticated)
            {
                //get the user who already logged in
                IdentityUser user = await _userManagerService.FindByNameAsync(User.Identity.Name);
                //get the profile for this user
                Customer customer = _customerDataService.GetSingle(p => p.UserId == user.Id);
                //create vm
                CustomerUpdateProfileViewModel vm = new CustomerUpdateProfileViewModel
                {

                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    Phone = customer.Phone,
                    Address = customer.Address,
                    ProfilePhoto = customer.ProfilePhoto,
                    Email = user.Email,
                    UserName = user.UserName
                };
                return View(vm);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
            

        }
        //[Authorize("Customer")]
        [HttpPost]
       
        public async Task<IActionResult> UpdateProfile(CustomerUpdateProfileViewModel vm, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                //get the user who already logged in
                IdentityUser user = await _userManagerService.FindByNameAsync(User.Identity.Name);
                //get the profile for this user
                Customer customer = _customerDataService.GetSingle(p => p.UserId == user.Id);
                //map the vm
               
                customer.FirstName = vm.FirstName;
                customer.LastName = vm.LastName;
                customer.Phone = vm.Phone;
                customer.Address = vm.Address;
                customer.ProfilePhoto = vm.ProfilePhoto;

                if (file != null)
                {
                    //&& System.IO.File.Exists(customer.ProfilePhoto)
                    //System.IO.File.Delete(customer.ProfilePhoto);
                    //upload server path
                    var uploadPath = Path.Combine(_environment.WebRootPath, "images");
                    //create subfolder
                    Directory.CreateDirectory(Path.Combine(uploadPath, User.Identity.Name));
                    //get the file name
                    string fileName = FileNameHelper.GetNameFormated(Path.GetFileName(file.FileName));

                    // stream the file to the srever
                    using (var fileStream = new FileStream(Path.Combine(uploadPath, User.Identity.Name, fileName), FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                    //add the file url to category

                    customer.ProfilePhoto = User.Identity.Name + "/" + fileName;
                }

                //save changes
                _customerDataService.Update(customer);

                user.Email = vm.Email;
                user.UserName = vm.UserName;
                await _userManagerService.UpdateAsync(user);
                //go home
                return RedirectToAction("Index", "Customer");
            }
            return View(vm);
        }

     
   
            #endregion

            #region PasswordUpdate

        [HttpGet]
        public async Task<IActionResult> PasswordUpdate()
        {
            if (User.Identity.IsAuthenticated)
            {
                //get the user who already logged in
                IdentityUser user = await _userManagerService.FindByNameAsync(User.Identity.Name);
                var hashPassword = await _userManagerService.HasPasswordAsync(user);
                //get the customer for this user
                //Customer customer = _customerDataService.GetSingle(p => p.UserId == user.Id);
                // create vm
                CustomerPasswordUpdateViewModel vm = new CustomerPasswordUpdateViewModel
                {

                };
                return View(vm);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
            

        }

       
        [HttpPost]
        public async Task<IActionResult> PasswordUpdate(CustomerPasswordUpdateViewModel vm)
        {


            if (ModelState.IsValid)
            {
                IdentityUser user = await _userManagerService.FindByNameAsync(User.Identity.Name);
                Customer profile = _customerDataService.GetSingle(p => p.UserId == user.Id);
                var result = await _userManagerService.ChangePasswordAsync(user, vm.OldPassword, vm.NewPassword);
                if (vm.OldPassword != vm.NewPassword)
                {
                    vm.NewPassword = vm.ConfirmPassword;
                }

                else if (!result.Succeeded)
                {
                    ViewBag.notification = "Sorry! Please try again";

                }
               
                await _signInManagerService.SignInAsync(user, false);

                return RedirectToAction("Login", "Account");

            }
            return View(vm);
        }



        #endregion


        #region Package Display, Search & Filter
        [HttpGet]
        public async Task<IActionResult> Index(string SearchLocation, decimal MinPrice, decimal MaxPrice)
        {


            IEnumerable<Package> package;
            IdentityUser user = await _userManagerService.FindByNameAsync(User.Identity.Name);
            Customer cust = _customerDataService.GetSingle(c => c.UserId == user.Id);
            ViewData["PackLocation"] = SearchLocation;
            ViewData["MinPrice"] = MinPrice;
            ViewData["MaxPrice"] = MaxPrice;

            package = _packageDataService.GetAll().Where(p => p.PackageStatus == 0);
            // 0 comes from the Enum: Active || to display active Packages for the loged in customer


            if (!String.IsNullOrEmpty(SearchLocation))
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
                     .Where(p => p.PackLocation.ToUpper().Trim().Contains(SearchLocation.ToUpper().Trim()) && p.PackageStatus == 0);
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
                       .Where(p => p.PackPrice >= MinPrice && p.PackPrice <= MaxPrice && p.PackageStatus == 0);
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
         

            CustomerIndexViewModel vm = new CustomerIndexViewModel
            {
                GetPackage = package,
                TotalPackageFound = 0,
                ProfilePhoto = cust.ProfilePhoto

            };
           
            ViewData["PackLocation"] = "";
            ViewData["MinPrice"] = "";
            ViewData["MaxPrice"] = "";

            return View(vm);

        }
        #endregion

        #region Display Ordered Packages
        [HttpGet]
        public async Task<IActionResult> Order()
        {
            if (User.Identity.IsAuthenticated)
            {
                IdentityUser user = await _userManagerService.FindByNameAsync(User.Identity.Name);
                //get all Orderedpackage for this user
                if (User.IsInRole("Customer"))
                {
                    IEnumerable<Order> order = _orderDataService.GetAll().Where(p => p.UserId == user.Id);
                    Customer customer = _customerDataService.GetSingle(c => c.UserId == user.Id);
                    CustomerOrderViewModel vm = new CustomerOrderViewModel
                    {
                        GetOrders = order,
                        GetCustomer = customer

                    };
                    return View(vm);
                }
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
            
            
        

           
        }
        #endregion

    }
}