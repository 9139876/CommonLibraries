using CommonLibraries.Config.Enums;

namespace CommonLibraries.Config.Models
{
    public class GetConfigRequest
    {
        public EnvironmentEnum Environment { get; set; }

        public string Application { get; set; }
    }
}
