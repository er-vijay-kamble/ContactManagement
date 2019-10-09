namespace ContactManagement.Storage.Repositories
{
    using ContactManagement.Storage.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Threading.Tasks;

    public class Repository<TEntity> : IRepository<TEntity>, IDisposable where TEntity : class
    {
        #region Private Fields
        private DbContext _dbContext { get; set; }
        private readonly DbSet<TEntity> _dbSet;
        #endregion
        
        #region Constructors

        public Repository(DbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TEntity>();
        }

        #endregion

        #region Public Methods

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id).ConfigureAwait(false);
        }

        public async Task<bool> InsertAsync(TEntity entity)
        {
            _dbSet.Add(entity);
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);
            return true;
        }

        public async Task<bool> UpdateAsync(TEntity entity)
        {
            _dbSet.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entityToDelete = await GetByIdAsync(id).ConfigureAwait(false);
            _dbSet.Remove(entityToDelete);
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);
            return true;
        }

        #endregion

        #region Dispose

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dbContext.Dispose();
                _dbContext = null;
            }
        }
        #endregion
    }
}
