using ECommerce.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Repositories
{
    public interface IReadRepository<T> : IRepository<T> where T : BaseEntity
    {
        IQueryable<T> GetAll(bool isTracking = true);
        IQueryable<T> Where(Expression<Func<T, bool>> expression, bool isTracking = true);
        Task<T> SingleAsync(Expression<Func<T, bool>> expression, bool isTracking = true);
        Task<T> GetByIdAsync(string id, bool isTracking = true);
    }
}
