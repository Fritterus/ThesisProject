using CarSharing.Entities.DataBaseModels;
using CarSharing.Entities.Models;
using RestEase;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarSharing.Client.BookApi
{
    [BasePath("book")]
    [Header("Content-Type", "application/json")]
    public interface IBookClient
    {
        [Put("resetCarStatus/{id}")]
        public Task ResetCarStatus([Path] int id);

        [Put("confirm/{id}")]
        public Task Confirm([Path] int id);

        [Put("reject/{id}")]
        public Task Reject([Path] int id);

        [Get("requests")]
        public Task<List<RequestModel>> RequestsAsync();

        [Get]
        public Task<List<Car>> BookedCarsListAsync();

        [Put("rent/{carId}")]
        public Task RentAsync([Body] Order order, [Path] int carId);
    }
}
