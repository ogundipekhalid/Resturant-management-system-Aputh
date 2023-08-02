using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RDMS.Interface.Service;
using RDMS.Models.Dtos;
using RDMS.ViewModel;

namespace RDMS.Controllers
{
    public class BranchController : Controller
    {
        private readonly IBranchService _branchServices;
        private readonly IBranchMangerService _branchMangerService;
        private readonly IEateryServices _eateryServices;

        public BranchController(IBranchService branchServices, IBranchMangerService branchMangerService, IEateryServices eateryServices)
        {
            _branchServices = branchServices;
            _branchMangerService = branchMangerService;
            _eateryServices = eateryServices;
        }

        public async Task<IActionResult> Index()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var eatry = await _branchServices.GetBranchByEateryId(userId);
            ViewBag.Eatery = eatry.Data;
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(CreateBranchModel model)
        {
            var branch = await _branchServices.CreateBranch(model);
             if (branch.Message == "User already exist")
            {
                TempData["ErrorMessage"] = branch.Message;
                return RedirectToAction("Register", "Branch");
            }
            return RedirectToAction("Login", "User");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBranchy()
        {
             var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
             var eatry = await _eateryServices.GetByAdminId(userId);
            ViewBag.Eatery = eatry.Data;
            var response = await _branchServices.GetAllBranch();
            return View(response.Data);
        }

        /**/
        [HttpGet]
        public async Task<IActionResult> GetAllBranch()
        {
           //  var orders = await _branchServices.GetAllBranch();
            var order = await _branchServices.GetBranchesByCompanyId();
          
            if (order.Data != null)
            {
                return View(order.Data);
            }
            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBranches()
        {
            var response = await _branchServices.GetAllBranch();
            // var response = await _branchServices.GetBranchesByEateryId(id);
            return View(response.Data);
        }

         public async Task<IActionResult> Details(int id , int useId)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var response = await _branchServices.GetBranchByEateryId(id);
            ViewBag.Eatery = response.Data;
            ViewBag.IsVerify = true;
            if(response.Status)
            {
                return View(response.Data);
            }
            TempData["message"] = response.Message;
            return View();
        }
         public async Task<IActionResult> DetailsBranch(int id)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var response = await _branchServices.Get(id);
            if(response.Status)
            {
                return View(response.Data);
            }
            TempData["message"] = response.Message;
            return View();
        }
        public async Task<IActionResult> Update()
        {
            // var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
           // var response = await _branchServices.Get(id);
            // ViewBag.Eatery = response.Data;
          //  return View(response.Data);
           return View();
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, UpDateBranchRequestModel model)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var response = await _branchServices.UpdateBranch(userId, model);
             ViewBag.Eatery = response.Data;
            if (response.Status)
            {
                TempData["Successful"] = " Updated Successful";

                return RedirectToAction("Details");
                // return RedirectToAction("GetAllBranches");
            }
            return View();
        }

       // [HttpGet]
        public async Task<IActionResult> BranchDash()
        {
            var response = await _branchServices.BranchDeshResponceModels();
     
            return View(response.Data);
        }




            ////////
        ////anoteher

       

    }
}
