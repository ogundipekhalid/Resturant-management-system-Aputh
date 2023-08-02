using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RDMS.Models;

namespace RDMS.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult about()
    {
        return View();
    }
    public IActionResult Order()
    {
        return View();
    }
    public IActionResult Menu()
    {
        return View();
    }
    

    public IActionResult Privacy()
    {
        return View();
    }
    public IActionResult CustomerDash()
    {
        return View();
    }

    public IActionResult EateryDash()
    {
        return View();
    }
    public IActionResult BranchDash()
    {
        return View();
    }
    public IActionResult StaffDash()
    {
        return View();
    }

    public IActionResult SuperDash()
    {
        return View();
    }

    public IActionResult Dash()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    
}
