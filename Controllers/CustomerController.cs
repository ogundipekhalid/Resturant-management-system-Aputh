using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RDMS.Interface.Repositry;
using RDMS.Interface.Service;
using RDMS.Models.Dtos;
using RDMS.ViewModel;

namespace RDMS.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;
        private readonly IUserRepositry _userRepositry;
        private readonly IUserServices _userServices;
        private readonly IFoodServices _foodService;
        private readonly IOrderService _orderService;
        private readonly IAddressServices _addressServices;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CustomerController(
            ICustomerService customerService,
            IUserRepositry userRepositry,
            IUserServices userServices,
            IFoodServices foodService,
            IAddressServices addressServices,
            IHttpContextAccessor httpContextAccessor
        )
        {
            _customerService = customerService;
            _userRepositry = userRepositry;
            _userServices = userServices;
            _foodService = foodService;
            _addressServices = addressServices;
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult Dash()
        {
            return View();
        }

        // [ActionName("Index")]


        [ActionName("Index")]
        public async Task<IActionResult> IndexAsyncAll()
        {
            var addres = await _addressServices.AddressOfAvaialableBranches();
            ViewData["Address"] = new SelectList(addres, "Id", "Street");
            await _foodService.UpdateFoodStatus();
            return View(await _foodService.AllAvailaleFood());
        }

        [ActionName("IndexOfFood")]
        [HttpPost]
        public async Task<IActionResult> AllFoodsByAddressIds(int id)
        {
            var result = await _foodService.BranchFoodsByAddressIds(id);
            return View(result);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(CreateCustomerRequestModel model)
        {
            var customer = await _customerService.Create(model);
            TempData["Message"] = customer.Message;
            ViewBag.customer = "customer created Successfully";
            if (customer != null)
            {
                // ViewData ["Info"] = "Already Exist";
                // TempData["Exist"] = "customer created Successfully";
                return View();
            }
            return RedirectToAction("Login", "User");
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            var customer = await _customerService.Get(userId);
            return View(customer.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, UpdateCustomerRequestModel model)
        {
            var user = _httpContextAccessor.HttpContext?.User?.FindFirstValue(
                ClaimTypes.NameIdentifier
            );
            var customer = await _customerService.UpdateCustomer(id, model);
            TempData["success"] = "profile updated Successfully.";
            return RedirectToAction("Customer", "User");
        }

        public async Task<IActionResult> GetAll()
        {
            var cutomers = await _customerService.GetAll();
            if (cutomers.Status == true)
            {
                return View(cutomers.Data);
            }
            var newCust = cutomers.Data.ToList();
            return View(newCust);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var user = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            // var customer = await _customerService.Get(int.Parse(user));
            var customer = await _customerService.Get(user);
            //  if (customer.Status == true)
            //  {
            return View(customer.Data);
            // }
            // return View();
        }

        [HttpGet]
        public async Task<IActionResult> Orderstatus(int id, int staffId)
        {
            var ordee = int.Parse(
                _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier)
            );
            // var order = await _orderSerices.GetsingleOrder(ordee,id);
            var order = await _orderService.GetOrderoBystaff(id, ordee);

            if (order.Data != null)
            {
                return View(order.Data);
            }
            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> Profile(int id)
        {
            var customer = await _customerService.Get(id);

            return View(customer.Data);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var customer = await _customerService.Get(id);
            return View(customer.Data);
            //   return View();
        }

        [Authorize]
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> SoftDelete(int id)
        {
            var response = await _customerService.Delete(id);
            // return RedirectToAction("Menu","User");
            return RedirectToAction("GetAll");
        }

        /*
         [HttpGet]
        public IActionResult FundWallet()
        {
            TempData["Updated"] = "Wallet Funded sucessful";
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> FundWallet(double price)
        {
            var user = _httpContentAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            var tickets = await _driverService.FundWallet(int.Parse(user), price);
            if (tickets.Status == true)
            {
                TempData["Updated"] = "Wallet Funded sucessful";
                return RedirectToAction("DriverBoard", "User");
            }
            else
            {
                TempData["Invalid"] = "Invalid Transaction";
                return RedirectToAction("Fundwallet");
            }
        */

        [HttpGet]
        public async Task<IActionResult> AddMoneyToWallet(int id)
        {
            var waamout = await _customerService.Get(id);
            return View(waamout.Data);
            // return View();
        }

        [HttpPost]
        public async Task<IActionResult> FundWallet(UpdateWalletRequestModel model)
        {
            // await  _customerService.AddMoneyToWallet(model.Id, model.Wallet);
            await _customerService.FundWallet(model.Id, model.Wallet);
            TempData["Updated"] = "Wallet Funded sucessful";
            return RedirectToAction("Super", "User");
        }
    }
}
