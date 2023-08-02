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
    public class EateryController : Controller
    {
        private readonly IEateryServices _eateryServices;
        private readonly IEateryManagerServices _eateryManagerServices;

        public EateryController(
            IEateryServices eateryServices,
            IEateryManagerServices eateryManagerServices
        )
        {
            _eateryServices = eateryServices;
            _eateryManagerServices = eateryManagerServices;
        }

        public async Task<IActionResult> Eatery()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var eatry = await _eateryServices.GetByAdminId(userId);
            ViewBag.Eatery = eatry.Data;
            return View();
        }

        public async Task<IActionResult> Profile(int id)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            //var eatry = await _eateryServices.GetByAdminId(userId);
            var result = await _eateryServices.GetOneUser(userId);
            return View(result.Data);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(CreataEateryRequestModel model)
        {
            var result = await _eateryServices.CreateEatery(model);
            if (result.Message == "User already exist")
            {
                TempData["ErrorMessage"] = result.Message;
                return RedirectToAction("Register", "Eatery");
            }
            TempData["SuccessMessage"] = result.Message;
            return RedirectToAction("Login", "User");

        }

        // public async Task<IActionResult> Delete(int id)
        // {
        //     var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        //     //var eatry = await _eateryServices.GetByAdminId(userId);
        //     var result = await _eateryServices.RemoveEatery(id);
        //     return View(result);
        // }
        [ActionName("RealDelete")]
        //[HttpPost]
        public async Task<IActionResult> RemoveDelete(int id)
        {
            var result = await _eateryServices.RemoveEatery(id);
            if (result.Status)
            {
                return RedirectToAction("GetAllEateryForAdmin");
            }
            return View(result);
        }

        [ActionName("Delete")]
        public async Task<IActionResult> DeleteForSuperAdmin(int id)
        {
            var result = await _eateryServices.RemoveEatery(id);
            if (result.Status)
            {
                return RedirectToAction("GetAllEateryForAdmin");
            }
            return View(result);
        }

        public async Task<IActionResult> Details(int id)
        {
            var user = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            // var result = await _eateryServices.GetOneUser(id);
            var result = await _eateryServices.GetByAdminId(user);
            ViewBag.Eatery = result.Data;
            if (result.Status)
            {
                return View(result.Data);
            }
            TempData["message"] = result.Message;
            return View();
        }

        public async Task<IActionResult> DetailsVerfy(int id)
        {
            var user = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var result = await _eateryServices.GetOneUser(user);
            if (!result.Status)
            {
                return View();
            }
            return View(result.Data);
        }

        public async Task<IActionResult> GetAllEatery(int id)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var eatry = await _eateryServices.GetByAdminId(userId);
            ViewBag.Eatery = eatry.Data;
            // var eatery = await _eateryServices.GetAllEatery();
            var eatery = await _eateryServices.GetAllEateryOne(id);
            if (eatery.Status == true)
            {
                return View(eatery.Data);
            }
            var newCust = eatery.Data.ToList();
            return View(newCust);
        }

        public async Task<IActionResult> GetAllEateryForAdmin()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var result = await _eateryServices.GetAllEatery();
            return View(result.Data);
        }

        public async Task<IActionResult> Update(int id)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var result = await _eateryServices.GetByAdminId(userId);
            ViewBag.Eatery = result.Data;
            if (result.Status)
            {
                return View(result.Data);
            }
            TempData["message"] = result.Message;
            return View(result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, UpdateEateryRequestModel model)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var response = await _eateryServices.Update(userId, model);
            ViewBag.Eatery = response.Data;
            if (response.Status)
            {
                TempData["Successful"] = " Updated Successful";
                return RedirectToAction("Eatery", "User");
            }
            return View();
        }

        public async Task<IActionResult> Verify(int id, VerifyEateryRequestModel model)
        {
            var result = await _eateryServices.VerifyEatery(id, model);

            // return RedirectToAction("Details", new{id = id});
            return RedirectToAction("DetailsVerfy", new { id = id });
            // return RedirectToAction("GetAllEateryForAdmin");
        }

        public async Task<IActionResult> NotVerify(int id, VerifyEateryRequestModel model)
        {
            var result = await _eateryServices.NotVerify(id, model);

            return RedirectToAction("DetailsVerfy", new { id = id });
        }
    }
}
