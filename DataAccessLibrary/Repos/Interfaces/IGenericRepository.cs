using System.Threading.Tasks;

namespace DataAccessLibrary.Repos.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task AddAsync(TEntity entity);

        Task DeleteAsync(TEntity entity);

    }
}
