using Microsoft.EntityFrameworkCore;
using MusicStore.Models;
using System;
using System.Collections.Generic;

namespace MusicStore.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private DbContext _dbContext;
        private bool _disposed;

        private Dictionary<string, object> _repositories;
        public UnitOfWork(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public virtual void Dispose(bool isDisposing)
        {
            if(!_disposed && isDisposing)
            {
                _dbContext.Dispose();
            }

            _disposed = true;
        }

        public IRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            if(_repositories == null)
                _repositories = new Dictionary<string, object>();

            string typeName = typeof(TEntity).Name;

            if(!_repositories.ContainsKey(typeName))
            {   
                var repositoryType = typeof(Repository<>);
                var repositoryInstance = 
                    Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _dbContext);
                
                _repositories.Add(typeName, repositoryInstance);
            }

            return (IRepository<TEntity>)_repositories[typeName];
        }
    }
}