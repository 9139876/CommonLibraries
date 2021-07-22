using System.Collections.Generic;

namespace CommonLibraries.BackgroundWorker.Models
{
    public class PingApiResponse
    {
        public List<BackgroundWorkerPingModel> Services { get; set; } = new List<BackgroundWorkerPingModel>();
    }
}
