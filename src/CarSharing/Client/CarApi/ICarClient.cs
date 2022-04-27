using CarSharing.Entities.DataBaseModels;
using RestEase;
using System.Threading.Tasks;

namespace CarSharing.Client.CarApi
{
    public interface ICarClient
    {
        [Put("reset/{id}")]
        public Task Reset([Path] int id);

        [Post]
        public Task AddAsync([Body] Car car);

        [Get("{id}")]
        public Task<Car> Get([Path] int id);

        [Get]
        public Task GetAllAsync();

        [Put]
        public Task Update([Body] Car car);

        [Delete("{id}")]
        public Task Delete([Path] int id);
    }
}
