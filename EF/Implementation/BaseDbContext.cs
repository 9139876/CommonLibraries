using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace CommonLibraries.EF.Implementation
{
    public abstract class BaseDbContext: DbContext, IBaseDbContext
    {
        #region ctor

        protected BaseDbContext(DbContextOptions options)
            : base(options)
        {
        }

        #endregion

        #region public methods

        public IDbContextTransaction BeginTransaction()
        {
            return Database.CurrentTransaction ?? Database.BeginTransaction();
        }

        public IDbContextTransaction BeginTransaction(System.Data.IsolationLevel isolationLevel)
        {
            return Database.CurrentTransaction ?? Database.BeginTransaction(isolationLevel);
        }

        public DbConnection GetDbConnection()
        {
            return Database.GetDbConnection();
        }

        #endregion
    }
}
