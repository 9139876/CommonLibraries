namespace CommonLibraries.BackgroundWorker.Options
{
    public class BackgroundWorkerOptionModel
    {
        public bool IsDisabled { get; set; } = false;

        public string ClassName { get; set; }

        public uint? IterationDelayInMilliseconds { get; set; }

        public uint? ListMaxDegreeOfParallelism { get; set; }

        public bool LogIterationRetries { get; set; } = true;

        public uint? IterationRetryCount { get; set; }

        public uint? IterationRetryDelayInMillisecondsStart { get; set; }

        public uint? IterationRetryDelayInMillisecondsEnd { get; set; }
    }
}
