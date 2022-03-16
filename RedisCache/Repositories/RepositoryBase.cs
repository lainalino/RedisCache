using RedisCache.Entities;
using RedisCache.Migrations;
using Microsoft.EntityFrameworkCore;
using RedisCache.Repositories.Interfaces;

namespace RedisCache.Repositories
{
    public class RepositoryBase<TModel> : IRepositoryBase<TModel> where TModel : Entity
    {
        #region Constructor

        protected DbSet<TModel> DbSet;
        protected ApplicationDbContext Db;
        protected readonly string CurrentUser;

        public RepositoryBase(ApplicationDbContext context)
        {
            Db = context;
            DbSet = Db.Set<TModel>();
        }
        #endregion

        public virtual async Task<TModel> Add(TModel entity)
        {
            await DbSet.AddAsync(entity);
            await Db.SaveChanges();
            return await GetById(entity.Id);
        }

        public virtual async Task<TModel> GetById(int id)
        {
            return await DbSet.FindAsync(id);
        }
      
        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // dispose managed state (managed objects).
                }

                // free unmanaged resources (unmanaged objects) and override a finalizer below.
                // set large fields to null.

                disposedValue = true;
            }
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
        }
        #endregion

    }
}
