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

        public BookController(UserManager<User> userManager, IRepository<Order> orderRepository)
        {
            _userManager = userManager;
            _orderRepository = orderRepository;
        }

        public async Task<IActionResult> Confirm(int id)
        {
            var order = await _orderRepository.GetAsync(id);
            order.Status = OrderStatus.Confirmed;

            await _orderRepository.UpdateAsync(order);
            return RedirectToAction("Requests", "Employee");
        }

        public async Task<IActionResult> Reject(int id)
        {
            var order = await _orderRepository.GetAsync(id);
            order.Status = OrderStatus.Rejected;

            await _orderRepository.UpdateAsync(order);
            return RedirectToAction("Requests", "Employee");
        }

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

            return View(requestsViewModel);
        }
    }
}
