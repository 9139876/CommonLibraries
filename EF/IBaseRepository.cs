using System.Threading.Tasks;

namespace CommonLibraries.EF
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        void Insert(TEntity entity);

        Task InsertAsync(TEntity entity);

        void Update(TEntity entity);

        Task UpdateAsync(TEntity entity);

        void Delete(TEntity entity);

        Task DeleteAsync(TEntity entity);
    }
}
