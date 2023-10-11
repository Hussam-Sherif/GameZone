using GameZone.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GameZone.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IGameServices _gameServices;

        public HomeController(ILogger<HomeController> logger, 
            IGameServices gameServices)
        {
            _logger = logger;
            _gameServices = gameServices;
        }

        public IActionResult Index()
        {
            var record = _gameServices.GetAllGames();
            return View(record);
        }

        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}