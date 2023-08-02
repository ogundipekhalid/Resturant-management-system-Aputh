using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using RDMS.Interface.Service;
using RDMS.Models.Dtos;
using RDMS.Models.Entity;

namespace RDMS.Controllers
{
    public class FoodController : Controller
    {
        private readonly IFoodServices _foodSerices;
        private readonly IAddressServices _addressServices;
        private readonly ICategoryservices _categoryservices;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public FoodController(IFoodServices foodSerices, IAddressServices addressServices, ICategoryservices categoryservices, IHttpContextAccessor httpContextAccessor)
        {
            _foodSerices = foodSerices;
            _addressServices = addressServices;
            _categoryservices = categoryservices;
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult Index()
        {
            return View();
        }

           public async Task<IActionResult> GetAllFoodFromLocation(int location)
        {
            // var locations = await _foodSerices.BranchFoodsByAddressId(location);
            var locations = await _addressServices.GetAdds(location);
            // var locations = await _foodSerices.BranchFoodsByAddressId(location);
            return View(location);
        }

        public async Task<IActionResult> Search(string searchTerm)
        {
            var searchResults = await _foodSerices.SearchFoods(searchTerm);

            return View(searchResults); 
        }

        public  async Task<IActionResult> AddFood()
        {
              var food =   await _categoryservices.GetAllCategory();
                return View(food);
        }

        [HttpPost]
        public async Task<IActionResult> AddFood(int id, CreateFoodRequestModel model)
        {
         var managerId = int.Parse(_httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier) );
            var food = await _foodSerices.AddFood(model, managerId);
            TempData["Message"] = food.Message;
            ViewBag.food = "customer created Successfully";
            if (food.Status)
            {
                return RedirectToAction("Branch" , "User");
            }
                return View(food);
        }
    

        [ActionName("DeleteFood")]
        public async Task<IActionResult> DeleteFoodAsync(int id)
        {
            
            var ans = await _foodSerices.DeleteFood(id);
            
           return RedirectToAction("Foods");
        }

        [ActionName("Foods")]
        public async Task<IActionResult> Foods()
        {
            await _foodSerices.UpdateFoodStatus();
            // var food = await _foodSerices.AllAvailaleFood();
           var food = await _foodSerices.AllAvailaleFood();
           return View(food);
        }

        public async Task<IActionResult> SearchFood(string addressId)
        {
            var food = await _foodSerices.BranchFoodsByAddressId(addressId);
            return View(food.Data);
        }

       
        [ActionName("FoodsAsycn")]
        public async Task<IActionResult> AllFood(int id)
        {
            await _foodSerices.UpdateFoodStatus();
            var food = await _foodSerices.AllFoodByaBranch(id);
            return View(food.Data);
        }


        [ActionName("EditFood")]
        public async Task<IActionResult> EditFood(UpdateFoodRequestModel model, int id)
        {
            var ans = await _foodSerices.UpdateFood(model, id);

            var result = await _foodSerices.GetFood(id);
            if (result.Status == false)
            {
                return Content(result.Message);
            }
            return View(result);
        }


        [HttpGet]
        [ActionName("Food/Get")]
        public async Task<IActionResult> Get( int id)
        {
            var ordee = int.Parse(_httpContextAccessor.HttpContext?.User?.FindFirstValue(
                ClaimTypes.NameIdentifier)
            );
           // var order = await _foodSerices.Get(order,id,customerId);
            var order = await _foodSerices.Get(ordee,id);
          
            if (order.Status)
            {
                return View(order.Data);
            }
                //return RedirectToAction("");
                return NotFound();
        }
        [HttpGet]
        public async Task<IActionResult> GetList( int id)
        {
            var ordee = int.Parse(_httpContextAccessor.HttpContext?.User?.FindFirstValue(
                ClaimTypes.NameIdentifier)
            );
            var order = await _foodSerices.GetList(id,ordee);
           // var order = await _foodSerices.GetList(id,customerId);
          if (order.Status == true)
            {
                return View(order.Data);
            }
            var ord = order.Data.ToList();
            return View(ord);
         
        }


        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var customer = await _foodSerices.GetFood(id);
            return View(customer.Data);
        }



        [HttpPost]
        public async Task<IActionResult> Update(int id, UpdateFoodRequestModel model)
        {
            var user = _httpContextAccessor.HttpContext?.User?.FindFirstValue(
                ClaimTypes.NameIdentifier
            );
            var customer = await _foodSerices.UpdateFood(model,id);//
            TempData["success"] = " updated Successfully.";
           // return RedirectToAction("Branch","User");
            return RedirectToAction("Foods");
           
        }


        [Authorize]
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> ActualDelete(int id)
        {
            var order = await _foodSerices.DeleteFoodAsync(id);
            return View(order.Data);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var food = await _foodSerices.GetFood(id);
            return RedirectToAction("AllFood");
        }
    }
}
