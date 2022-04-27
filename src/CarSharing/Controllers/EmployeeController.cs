using CarSharing.Client.BookApi;
using CarSharing.Client.CarApi;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CarSharing.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ICarClient _carClient;
        private readonly IBookClient _bookClient;

        public EmployeeController(ICarClient carClient, IBookClient bookClient)
        {
            _carClient = carClient;
            _bookClient = bookClient;
        }

        public async Task <IActionResult> BookedCarsList()
        {
            var carList = await _bookClient.BookedCarsListAsync();

            return View(carList);
        }

        public async Task<IActionResult> Reset(int id)
        {
            await _carClient.Reset(id);
            return RedirectToAction("BookedCarsList", "Employee");
        }

        public async Task<IActionResult> Confirm(int id)
        {
            await _bookClient.Confirm(id);
            return RedirectToAction("Requests", "Employee");
        }

        public async Task<IActionResult> Reject(int id)
        {
            await _bookClient.Reject(id);
            return RedirectToAction("Requests", "Employee");
        }

        public IActionResult AddCar()
        {
            return View();
        }


        public async Task<IActionResult> Requests()
        {
            var requestsModel = await _bookClient.RequestsAsync();

            return View(requestsModel);
        }

    }
}
