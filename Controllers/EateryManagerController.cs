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
    public class EateryManagerController : Controller
    {

        private readonly IEateryServices _eateryServices;
        private readonly IEateryManagerServices _eateryManagerServices;

        public EateryManagerController(IEateryServices eateryServices, IEateryManagerServices eateryManagerServices)
        {
            _eateryServices = eateryServices;
            _eateryManagerServices = eateryManagerServices;
        }

        public IActionResult Index()
        {
            return View();
        }

         public async Task<IActionResult> DetailsCompanyManager(string email)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
              var eatery = await _eateryManagerServices.GetManager(userId);
            return View(eatery.Data);
        }

        public async Task<IActionResult> GetAllEateryManager()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var result = await _eateryManagerServices.GetAllManager();
            return View(result.Data);
        }

        public async Task<IActionResult> UpdateManager(int id)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var result = await _eateryManagerServices.Get(userId);
            ViewBag.Eatery = result.Data;
            if (result.Status)
            {
                return View(result.Data);
            }
            TempData["message"] = result.Message;
            return View(result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateManager(int id,  UpdateEateryManagerRequestModel model)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var response = await _eateryManagerServices.UpdateManager(userId, model);
            ViewBag.Eatery = response.Data;
            if (response.Status)
            {
                TempData["Successful"] = " Updated Successful";
                return RedirectToAction("Eatery", "User");
            }
            return View();
        }





    }
}