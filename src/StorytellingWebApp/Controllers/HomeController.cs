using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using StorytellingWebApp.Models;
using System.Diagnostics;
using System.Globalization;

namespace StorytellingWebApp.Controllers
{
    public class HomeController : Controller
    {
        public  IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View();
        }
    }
}