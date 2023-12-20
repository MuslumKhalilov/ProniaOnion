using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ProniaOnion.Domain.Entities;

namespace ProniaOnion.Application.Abstractions.Repositories
{
    public interface IRepository<T> where T : BaseEntity, new()   
    {
        IQueryable<T> GetAllAsync(
          Expression<Func<T, bool>>? expression = null,
          Expression<Func<T, object>>? orderExpression = null,
          bool isDescending = false,
          int skip = 0,
          int take = 0,
          bool isTracking = true,
          params string[] includes);

        Task<T> GetByIDAsync(int id);
        Task AddAsync(T entity);
        void DeleteAsync(T entity);
        void Update(T entity);
        Task SaveChangesAsync();
    }
}
