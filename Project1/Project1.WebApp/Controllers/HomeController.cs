using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Project1.Data;
using Project1.Domain;
using Project1.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Project1.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IStoreRepository _storeRepo;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IStoreRepository storeRepo)
        {
            _storeRepo = storeRepo;
            _logger = logger;
        }

        public IActionResult Index()
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
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
