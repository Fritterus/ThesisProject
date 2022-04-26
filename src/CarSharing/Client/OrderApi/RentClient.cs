using CarSharing.Entities.DataBaseModels;
using RestEase;
using System.Threading.Tasks;

namespace CarSharing.Client.OrderApi
{
    [BaseAddress("rent")]
    [Header("Content-Type", "application/json")]
    public interface RentClient
    {
        [Put("{carId}")]
        public Task RentAsync([Body] Order order, int carId);
    }
}
