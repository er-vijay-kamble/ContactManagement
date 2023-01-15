namespace ContactManagement.Storage.Interfaces
{
    using System.Threading.Tasks;
    public interface IRepository<TEntity>
    {
        Task<TEntity> GetByIdAsync(int id);
        Task<bool> InsertAsync(TEntity entity);
        Task<bool> UpdateAsync(TEntity entity);
        Task<bool> DeleteAsync(int id);
    }
}