using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using ProniaOnion.Application.Abstractions.Repositories;
using ProniaOnion.Domain.Entities;
using ProniaOnion.Persistance.Contexts;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Update(T entity)
        {
            _table.Update(entity);
        }

        public  IQueryable<T> GetAll(bool isTracking = true, bool ignoreQuery = false, params string[] includes)
        {
            IQueryable<T> query =  _table.AsQueryable();
            if (!ignoreQuery)
            {
                query = query.IgnoreQueryFilters();
            }
            return isTracking ? query : query.AsNoTracking();

        }

        public IQueryable<T> GetAllWhere(Expression<Func<T, bool>>? expression = null, Expression<Func<T, object>>? orderExpression = null, bool isDescending = false, int skip = 0, int take = 0, bool isTracking = true, bool ignoreQuery = false, params string[] includes)
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
            query = _addIncludes(query,includes);
            if (skip != 0) { query = query.Skip(skip); }
            if (take != 0) { query.Take(take); }
            //if (ignoreQuery is true) query = query.IgnoreQueryFilters();

            return isTracking ? query : query.AsNoTracking();
        }

        public async Task<T> GetByIDAsync(int id, bool isTracking = true, bool ignoreQuery = false, params string[] includes)
        {
            IQueryable<T> query = _table.Where(x => x.Id == id);
            query = _addIncludes(query,includes);
            if (!isTracking)
            {
                query = query.AsNoTracking();
            }
            if (!ignoreQuery)
            {
                query = query.IgnoreQueryFilters();
            }
            return await query.FirstOrDefaultAsync();
        }

        public async Task<T> GetByExpressionAsync(Expression<Func<T, bool>>? expression = null, bool isTracking = true, bool ignoreQuery = false, params string[] includes)
        {
            IQueryable<T> query = _table.Where(expression);
            query = _addIncludes(query,includes);
            if(!isTracking)
            {
                query = query.AsNoTracking();
            }
            if (!ignoreQuery)
            {
                query = query.IgnoreQueryFilters();
            }
            return await query.FirstOrDefaultAsync(); 
        }

        public async Task<bool> IsExistAsync(Expression<Func<T, bool>>? expression = null, bool ignoreQuery = false)
        {
            return ignoreQuery? await _table.AnyAsync(expression) : await _table.IgnoreQueryFilters().AnyAsync(expression);
        }

        public void ReverseSoftDeleteAsync(T entity)
        {
            entity.IsDeleted = false;
        }
        private IQueryable<T> _addIncludes(IQueryable<T> query,params string[] includes)
        {
            if (includes != null)
            {
                for (int i = 0; i < includes.Length; i++)
                {
                    query.Include(includes[i]);
                }
            }
            return query;
        }

        
    }
}
