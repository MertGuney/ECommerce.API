using ECommerce.Application.Repositories;
using ECommerce.Domain.Entities.Common;
using ECommerce.Persistance.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ECommerce.Persistance.Repositories
{
    public class ReadRepository<T> : IReadRepository<T> where T : BaseEntity
    {
        private readonly ECommerceDbContext _context;

        public ReadRepository(ECommerceDbContext context)
        {
            _context = context;
        }

        public DbSet<T> Table => _context.Set<T>();

        public IQueryable<T> GetAll(bool isTracking = true) => isTracking ? Table : Table.AsNoTracking();

        public async Task<T> GetByIdAsync(string id, bool isTracking = true) => isTracking ? await Table.FindAsync(id) : await Table.AsNoTracking().FirstOrDefaultAsync(x => x.Id == Guid.Parse(id));

        public async Task<T> SingleAsync(Expression<Func<T, bool>> expression, bool isTracking = true) => isTracking ? await Table.SingleAsync(expression) : await Table.AsNoTracking().SingleAsync(expression);

        public IQueryable<T> Where(Expression<Func<T, bool>> expression, bool isTracking = true) => isTracking ? Table.Where(expression) : Table.AsNoTracking().Where(expression);
    }
}
