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
using RDMS.Models.Enums;
using RDMS.ViewModel;

namespace RDMS.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderSerices;
        private readonly ICustomerService _customerService;
        private readonly IFoodServices _foodService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public OrderController(IOrderService orderSerices, ICustomerService customerService, IFoodServices foodService, IHttpContextAccessor httpContextAccessor)
        {
            _orderSerices = orderSerices;
            _customerService = customerService;
            _foodService = foodService;
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult Index()
        {
            return View();
        }
       
       [HttpGet]
       //[ActionName ("Reciept")]
        public async Task<IActionResult> Reciept(int id )
        {
            var ordee = int.Parse(_httpContextAccessor.HttpContext?.User?.FindFirstValue(
                ClaimTypes.NameIdentifier)
            );
            var order = await _orderSerices.GetsingleOrder(id,ordee);
            if (order.Status == true)
            {
                return View(order.Data);
            }
            return View(order);
        }

        [ActionName("Orders")]
        public async Task<IActionResult> IndexAsync()
        {
          
            var opiy = await _orderSerices.OrdersAsync();

            return View(opiy);
        }

        public IActionResult MakeOrder(int id)
        {
            return View();
        }

        [HttpPost]
        // public async Task<IActionResult> MakeOrder(int OrderId ,List<int> CartId,OrderStatus OrderStatus,int BranchId)
        public async Task<IActionResult> MakeOrder(CreateOrderRequestModel model, int OrderId )
        {
            var user = int.Parse(User.FindFirst(
                ClaimTypes.NameIdentifier
            )?.Value);
            //var model = new CreateOrderRequestModel {};
            var order = await _orderSerices.CreateOrder(model,user);
            //return RedirectToAction("OrderDetails", new{id = order.Data.Id});
            return RedirectToAction("OrderDetails", new{id = order.Data.Id});//
            //  //new{id = id}
        }


        public async Task<IActionResult> UpdateRefrenceNumber(string ReferenceNumber)
        {
            var userId  = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var order = await _orderSerices.UpdateRefrenceNumber(userId);

            return View(order.Data);
        }

        [HttpGet]
        public async Task<IActionResult> GetOrderByUserId(int id)
        {
            var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            
            var order = await _orderSerices.GetOrderUserId(userId);
          
            return View(order);
        }

        public async Task<IActionResult> GetAll()
        {
            var cutomers = await _orderSerices.GetAllOrderFood();
            if (cutomers.Status == true)
            {
                return View(cutomers.Data);
            }
            var newCust = cutomers.Data.ToList();
            return View(newCust);
        }

        [HttpGet]
        public async Task<IActionResult> CustomerOrderbyStaff(int branchId)
        {
            
            var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            
            var allord = await  _orderSerices.GetAllOrdersWithCustomer(userId);
            return View(allord.Data);
        }

        [HttpGet]
        public async Task<IActionResult> OrderDetails(int id)
        {
            var ordee = int.Parse(_httpContextAccessor.HttpContext?.User?.FindFirstValue(
                ClaimTypes.NameIdentifier)
            );
            var order = await _orderSerices.Get(id,ordee);
            if (order.Status)
            {
                return View(order.Data);
            }
            return NotFound();
        }
        [HttpGet]
        public async Task<IActionResult> Get(int id)
        {
            var ordee = _httpContextAccessor.HttpContext?.User?.FindFirstValue(
                ClaimTypes.NameIdentifier
            );
            var order = await _orderSerices.Get(id);
          
            if (order.Status)
            {
                return View(order.Data);
            }
            return NotFound();
        }
        
        [HttpGet]
        public async Task<IActionResult> Orderstatus(int id , int staffId)
        {
            var ordee = int.Parse(_httpContextAccessor.HttpContext?.User?.FindFirstValue(
                ClaimTypes.NameIdentifier
            ));
            // var order = await _orderSerices.GetsingleOrder(ordee,id);
            var order = await _orderSerices.GetOrderoBystaff(id,ordee);
            if (order.Data != null)
            {
                return View(order.Data);
            }
            return NotFound();
        }

        
        [HttpGet]
        public async Task<IActionResult> LrgOrderstatush(int id)
        {
            var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            
            var order = await _orderSerices.GetOrderUserId(userId);
          
            return View(order.Data);
        }

        //noteOrderstatus

        public async Task<IActionResult> DeliveryStatus(int id)
        {
            // var ord = 1;
            var order = await _orderSerices.UpdateStatus(id);
            
            return RedirectToAction("CustomerOrderbyStaff");
        }

        // public async Task<IActionResult> ListOrderByBranchId(int id)
        // {
        //     var response = await _orderSerices.GetAllOrder();
        //         // return View(response.Data);
        //     var OrderList = await _orderSerices.Get(id);
        //         var ids = OrderList.Data.OrderFoods.Select(c => c.Id).ToList();
        //     var order = await _orderSerices.GetListOfOrderByBranchId(ids);
        //         return View(response.Data);
        //     //return RedirectToAction("Orders");
        // }


        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> ActualDelete(int id)
        {
            var response = await _orderSerices.DeleteAysc(id);
            return RedirectToAction("Menu", "User");
        }

    }
}
