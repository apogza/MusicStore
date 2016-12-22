using System;
using MusicStore.Models;

namespace MusicStore.Data
{
    public interface IUnitOfWork : IDisposable
    {
        void Save();
        IRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity;
    }
}