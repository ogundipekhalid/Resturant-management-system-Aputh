using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using RDMS.Interface.Service;
using RDMS.Models.Dtos;

namespace RDMS.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserServices _userServices;
        private readonly IEateryServices _eateryService;
        private readonly IBranchService _branchService;
        private readonly IOrderService _orderService;

        public UserController(IUserServices userServices, IEateryServices eateryService, IBranchService branchService, IOrderService orderService)
        {
            _userServices = userServices;
            _eateryService = eateryService;
            _branchService = branchService;
            _orderService = orderService;
        }

        public IActionResult Dashboard()
        {
            return View();
        }
        public IActionResult Customer()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LogingRequesteModel model)
        {
            var user = await _userServices.Login(model);

            if (user.Data != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Data.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Data.Email),                    
                    new Claim(ClaimTypes.Name, user.Data.FirstName + " " + user.Data.LastName),
                   /// new Claim(ClaimTypes.Name, user.Data.FirstName + " " + user.Data.LastName),
                                    
                };
                foreach (var role in user.Data.UserRoles)
                {
                  claims.Add(new Claim(ClaimTypes.Role, role.Name));
                 
                }
                    foreach (var claim in claims)
                    {
                        System.Console.WriteLine(claim);
                    }
                    Console.WriteLine(claims);
                    var identity = new ClaimsIdentity(
                    claims,
                    CookieAuthenticationDefaults.AuthenticationScheme
                );
                var principal = new ClaimsPrincipal(identity);
                var properties = new AuthenticationProperties();

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    principal,
                    properties
                );
                if (user.Status == true)
                {
                    if (user.Data.UserRoles.Select(r => r.Name).Contains("SuperAdmin"))
                    {
                         TempData["success"] = "Login Successfully";
                        return RedirectToAction("Super");
                    //    return RedirectToAction("SuperDash" ,"Home");
                    }
                    else if (user.Data.UserRoles.Select(r => r.Name).Contains("Customer"))
                    {
                         TempData["success"] = "Login Successfully";
                        return RedirectToAction("Customer");
                    //    return RedirectToAction("CustomerDash" ,"Home");
                    }
                    else if (user.Data.UserRoles.Select(r => r.Name).Contains("Eatery"))
                    {
                         TempData["success"] = "Login Successfully";
                          return RedirectToAction("Eatery");
                    //    return RedirectToAction("EateryDash" ,"Home");
                    }
                      else if (user.Data.UserRoles.Select(r => r.Name).Contains("Branch"))
                    {
                         TempData["success"] = "Login Successfully";
                             return RedirectToAction("Branch");
                    //    return RedirectToAction("BranchDash" ,"Home");
                    }
                    else if (user.Data.UserRoles.Select(r => r.Name).Contains("EateryManager"))
                    {
                         TempData["success"] = "Login Successfully";
                           // return RedirectToAction("GetAllEatery" , "Eatery");
                             return RedirectToAction("EateryManager");
                    }
                     else if (user.Data.UserRoles.Select(r => r.Name).Contains("BranchManager"))
                    {
                         TempData["success"] = "Login Successfully";
                       // return RedirectToAction("Super","User");
                         return RedirectToAction("BranchManager");
                    }
                     else if (user.Data.UserRoles.Select(r => r.Name).Contains("Staff"))
                    {
                         TempData["success"] = "Login Successfully";
                       // return RedirectToAction("Super","User");
                         return RedirectToAction("Staff");
                    //    return RedirectToAction("StaffDash" ,"Home");

                         
                    }
                }
            }
            return View();
        }


        public IActionResult Logout()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction("Index","Home");
            //return RedirectToAction("Login");
        }

         public async Task<IActionResult> GetAll()
        {
             var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var user = await _userServices.Get(userId);
            var users = await _userServices.GetAll();
            return View(users.Data);
        }

        public IActionResult Super()
        {
            return View();
        }
        public async Task<IActionResult>  Branch()
        {
            //  var branchId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            //  var branch = await _branchService.Get(branchId);
            //   ViewBag.branch = branch.Data;
            return View();
        }
        public async Task<IActionResult> Eatery()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var eatry = await _eateryService.GetByAdminId(userId);
            ViewBag.Eatery = eatry.Data;
            return View();
        }
        public async Task<IActionResult> Staff()
        {
            return View();
        }

        public IActionResult Home()
        {
            return View();
        }
        public async Task<IActionResult> Menu()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var eatry = await _eateryService.GetByAdminId(userId);

            ViewBag.Eatery = eatry.Data;

            return View();
        }


        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var user = await _userServices.Get(id);
             if (user.Status == true)
            {
                return View(user.Data);
            }
            return View();
        }
    }
}
