using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using System.Linq;
using MusicStore.Models;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace MusicStore.Data
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        private DbContext _dbContext;
        private DbSet<TEntity> _entities;

        public Repository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual async Task<TEntity> GetById(int id, IEnumerable<string> includes = null)
        {
            IQueryable<TEntity> query = Entities;
            if(includes != null)
            {
                foreach(string includeProperty in includes)
                    query = query.Include(includeProperty);
            }

            return await query.Where(a => a.ID == id).SingleOrDefaultAsync();
        }

        public virtual async Task<IEnumerable<TEntity>> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            IEnumerable<string> includeProperties = null)
        {
            IQueryable<TEntity> query = Entities;

            if (filter != null)
                query = query.Where(filter);

            if(includeProperties != null)
            {
                foreach(string includeProperty in includeProperties)
                    query = query.Include(includeProperty);
            }

            return (orderBy != null) ?  
                        await orderBy(query).ToListAsync() : 
                            await query.ToListAsync();
        }

        public virtual async void Insert(TEntity entity)
        {
            if(entity == null)
                throw new ArgumentNullException("To insert an entity, it cannot be null!");

            try
            {
                Entities.Add(entity);
                await _dbContext.SaveChangesAsync();
            }
            catch
            {
                throw new Exception("Error during inserting entity");
            }
        }

        public virtual async void Update(TEntity entity)
        {
            if(entity == null)
                throw new ArgumentNullException("In order to update an entity, it cannot be null");

            try
            {
                Entities.Update(entity);
                await _dbContext.SaveChangesAsync();
            }
            catch
            {
                throw new Exception("Error during updating an entity");
            }
        }

        public virtual async void Delete(TEntity entity)
        {
            if(entity == null)
                throw new ArgumentNullException("In order to delete an entity, it cannot be null");

            try
            {
                Entities.Remove(entity);
                await _dbContext.SaveChangesAsync();
            }
            catch
            {
                throw new Exception("Error during deletion of an entity");
            }
        }

        public virtual IQueryable<TEntity> Table
        {
            get
            {
                return Entities;
            }
        }

        private DbSet<TEntity> Entities
        {
            get
            {
                if(_entities == null)
                    _entities = _dbContext.Set<TEntity>();

                return _entities;
            }
        }
    }
}