using CarSharing.DataLayer.Services.Repository;
using CarSharing.Entities.DataBaseModels;
using CarSharing.Entities.Enums;
using CarSharing.Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CarSharing.CarApi.Controllers
{
    [ApiController]
    [Route("book")]
    public class BookController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<Mark> _markRepository;
        private readonly IRepository<Model> _modelRepository;
        private readonly IRepository<Car> _carRepository;

        public BookController(UserManager<User> userManager,
                              IRepository<Order> orderRepository,
                              IRepository<Mark> markRepository,
                              IRepository<Model> modelRepository,
                              IRepository<Car> carRepository)
        {
            _userManager = userManager;
            _orderRepository = orderRepository;
            _markRepository = markRepository;
            _modelRepository = modelRepository;
            _carRepository = carRepository;
        }

        [HttpPut("rent/{carId}")]
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

        [HttpPut("confirm/{id}")]
        public async Task<IActionResult> Confirm(int id)
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
            return Ok();
        }

        [HttpGet("requests")]
        public async Task<IActionResult> Requests()
        {
            var order = _orderRepository.GetAll().ToList();
            var requestsViewModel = new List<RequestModel>();

            foreach (var item in order)
            {
                requestsViewModel.Add(new RequestModel
                {
                    Id = item.Id,
                    StartDate = item.StartDate,
                    EndDate = item.EndDate,
                    TotalCost = item.TotalCost,
                    Status = item.Status,
                    UserName = await _userManager.GetUserNameAsync(await _userManager.FindByIdAsync(item.UserId)),
                });
            }

            return Ok(requestsViewModel);
        }
    }
}
