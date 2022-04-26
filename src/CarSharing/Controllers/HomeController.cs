using CarSharing.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace CarSharing.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRepository<Car> _carRepository;
        private readonly IRepository<Mark> _markRepository;
        private readonly IRepository<Model> _carModelRepository;

        public HomeController(ILogger<HomeController> logger, IRepository<Car> carRepository, 
            IRepository<Mark> markRepository, IRepository<Model> carModelRepository)
        {
            _logger = logger;
            _carRepository = carRepository;
            _markRepository = markRepository;
            _carModelRepository = carModelRepository;
        }
        public IActionResult Index()
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
