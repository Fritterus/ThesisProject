using Microsoft.EntityFrameworkCore;

namespace CarSharing.DataLayer.Services.Repository
{
    public class Repository<T> : IRepository<T>
        where T : class, new()
    {
        private readonly Context _context;

        public Repository(Context context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task CreateAsync(T item)
        {
            await _context.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var item = await GetAsync(id);

            if (item == null)
            {
                return;
            }

            _context.Remove(item);
            await _context.SaveChangesAsync();
        }

        public IQueryable<T> GetAll()
        {
            IQueryable<T> collection = _context.Set<T>().AsNoTracking();
            return collection;
        }

        public async Task<T> GetAsync(int id)
        {
            T item = await _context.FindAsync<T>(id);
            return item;
        }

        public async Task UpdateAsync(T item)
        {
            _context.Update(item);
            await _context.SaveChangesAsync();
        }
    }
}
