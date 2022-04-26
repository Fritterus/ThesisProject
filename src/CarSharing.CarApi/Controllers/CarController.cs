using CarSharing.DataLayer.Services.Repository;
using CarSharing.Entities.DataBaseModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarSharing.CarApi.Controllers
{
    [ApiController]
    [Route("car")]
    public class CarController : Controller
    {
        private readonly IRepository<Car> _carRepository;

        public CarController(IRepository<Car> carRepository)
        {
            _carRepository = carRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Add(Car car)
        {
            try
            {
                await _carRepository.CreateAsync(car);
                return CreatedAtAction(nameof(Get), new { id = car.Id }, car);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var car = await _carRepository.GetAsync(id);
            IActionResult result = car is null ? NotFound() : Ok(car);
            return result;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var cars = _carRepository.GetAll().ToList();
            return Ok(cars);
        }

        [HttpPut]
        public async Task<IActionResult> Update(Car car)
        {
            try
            {
                await _carRepository.UpdateAsync(car);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _carRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
