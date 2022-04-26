using Microsoft.AspNetCore.Mvc;

namespace CarSharing.Controllers
{
    public class OrderHistoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
