using CarSharing.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public IActionResult BookedCarsList()
        {
            var car = _carRepository.GetAll().ToList();
            var make = _markRepository.GetAll().ToList();
            var carModel = _carModelRepository.GetAll().ToList();

            var carListViewModel = new List<CarListViewModel>();

            foreach (var item in car)
            {
                carListViewModel.Add(new CarListViewModel
                {
                    CarId = item.Id,
                    ReleaseDate = item.ReleaseDate,
                    ImageURL = item.ImageURL,
                    Cost = item.Cost,
                    Status = item.Status,
                    MarkName = make.Where(mke => mke.Id == item.MakeId).Select(item => item.Name).FirstOrDefault(),
                    ModelName = carModel.Where(mdl => mdl.Id == item.CarModelId).Select(item => item.Name).FirstOrDefault(),
                });
            }

            return View(carListViewModel);
        }

        public async Task<IActionResult> Reset(int id)
        {
            var car = await _carRepository.GetAsync(id);
            car.Status = 1;

            await _carRepository.UpdateAsync(car);

            return RedirectToAction("BookedCarsList", "Employee");
        }

        public async Task<IActionResult> Confirm(int id)
        {
            var order = await _orderRepository.GetAsync(id);
            order.Status = 1;

            await _orderRepository.UpdateAsync(order);
            return RedirectToAction("Requests", "Employee");
        }

        public async Task<IActionResult> Reject(int id)
        {
            var order = await _orderRepository.GetAsync(id);
            order.Status = 2;

            await _orderRepository.UpdateAsync(order);
            return RedirectToAction("Requests", "Employee");
        }

        public IActionResult AddCar()
        {
            return View();
        }


        public async Task<IActionResult> Requests()
        {
            var order = _orderRepository.GetAll().ToList();
            var requestsViewModel = new List<RequestsViewModel>();

            foreach(var item in order)
            {
                requestsViewModel.Add(new RequestsViewModel
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
