using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using GrandTravelPackages.Helpers;
using GrandTravelPackages.Models;
using GrandTravelPackages.Services;
using GrandTravelPackages.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GrandTravelPackages.Controllers
{

    

   
    public class PackageController : Controller
    {
        #region Constructor & Injections
        private IDataService<TravelProvider> _providerDataService;
        private IDataService<Package> _packageDataService;
        private IHostingEnvironment _environment;
        private UserManager<IdentityUser> _userManagerService;
        private RoleManager<IdentityRole> _roleManagerService;

        public PackageController(IDataService<TravelProvider> providerDataService,
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

        #region Display Package, Search & Filter

        
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Index(string SearchLocation, decimal MinPrice, decimal MaxPrice)
        {
          
            
            IEnumerable<Package> package;
            IdentityUser user = await _userManagerService.FindByNameAsync(User.Identity.Name);
            ViewData["PackLocation"] = SearchLocation;
            ViewData["MinPrice"] = MinPrice;
            ViewData["MaxPrice"] = MaxPrice;

            //get all package of this provider
            if (User.IsInRole("TravelProvider"))
            {
                package = _packageDataService.GetAll().Where(t => t.UserId == user.Id);
                // get all those Packages which belongs to the provider loged in || Where(t => t.UserId == user.Id)
            }
            else
            {
                package = _packageDataService.GetAll().Where(p => p.PackageStatus == 0);
                // 0 comes from the Enum: Active || to display active Packages for the loged in customer


                if (!String.IsNullOrEmpty(SearchLocation))
                {

                    if (MaxPrice>0 && MinPrice>0)
                    {
                        package = _packageDataService.GetAll()
                    .Where(p => p.PackLocation.ToUpper().Trim().Contains(SearchLocation.ToUpper().Trim()) 
                    && p.PackPrice >= MinPrice && p.PackPrice <= MaxPrice);
                    }
                    else 
                    {
                        package = _packageDataService.GetAll()
                   .Where(p => p.PackLocation.ToUpper().Trim().Contains(SearchLocation.ToUpper().Trim()));
                    }
                   

                }
                else 
                {
                    if (MaxPrice>0 && MinPrice>0)
                    {
                        package = _packageDataService.GetAll()
                           .Where(p => p.PackPrice >= MinPrice && p.PackPrice <= MaxPrice);
                    }
                    else if (MaxPrice > 0)
                    {
                        package = _packageDataService.GetAll()
                           .Where(p => p.PackPrice <=MaxPrice);
                    }
                    else if (MinPrice > 0)
                    {
                        package = _packageDataService.GetAll()
                           .Where(p => p.PackPrice >= MinPrice);
                    }

                    else
                    {
                        package = _packageDataService.GetAll();
                    }
                    
                }

            }

           
            PackageIndexViewModel vm = new PackageIndexViewModel
            {
                Packages = package,
                TotalPackageFound = 0

            };
            return View(vm);
            
        }
        #endregion

        #region Add Package
                
        [HttpGet]
        //[Authorize("TravelProvider")]
        public IActionResult Add()
        {
            
            if (User.Identity.IsAuthenticated)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
            
            
        }
        [HttpPost]
        //[Authorize("TravelProvider")]
        public async Task<IActionResult> Add(PackageAddViewModel vm, IFormFile file)
        {

            if (ModelState.IsValid)
            {
                //get the user who already logged in
                IdentityUser user = await _userManagerService.FindByNameAsync(User.Identity.Name);
                //get the profile for this user
                //call the service to check if Package name already exist
                Package packExist = _packageDataService.GetSingle(p => p.PackName.ToUpper().Trim() == vm.PackName.ToUpper().Trim());

                if (packExist == null) // Not found, then ok to add
                {
                    Package package = new Package
                    {
                        UserId = user.Id,
                        PackName = vm.PackName,
                        PackLocation = vm.PackLocation,
                        PackDescription = vm.PackDescription,
                        PackPrice = vm.PackPrice,
                        PackageStatus = vm.PackageStatus 
                       

                    };


                    if (file != null)
                    {
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

                        package.PackImage = "images" + "/" + User.Identity.Name + "/" + fileName;
                    }

                    _packageDataService.Create(package);

                    //go to home page
                    return RedirectToAction("Index", "Package");

                }
                else
                {

                    ModelState.AddModelError("", "Something is wrong");
                }
               
            }
            return View(vm);
        }
        #endregion


        #region Udate Package

        
        [HttpGet]
        //[Authorize("TravelProvider")]
        public async Task<IActionResult> Update(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                //get the user who already logged in
                IdentityUser user = await _userManagerService.FindByNameAsync(User.Identity.Name);

                //get the profile for this user
                Package package = _packageDataService.GetSingle(p => p.PackageId == id);

                if (package?.UserId == user.Id)
                {
                    //create vm
                    PackageUpdateViewModel vm = new PackageUpdateViewModel
                    {
                        PackageId = package.PackageId,
                        PackName = package.PackName,
                        PackLocation = package.PackLocation,
                        PackDescription = package.PackDescription,
                        PackPrice = package.PackPrice,
                        PackImage = package.PackImage,
                        PackageStatus = package.PackageStatus

                    };
                    return View(vm);
                }
                else
                {
                    return RedirectToAction("Index", "TravelProvider");
                }

            }
            else
            {
                return RedirectToAction("Index","Home");
            }
            

        }
        [HttpPost]
        //[Authorize("TravelProvider")]
        public async Task<IActionResult> Update(PackageUpdateViewModel vm, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                //get the user who already logged in
                IdentityUser user = await _userManagerService.FindByNameAsync(User.Identity.Name);
                
              
                Package pack = new Package
                {
                    UserId = user.Id,
                    PackageId = vm.PackageId,
                    PackName = vm.PackName,
                    PackLocation = vm.PackLocation,
                    PackDescription = vm.PackDescription,
                    PackPrice = vm.PackPrice,                    
                    PackImage = vm.PackImage,
                    PackageStatus = vm.PackageStatus
                    

                };
                if (file != null)
                {
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

                    pack.PackImage = "images" + "/" + User.Identity.Name + "/" + fileName;
                }

                //save changes
                _packageDataService.Update(pack);                              
               
                //go home
                return RedirectToAction("Index", "Package");
            }
            return View(vm);
        }

        #endregion

      
    }

}