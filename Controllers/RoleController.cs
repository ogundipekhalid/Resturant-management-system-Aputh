using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RDMS.Interface.Service;
using RDMS.Models.Dtos;

namespace RDMS.Controllers
{
    public class RoleController : Controller
    {
        private readonly IRoleServices _roleService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public RoleController(IRoleServices roleService, IWebHostEnvironment webHostEnvironment)
        {
            _roleService = roleService;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateRoleRequestModel model)
        {
            var role = await _roleService.Create(model);
            if (role.Status == true)
            {
                // return RedirectToAction("Index", "Home");
                return RedirectToAction("User", "Super");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var roles = await _roleService.GetAll();
            if (roles.Status == true)
            {
                return View(roles.Data);
            }
            return View();
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            return View();
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> ActualDelete(int id)
        {
             await _roleService.Get(id);
            return RedirectToAction("Super","User");
        }
    }
}
