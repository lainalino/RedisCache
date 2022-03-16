using RedisCache.Entities;

namespace RedisCache.Repositories.Interfaces
{
    public interface IRepositoryBase<TEntity> : IDisposable where TEntity : Entity
    {
        Task<TEntity> Add(TEntity entity);
    }
}
