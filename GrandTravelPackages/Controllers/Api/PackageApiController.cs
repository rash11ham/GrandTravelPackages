using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using GrandTravelPackages.Models;
using GrandTravelPackages.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GrandTravelPackages.Controllers.Api
{
    //[Produces("application/json")]
    //[Route("api/PackageApi")]
    public class PackageApiController : Controller
    {
    //    private UserManager<IdentityUser> _userManagerService;
    //    private SignInManager<IdentityUser> _signInManagerService;
    //    private RoleManager<IdentityRole> _roleManagerService;
        private IDataService<Package> _packageDataService;

        public PackageApiController(
            IDataService<Package> packageDataService)
        {
            _packageDataService = packageDataService;
        }

        //web method - get all categories
        //we method URI
        [HttpGet("api/packages")]
        public JsonResult GetAll()
        {
            try
            {
                IEnumerable<Package> list = _packageDataService.GetAll();
                return Json(list);
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { message = ex.Message });
            }
        }

        //web method - get all products by category name
        [HttpGet("api/package/{Location}")]
        public JsonResult GetSearchLocation(string Location)
        {
            try
            {
                Package pack = _packageDataService.GetSingle(p => p.PackLocation.ToUpper() == Location.ToUpper());
                if (pack != null)
                {
                    IEnumerable<Package> list = _packageDataService.Query(p => p.PackLocation == pack.PackLocation);
                    return Json(list);
                }
                else
                {
                    return Json(new { message = "cannot find this category" });
                }
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { message = ex.Message });
            }
        }
  
    }
}