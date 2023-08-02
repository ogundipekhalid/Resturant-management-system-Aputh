using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using RDMS.AppAuteticte;
using RDMS.Interface.Service;
using RDMS.Models.Dtos;

namespace RDMS.Controllers
{
    public class StaffController : Controller
    {
        private readonly IStaffService _staffService;
        private readonly IPositionService _positionService;

        private readonly IBranchService _branchService;
        private readonly IEateryServices _eateryServices;
        private readonly IAppAuthentication _appAuthentication;
        public StaffController(IStaffService staffService, IPositionService positionService, IBranchService branchService, IEateryServices eateryServices, IAppAuthentication appAuthentication)
        {
            _staffService = staffService;
            _positionService = positionService;
            _branchService = branchService;
            _eateryServices = eateryServices;
            _appAuthentication = appAuthentication;
        }

        public async Task<IActionResult> Adds(int id)
        {
            var branchList = await _branchService.GetBranchByEateryId(id);
            var model = new CreateStaffRequestModel{
                EateryId = (id),//AppAuthentication.GetRouteData("id"),
                BranchList = new SelectList( branchList.Data, "Id", "Name"),
            };
            return View();
        }
         public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add( CreateStaffRequestModel model)
        {
             var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var staff = await _staffService.Create(userId,model);
            if (staff.Message == "User already exist")
            {
                TempData["ErrorMessage"] = staff.Message;
                return RedirectToAction("Register", "Staff");
            }
                TempData["SuccessMessage"] = staff.Message;
                return RedirectToAction("Login", "User");
        }

       

        [ActionName("Index")]
        [HttpGet("Staff/Index")]
         public async Task<IActionResult> IndexAsyc()
        {
            return View(await _staffService.GetAllStaff());
         
        }

         [ActionName("GetAllEateryStaff")]
        [HttpGet("Staff/GetAllEateryStaff")]
        public async Task<IActionResult> ListOfStaffsByEatery(int companyId)
        {
            var staffs = await _staffService.GetStaffsByEateryId(companyId);
            return View(staffs.Data);
        }

        [ActionName("GetAllStaffBranch")]
        [HttpGet("Staff/GetAllStaffBranch")]
        public async Task<IActionResult> ListStaffsBranch(int id)
        {
            //var user = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
           // // var staffs = await _staffService.GetStaffsByBranchId(id);
            var staffs = await _staffService.GetStaffsByBranchId(id);
            return View(staffs.Data);
        }


         [ActionName("StaffProfile")]
        public async Task<IActionResult> Details(int id)
        {
         var user = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var staff = await _staffService.Get(id);
            return View(staff.Data);
        }
         [ActionName("ProfileStaff")]
        public async Task<IActionResult> Detailes(int id)
        {
           var UseLog =  int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var staff = await _staffService.Get(UseLog);
            return View(staff.Data);
        }

         [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var staff = await _staffService.Get(id);
     
            return View(staff.Data);
        }



        [HttpPost]
        public async Task<IActionResult> Update(int id,UpdateStaffRequestModel model)
        {

           await _staffService.Update(id,model);
            return RedirectToAction("Staff", "User");
        }

         [ActionName("DeleteStaff")]
         public async Task<IActionResult> DeleteStaffAsync(int id)
        {
            
            if(HttpContext.Request.Method == "POST")
            {                
                var ans = await _staffService.RemoveStaff(id);
                if(ans.Status == true)
                {
                    return StatusCode(201,"Staff Account Successfully Deleted");
                }
                    return StatusCode(406,"Staff not Deleted");
            }
            return View();
        }
       
    }
}