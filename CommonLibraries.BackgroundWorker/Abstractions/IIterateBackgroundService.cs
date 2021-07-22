using System.Threading;
using System.Threading.Tasks;

namespace CommonLibraries.BackgroundWorker.Abstractions
{
    public interface IIterateBackgroundService
    {
        Task ExecuteIterationAsync(CancellationToken stoppingToken);
    }
}
