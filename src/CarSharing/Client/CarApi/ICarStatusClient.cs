using CarSharing.Entities.DataBaseModels;
using RestEase;
using System.Threading.Tasks;

namespace CarSharing.Client.CarApi
{
    [BasePath("carStatus")]
    public interface ICarStatusClient
    {
        [Put("reset/{id}")]
        public Task Reset([Path] int id);
    }
}
