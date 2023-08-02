using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RDMS.Interface.Service;
using RDMS.Models.Dtos;

namespace RDMS.Controllers
{
    
    public class BranchManagerController : Controller
    {
         private readonly IBranchService _branchServices;
        private readonly IBranchMangerService _branchMangerService;

        public BranchManagerController(IBranchService branchServices, IBranchMangerService branchMangerService)
        {
            _branchServices = branchServices;
            _branchMangerService = branchMangerService;
        }

        public IActionResult Index()
        {
            return View();
        }
        

          [HttpGet]
        public async Task<IActionResult> GetAllManager()
        {
            /// var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
           
            var response = await _branchMangerService.GetAllBranchManager();
            return View(response.Data);
        }

        public async Task<IActionResult> GetProfileCompanyManager(int id)
        {
            var user = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            
           // var customer = await _customerService.Get(int.Parse(user));
            var branchdetails = await _branchMangerService.GetBranchManager(id);
            //  if (brancgdetqails.Status == true)
            //  {
            return View(branchdetails.Data);
        }


         public async Task<IActionResult> UpdateCompanyManager(int id)
        {
            // var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var response = await _branchServices.Get(id);
            // ViewBag.Eatery = response.Data;
            return View(response.Data);
        }



         [HttpPost]
         public async Task<IActionResult> UpdateCompanyManager(int userId, UpdateBranchManagerRequestModel model)
        {
          
           var  Branch = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var response = await _branchMangerService.UpdateManager(Branch, model);
             ViewBag.Eatery = response.Data;
            if (response.Status)
            {
                TempData["Successful"] = " Updated Successful";

                // return RedirectToAction("GetAllManager");
                return RedirectToAction("Branch", "User");
            }
            return View();
        }
    }
}