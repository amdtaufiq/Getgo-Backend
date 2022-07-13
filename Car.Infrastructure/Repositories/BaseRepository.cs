using Car.Core.Entities;
using Car.Core.Interfaces.Repositories;
using Car.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Car.Infrastructure.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected readonly CarDbContext _ctx;
        protected readonly DbSet<T> _entities;

        public BaseRepository(CarDbContext ctx)
        {
            _ctx = ctx;
            _entities = ctx.Set<T>();
        }
        public async Task Add(T entity)
        {
            await _entities.AddAsync(entity);
        }

        public void Delete(T entity)
        {
            _entities.Update(entity);
        }

        public IEnumerable<T> GetAll()
        {
            return _entities
                .Where(x => x.IsDelete == false)
                .AsEnumerable();
        }

        public async Task<T> GetById(Guid id)
        {
            return await _entities
                .FirstOrDefaultAsync(x =>
                x.IsDelete == false &&
                x.Id == id);
        }

        public void Update(T entity)
        {
            _entities.Update(entity);
        }
    }
}
