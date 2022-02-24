using Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly DbContext Context;
        private readonly ILoggerManager _logger;

        public Repository(DbContext context, ILoggerManager logger)
        {
            Context = context;
            _logger = logger;
        }

        public IQueryable<T> Query() => Context.Set<T>().AsQueryable();

        public IQueryable<T> Find(Expression<Func<T, bool>> predicate) => Context.Set<T>().Where(predicate);

        public async Task<T> GetByIdAsync(int id)
        {
            return await Context.Set<T>().FindAsync(id);
        }

        public async Task<T> GetByIdAsync(long id)
        {
            return await Context.Set<T>().FindAsync(id);
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await Context.Set<T>().FindAsync(id);
        }

        public async Task<bool> AddAsync(T entity)
        {
            try
            {
                Context.Set<T>().Add(entity);
                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message}{System.Environment.NewLine}{ex.StackTrace}");
                return await Task.FromResult(false);
            }
        }

        public async Task<(bool result, T entity)> AddReturnEntityAsync(T entity)
        {
            try
            {
                Context.Set<T>().Add(entity);
                return await Task<(bool, T)>.FromResult((true, entity));
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message}{System.Environment.NewLine}{ex.StackTrace}");
                return await Task<(bool, T)>.FromResult((false, entity));
            }
        }

        public async Task<bool> DeleteAsync(T entity)
        {
            Context.Set<T>().Remove(entity);
            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            try
            {
                Context.Set<T>().Attach(entity);
                Context.Entry(entity).State = EntityState.Modified;

                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message}{System.Environment.NewLine}{ex.StackTrace}");
                return await Task.FromResult(false);
            }
        }

        public async Task<(bool result, T entity)> UpdateReturnEntityAsync(T entity)
        {
            try
            {
                Context.Set<T>().Attach(entity);
                Context.Entry(entity).State = EntityState.Modified;

                return await Task<(bool, T)>.FromResult((true, entity));
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message}{System.Environment.NewLine}{ex.StackTrace}");
                return await Task<(bool, T)>.FromResult((false, entity));
            }
        }

        public async Task<int> CountAsync()
        {
            return await Context.Set<T>().CountAsync();
        }

        public bool Exist(Expression<Func<T, bool>> predicate)
        {
            var exist = Context.Set<T>().Where(predicate);
            return exist.Any() ? true : false;
        }
    }
}
