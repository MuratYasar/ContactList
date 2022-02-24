using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> Query();

        IQueryable<T> Find(Expression<Func<T, bool>> predicate);

        Task<T> GetByIdAsync(int id);

        Task<T> GetByIdAsync(long id);

        Task<T> GetByIdAsync(Guid id);

        Task<bool> AddAsync(T entity);

        Task<(bool result, T entity)> AddReturnEntityAsync(T entity);

        Task<bool> DeleteAsync(T entity);

        Task<bool> UpdateAsync(T entity);

        Task<(bool result, T entity)> UpdateReturnEntityAsync(T entity);

        Task<int> CountAsync();

        bool Exist(Expression<Func<T, bool>> predicate);
    }
}
