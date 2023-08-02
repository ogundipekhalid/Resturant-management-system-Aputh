using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RDMS.Interface.Service;
using RDMS.Models.Dtos;

namespace RDMS.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoryservices _categoryservices;
        public CategoriesController(ICategoryservices categoryservices)
        {
            _categoryservices  = categoryservices;
        }

        public IActionResult Create()
        {
            return View();
        }

         [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryRequestseModel model)
        {
            var customer = await _categoryservices.CreateCategory(model);
            if (customer != null)
            {
                TempData["Exist"] = "  Successfully Created";
            }
                return RedirectToAction("Branch" ,"User");
        }

         [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var roles = await _categoryservices.GetAllCategory();
            if (roles.Status == true)
            {
                return View(roles.Data);
            }
            return View();
        }

        
        // [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _categoryservices.CansuleOrder(id);
            // return RedirectToAction("Menu", "User");
            return RedirectToAction("Branch" ,"User");
        }

    }
}