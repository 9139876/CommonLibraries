using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;

namespace CommonLibraries.Web
{
    public static class ProgramUtils
    {
        public static void RunWebhost<TStartup>(string[] args, string nlogConfigFileName = "nlog.config")
            where TStartup : class
        {
            if (args == null) throw new ArgumentNullException(nameof(args));
            if (string.IsNullOrEmpty(nlogConfigFileName))
                throw new ArgumentException("Value cannot be null or empty.", nameof(nlogConfigFileName));

            var logger = NLog.Web.NLogBuilder.ConfigureNLog(nlogConfigFileName).GetCurrentClassLogger();
            try
            {
                logger.Debug($"Start aplication");
                var webHost = CreateWebHostBuilder<TStartup>(args)
                    .Build();

                webHost.Run();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Stopped program because of exception");
                throw;
            }
            finally
            {
                NLog.LogManager.Shutdown();
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder<TStartup>(string[] args)
            where TStartup : class
        {
            return WebHost.CreateDefaultBuilder(args)
                             .UseLogging()
                             .UseStartup<TStartup>();
        }

        public static IWebHostBuilder UseLogging(this IWebHostBuilder webHostBuilder)
        {
            var nLogConfiguration = NLog.LogManager.Configuration;

            return webHostBuilder
                .ConfigureLogging(logging =>
                    {
                        logging.ClearProviders();
                        logging.SetMinimumLevel(LogLevel.Trace);
                    }
                )
                .UseNLog();
        }
    }
}
