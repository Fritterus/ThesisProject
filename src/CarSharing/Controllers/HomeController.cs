using CarSharing.Client.BookApi;
using CarSharing.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading.Tasks;

namespace CarSharing.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBookClient _bookClient;
        private readonly ILogger<HomeController> _logger;

        public HomeController(IBookClient bookClient, ILogger<HomeController> logger)
        {
            _bookClient = bookClient;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var bookedCar = await _bookClient.BookedCarsListAsync();

            return View(bookedCar);
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
