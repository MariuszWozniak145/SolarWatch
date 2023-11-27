using Microsoft.AspNetCore.Mvc;

namespace SolarWatchMVC.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}