using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RDMS.Interface.Service;

namespace RDMS.Controllers
{
    public class AdressController : Controller
    {
         private readonly IAddressServices _addressService;

        public AdressController(IAddressServices addressService)
        {
            _addressService = addressService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetAddressAsync(int id)
        {
            var address = await _addressService.Get(id);
            // if (address.Status == false)
            // {
            //     return BadRequest(address);
            // }
            return View(address);
        }


    }
}