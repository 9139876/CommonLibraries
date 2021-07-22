using System.Data.Common;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;

namespace CommonLibraries.EF
{
    public interface IBaseDbContext
    {
        IDbContextTransaction BeginTransaction();

        IDbContextTransaction BeginTransaction(System.Data.IsolationLevel isolationLevel);

        int SaveChanges();

        EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        DbConnection GetDbConnection();
    }
}
