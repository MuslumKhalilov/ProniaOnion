using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProniaOnion.Application.Abstractions.Repositories;
using ProniaOnion.Domain.Entities;
using ProniaOnion.Persistance.Contexts;

namespace ProniaOnion.Persistance.Implementations.Repositories.Generic
{
    public class Repository<T> : IRepository<T> where T : BaseEntity, new()
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _table;

        public Repository(AppDbContext context)
        {
            _context = context;
            _table = context.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
            await _context.AddAsync(entity);
        }

        public void DeleteAsync(T entity)
        {
            _table.Remove(entity);
        }
        public void SoftDeleteAsync(T entity)
        {
            entity.IsDeleted = true;
            _table.Update(entity);
        }

        public IQueryable<T> GetAllAsync(
            Expression<Func<T, bool>>? expression = null,
            Expression<Func<T, object>>? orderExpression = null,
            bool isDescending = false,
            int skip = 0,
            int take = 0,
            bool isTracking = true,
            bool isDeleted = false,
            params string[] includes)
        {
            var query = _table.AsQueryable();
            if (expression is not null)
            {
                query = query.Where(expression);
            }
            if (orderExpression is not null)
            {
                if (isDescending == true)
                {
                    query = query.OrderBy(orderExpression);
                }
                else
                {
                    query = query.OrderByDescending(orderExpression);
                }
            }
            if (includes != null)
            {
                for (int i = 0; i < includes.Length; i++)
                {
                    query.Include(includes[i]);
                }
            }
            if (skip != 0) { query = query.Skip(skip); }
            if (take != 0) { query.Take(take); }
            //if (isDeleted is true) query = query.IgnoreQueryFilters();

            return isTracking ? query : query.AsNoTracking();
        }

        public async Task<T> GetByIDAsync(int id)
        {
            T entity = await _table.FirstOrDefaultAsync(e => e.Id == id);
            return entity;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        

        public void Update(T entity)
        {
            _table.Update(entity);
        }
    }
}
