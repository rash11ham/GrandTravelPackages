using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
//..
using Microsoft.Extensions.DependencyInjection;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using GrandTravelPackages.ViewModels;
using GrandTravelPackages.Services;
using GrandTravelPackages.Models;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using GrandTravelPackages.Helpers;
using Microsoft.AspNetCore.Authorization;


namespace GrandTravelPackages.Controllers
{
    
    public class AccountController : Controller
    {
        #region classes, constructor and injection

       
        private UserManager<IdentityUser> _userManagerService;
        private SignInManager<IdentityUser> _signInManagerService;
        private RoleManager<IdentityRole> _roleManagerService;
        private IDataService<Customer> _customerDataService;
        private IDataService<TravelProvider> _providerDataService;
        private IHostingEnvironment _environment;



        public AccountController(UserManager<IdentityUser> managerService,
                                 SignInManager<IdentityUser> signInService,
                                 RoleManager<IdentityRole> roleService,
                                 IDataService<Customer> customerService,
                                 IDataService<TravelProvider> providerService,
                                 IHostingEnvironment environment
                                )
        {
            _userManagerService = managerService;
            _signInManagerService = signInService;
            _roleManagerService = roleService;
            _customerDataService = customerService;
            _providerDataService = providerService;
            _environment = environment;
           
        }
        #endregion
        #region Customer Registration

        
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(CustomerRegisterViewModel vm, IFormFile file)
        {

            if (ModelState.IsValid)
            {
                //user
                IdentityUser user = new IdentityUser(vm.UserName);
                //add to database
                IdentityResult result = await _userManagerService.CreateAsync(user, vm.Password);
                if (vm.Password != null)
                {
                    vm.Password = vm.ConfirmPassword;
                }

                if (result.Succeeded)
                {
                    //add a default role
                    if (await _roleManagerService.FindByNameAsync("Customer") != null)
                    {
                        await _userManagerService.AddToRoleAsync(user, "Customer");
                    }

                    //add default profile
                    //get the user back to find the id
                    user = await _userManagerService.FindByNameAsync(vm.UserName);
                    Customer profile = new Customer
                    {
                        UserId = user.Id,
                        FirstName = vm.FirstName,
                        LastName = vm.LastName,
                        Address = vm.Address,
                        Email = vm.Email,
                        Phone = vm.Mobile,
                        ProfilePhoto = vm.ProfilePhoto


                    };
                    //..file

                    if (file != null)
                    {
                        //upload server path
                        var uploadPath = Path.Combine(_environment.WebRootPath, "images");
                        //create subfolder
                        Directory.CreateDirectory(Path.Combine(uploadPath, vm.UserName));
                        //get the file name
                        string fileName = FileNameHelper.GetNameFormated(Path.GetFileName(file.FileName));
                                               
                        // stream the file to the srever
                        using (var fileStream = new FileStream(Path.Combine(uploadPath, vm.UserName, fileName), FileMode.Create))
                        {
                            await file.CopyToAsync(fileStream);
                        }
                        //add the file url to category

                        profile.ProfilePhoto = vm.UserName + "/" + fileName;
                    }

                    _customerDataService.Create(profile);

                    //go to home page
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        //show 
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }
            return View(vm);

        }
        #endregion
      
            #region Provider Registration


        [HttpGet]
        [AllowAnonymous]
        public IActionResult ProviderRegister()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ProviderRegister(TravelProviderRegisterViewModel vm)
        {

            if (ModelState.IsValid)
            {
                //user
                IdentityUser user = new IdentityUser(vm.UserName);
                //add to database
                IdentityResult result = await _userManagerService.CreateAsync(user, vm.Password);
               
                if (vm.Password != null)
                {
                    vm.Password = vm.ConfirmPassword;
                }

                if (result.Succeeded)
                {
                    //add a default role
                    if (await _roleManagerService.FindByNameAsync("TravelProvider") != null)
                    {
                        await _userManagerService.AddToRoleAsync(user, "TravelProvider");
                    }

                    //add default profile
                    //get the user back to find the id
                    user = await _userManagerService.FindByNameAsync(vm.UserName);
                    TravelProvider provider = new TravelProvider
                    {
                        UserId = user.Id,
                        CompanyName = vm.CompanyName,
                        WebSite = vm.WebSite,
                        Address = vm.Address,
                        Email = vm.Email,
                        Phone = vm.Phone
               
                    };
                    _providerDataService.Create(provider);

                    //go to home page
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        //show 
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }
            return View(vm);

        }
        #endregion
        #region User Login

        
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string returnUrl = "")
        {
            AccountLoginViewModel vm = new AccountLoginViewModel
            {
                ReturnUrl = returnUrl
            };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Login(AccountLoginViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManagerService.PasswordSignInAsync(vm.UserName, vm.Password, vm.RememberMe, false);

                if (result.Succeeded)
                {
                    IdentityUser user = await _userManagerService.FindByNameAsync(vm.UserName);
                    var role = await _userManagerService.GetRolesAsync(user);
                    if (!string.IsNullOrEmpty(vm.ReturnUrl))
                    {
                        return Redirect(vm.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", role[0].ToString());
                       
                    }

                }
                else
                {
                    ModelState.AddModelError("", "Username or password incorrect");
                }
            }
            return View(vm);
        }
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManagerService.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        #endregion


    }
}