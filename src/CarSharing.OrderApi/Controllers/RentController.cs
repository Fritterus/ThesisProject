using CarSharing.DataLayer.Services.Repository;
using CarSharing.Entities;
using CarSharing.Entities.DataBaseModels;
using CarSharing.Entities.Enums;
using CarSharing.Entities.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarSharing.OrderApi.Controllers
{
    [ApiController]
    [Route("rent")]
    public class RentController : Controller
    {
        private readonly IRepository<Car> _carRepository;
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<Mark> _markRepository;
        private readonly IRepository<Model> _modelRepository;

        public RentController(IRepository<Order> orderRepository, IRepository<Car> carRepository, IRepository<Mark> markRepository, IRepository<Model> modelRepository)
        {
            _orderRepository = orderRepository;
            _carRepository = carRepository;
            _markRepository = markRepository;
            _modelRepository = modelRepository;
        }

        [HttpPut("{carId}")]
        public async Task<IActionResult> Rent(Order order, int carId)
        {
            await _orderRepository.CreateAsync(order);

            var car = await _carRepository.GetAsync(carId);
            car.Status = CarStatus.Busy;

            await _carRepository.UpdateAsync(car);

            return Ok();
        }


        [HttpGet]
        public IActionResult BookedCarsList()
        {
            var mark = _markRepository.GetAll();
            var carModel = _modelRepository.GetAll();

            var carListViewModel =
                from car in _carRepository.GetAll()
                select new CarModel
                {
                    CarId = car.Id,
                    ReleaseDate = car.ReleaseDate,
                    ImageURL = car.ImageURL,
                    Cost = car.Cost,
                    Status = car.Status,
                    MarkName = mark.FirstOrDefault(o => o.Id == car.MarkId).Name,
                    ModelName = carModel.FirstOrDefault(o => o.Id == car.CarModelId).Name,
                };

            return Ok(carListViewModel);
        }

        [HttpPut("resetCarStatus/{id}")]
        public async Task<IActionResult> ResetCarStatus(int id)
        {
            var car = await _carRepository.GetAsync(id);
            car.Status = CarStatus.Free;
            await _carRepository.UpdateAsync(car);

            return Ok();
        }

        [HttpPut("confirm/{id}")]
        public async Task<IActionResult> ConfirmCar(int id)
        {
            var order = await _orderRepository.GetAsync(id);
            order.Status = OrderStatus.Confirmed;

            await _orderRepository.UpdateAsync(order);
            return Ok();
        }

        [HttpPut("reject/{id}")]
        public async Task<IActionResult> Reject(int id)
        {
            var order = await _orderRepository.GetAsync(id);
            order.Status = OrderStatus.Rejected;

            await _orderRepository.UpdateAsync(order);
            return RedirectToAction("Requests", "Employee");
        }
    }
}
