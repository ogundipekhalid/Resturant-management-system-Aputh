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
   
    public class CartItemController : Controller
    {
        private readonly ICartItemServices _cartItemService;

        public CartItemController( ICartItemServices cartItemService)
        {
            _cartItemService = cartItemService;
        }

        public IActionResult Index()
        {
            return View();
        }
        // [ActionName ("/CartItem/Model")]
         public IActionResult Pass()
        {
            // List<string> newList = new List<string>();
            // var invoiceStatus = ViewBag.InvoiceStatus ?? newList;
            return View();
        }
        

         [HttpPost]
        // [Route("/CartItem/Add")]
        public async Task<IActionResult> Add(AddCartItemViewModel model)
        {            

            if (ModelState.IsValid)
            {
                 var response = await _cartItemService.AddCartItem(model);
                return Json(response);
            }

            return Json(new{
                Status = false,
                Message = "Input error"
            });

            // return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddCart(AddCartItemViewModel model)
        {
            // var auth = _cartItemService.getLoginUser();
            // ViewBag.user = auth;
            

            if (ModelState.IsValid)
            {
                var response = await _cartItemService.AddCartItem( model);
                if(response.Status)
                {
                    return RedirectToAction("List", new{id = response.Data.EateryId});
                }

            }

            return View(model);
        }

         [HttpPost]
        // [Route("/CartItem/Remove")]
        public IActionResult Remove(int id)
        {
            // var auth = _appAuthentication.getLoginUser();
            // ViewBag.user = auth;
            
            var response = _cartItemService.RemoveCartItem(id);
            return Json(response);
        }

        public IActionResult Details(int id)
        {
            var response = _cartItemService.GetCartItem(id);
            if(response.Status)
            {
                return View(response.Data);
            }
            
            return View();
        }

        [HttpGet]
        public IActionResult GetAll(int id)
        {
            
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var response = _cartItemService.GetCartItemByUserId(userId);
            if(response.Status)
            {
                return View(Ok(new{
                    TotalItem = response.Data.Select(c => c.Quantity).Sum(),
                    TotalPrice = Math.Round(response.Data.Select(c => c.Total).Sum(), 2),
                    Data = response.Data,
                }));
            }
            
            return Json("");
        }

        [HttpGet]
        public IActionResult List(int id)
        {
            
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var response = _cartItemService.GetCartItemByUserId(userId);
            if(response.Status)
            {
                return Json(Ok(new{
                    TotalItem = response.Data.Select(c => c.Quantity).Sum(),
                    TotalPrice = Math.Round(response.Data.Select(c => c.Total).Sum(), 2),
                    Data = response.Data,
                }));
            }
            
            return Json("");
        }
        [HttpGet]
        public IActionResult ListCart(int id)
        {
            
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var response = _cartItemService.GetCartItemByBranchId(id);
            if(response.Status == true)
            {
                return View(response.Data);
            }
            
            return Json("");
    }

         [HttpGet]
        public IActionResult Update(int id)
        {
          
            var response = _cartItemService.GetCartItem(id);
    
            return View(response.Data);
        }

        
          [HttpPost]
        public IActionResult Update(UpdateCartItemViewModel model)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            if (ModelState.IsValid)
            {
                var response = _cartItemService.Update(userId, model);
                if(response.Status)
                {
                    return RedirectToAction("List", new{id = response.Data.EateryId});
                }

            }

            return RedirectToAction("Update", new{id = model.Id});
        }

        [HttpGet]
        public IActionResult GetAllLIST()
        {
            
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var response = _cartItemService.GetAllCartItem();
            if(response.Status == true)
            {
                return View(Ok(new{
                    TotalItem = response.Data.Select(c => c.Quantity).Sum(),
                    TotalPrice = Math.Round(response.Data.Select(c => c.Total).Sum(), 2),
                    Data = response.Data,
                }));
            }
            
            return Json("");
        }
        

    }
}