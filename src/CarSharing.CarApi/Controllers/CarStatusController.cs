using CarSharing.DataLayer.Services.Repository;
using CarSharing.Entities.DataBaseModels;
using CarSharing.Entities.Enums;
using Microsoft.AspNetCore.Mvc;

namespace CarSharing.CarApi.Controllers
{
    [ApiController]
    [Route("carStatus")]
    public class CarStatusController : Controller
    {
        private readonly IRepository<Car> _carRepository;

        public CarStatusController(IRepository<Car> carRepository)
        {
            _carRepository = carRepository;
        }

        [HttpPut("reset/{id}")]
        public async Task<IActionResult> Reset(int id)
        {
            var car = await _carRepository.GetAsync(id);
            car.Status = CarStatus.Free;

            await _carRepository.UpdateAsync(car);

            return RedirectToAction("BookedCarsList", "Employee");
        }

    }
}
