namespace CarSharing.DataLayer.Services.Repository
{
    public interface IRepository<T>
        where T : class, new()
    {
        IQueryable<T> GetAll();
        Task<T> GetAsync(int id);
        Task CreateAsync(T item);
        Task UpdateAsync(T item);
        Task DeleteAsync(int id);
    }
}
