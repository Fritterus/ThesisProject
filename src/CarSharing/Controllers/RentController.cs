using CarSharing.Client.BookApi;
using CarSharing.Entities.DataBaseModels;
using CarSharing.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CarSharing.OrderApi.Controllers
{

    public class RentController : Controller
    {
        private readonly IBookClient _bookClient;

        public RentController(IBookClient bookClient)
        {
            _bookClient = bookClient;
        }

        [HttpGet]
        public IActionResult Rent(int carId, decimal cost)
        {
            var viewModel = new RentCarViewModel
            {
                CarId = carId,
                Cost = cost,
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Rent(RentCarViewModel viewModel)
        {
            var order = new Order
            {
                EndDate = viewModel.EndDate,
                StartDate = viewModel.StartDate,
                CarId = viewModel.CarId,
                UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value,
                TotalCost = (viewModel.EndDate - viewModel.StartDate).Days * viewModel.Cost,
            };

            await _bookClient.RentAsync(order, viewModel.CarId);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult CalculateCost(RentCarViewModel viewModel)
        {
            viewModel.TotalCost = (viewModel.EndDate - viewModel.StartDate).Days * viewModel.Cost;

            return View("Rent", viewModel);
        }
    }
}
