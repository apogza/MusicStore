using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MusicStore.Models;

namespace MusicStore.Data
{
    public interface IRepository<TEntity> where TEntity : BaseEntity 
    {
        Task<TEntity> GetById(int id, IEnumerable<string> includes = null);
        Task<IEnumerable<TEntity>> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            IEnumerable<string> includeProperties = null);
        void Insert(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        IQueryable<TEntity> Table { get;}
    }
}