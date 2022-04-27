using CarSharing.DataLayer.Services.Repository;
using CarSharing.EmployeeApi.Client.CarApi;
using CarSharing.Entities;
using CarSharing.Entities.DataBaseModels;
using CarSharing.Entities.Enums;
using CarSharing.Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CarSharing.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<Car> _carRepository;
        private readonly IRepository<Mark> _markRepository;
        private readonly IRepository<Model> _carModelRepository;

        public EmployeeController(UserManager<User> userManager, IRepository<Order> orderRepository, IRepository<Car> carRepository,
           IRepository<Mark> markRepository, IRepository<Model> carModelRepository)
        {
            _userManager = userManager;
            _orderRepository = orderRepository;
            _carRepository = carRepository;
            _markRepository = markRepository;
            _carModelRepository = carModelRepository;
        }

        [HttpPut]
        public IActionResult BookedCarsList()
        {
            var car = _carRepository.GetAll().ToList();
            var mark = _markRepository.GetAll().ToList();
            var carModel = _carModelRepository.GetAll().ToList();

            var carListViewModel = new List<CarModel>();

            foreach (var item in car)
            {
                carListViewModel.Add(new CarModel
                {
                    CarId = item.Id,
                    ReleaseDate = item.ReleaseDate,
                    ImageURL = item.ImageURL,
                    Cost = item.Cost,
                    Status = item.Status,
                    MarkName = mark.Where(mke => mke.Id == item.MarkId).Select(item => item.Name).FirstOrDefault(),
                    ModelName = carModel.Where(mdl => mdl.Id == item.CarModelId).Select(item => item.Name).FirstOrDefault(),
                });
            }

            return View(carListViewModel);
        }

        [HttpPut]
        public async Task<IActionResult> Confirm(int id)
        {
            var order = await _orderRepository.GetAsync(id);
            order.Status = OrderStatus.Confirmed;

            await _orderRepository.UpdateAsync(order);
            return Ok();
        }

        public async Task<IActionResult> Reject(int id)
        {
            var order = await _orderRepository.GetAsync(id);
            order.Status = OrderStatus.Awaiting;

            await _orderRepository.UpdateAsync(order);
            return Ok();
        }

        public async Task<IActionResult> Requests()
        {
            var order = _orderRepository.GetAll().ToList();
            var requestsViewModel = new List<RequestModel>();

            foreach(var item in order)
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

            return View(requestsViewModel);
        }

    }
}
