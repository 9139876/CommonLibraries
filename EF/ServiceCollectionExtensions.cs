using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CommonLibraries.EF
{
    public static class ServiceCollectionExtensions
    {
        public static void RegisterDbContext<TContextService, TContextImplementation>(
            this IServiceCollection services,
            Func<string> GetConnectionString,
            ServiceLifetime serviceLifetime = ServiceLifetime.Scoped,
            ILoggerFactory loggerFactory = null)
                where TContextService : class
                where TContextImplementation : DbContext, TContextService
        {
            services.AddDbContext<TContextImplementation>(options =>
            {
                if (loggerFactory != null) { options.UseLoggerFactory(loggerFactory); }

                options.UseSqlServer(GetConnectionString());
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });

            services.AddDbContext<TContextService, TContextImplementation>(serviceLifetime);
        }

        public static void RegisterDbContext<TContextService, TContextImplementation>(
            this IServiceCollection services,
            Func<string> GetConnectionString,
            TimeSpan retryDelay,
            int retryNumber,
            ILoggerFactory loggerFactory = null,
            ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
                where TContextService : class
                where TContextImplementation : DbContext, TContextService
        {
            services.AddDbContext<TContextImplementation>(options =>
            {
                if (loggerFactory != null) { options.UseLoggerFactory(loggerFactory); }

                options.UseSqlServer(GetConnectionString(), sqlServerOptions =>
                {
                    sqlServerOptions.EnableRetryOnFailure(retryNumber, retryDelay, null);
                });
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                
            });

            services.AddDbContext<TContextService, TContextImplementation>(serviceLifetime);
        }

        public static void RegisterDbContext<TContextService, TContextImplementation>(
            this IServiceCollection services,
            Func<string> GetConnectionString,
            int commandTimeout,
            ILoggerFactory loggerFactory = null,
            ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
                where TContextService : class
                where TContextImplementation : DbContext, TContextService
        {
            services.AddDbContext<TContextImplementation>(options =>
            {
                if (loggerFactory != null) { options.UseLoggerFactory(loggerFactory); }

                options.UseSqlServer(GetConnectionString(), sqlServerOptions => 
                {
                    sqlServerOptions.CommandTimeout(commandTimeout);
                });
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });

            services.AddDbContext<TContextService, TContextImplementation>(serviceLifetime);
        }
    }
}
