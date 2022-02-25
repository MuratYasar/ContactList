using Contracts;
using Entities.DataModel;
using Entities.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _dbContext;
        private readonly Dictionary<Type, object> _repositories = new Dictionary<Type, object>();
        private readonly ILoggerManager _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public Dictionary<Type, object> Repositories
        {
            get { return _repositories; }
            set { Repositories = value; }
        }

        public UnitOfWork(DbContext dbContext, ILoggerManager logger, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException("dbContext can not be null.");
            _logger = logger;
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        public IRepository<T> GetRepository<T>() where T : class
        {
            if (Repositories.Keys.Contains(typeof(T)))
            {
                return Repositories[typeof(T)] as IRepository<T>;
            }

            IRepository<T> repo = new Repository<T>(_dbContext, _logger);
            Repositories.Add(typeof(T), repo);
            return repo;
        }

        public async Task<bool> SaveChangesAsync()
        {
            try
            {
                int _save = await _dbContext.SaveChangesAsync();                

                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message}{System.Environment.NewLine}{ex.StackTrace}");
                if (ex.InnerException != null && !string.IsNullOrWhiteSpace(ex.InnerException.Message))
                {
                    _logger.LogFatal($"{ex.InnerException.Message}");
                }
                return await Task.FromResult(false);
            }
        }

        public void Rollback()
        {
            _dbContext.ChangeTracker.Entries().ToList().ForEach(x => x.Reload());
        }

        #region IDisposable Members        
        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }

            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~UnitOfWork()
        {
            Dispose(false);
        }
        #endregion

    }
}
