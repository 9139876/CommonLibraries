using System.Collections.Generic;

namespace CommonLibraries.Config.Models
{
    public class GetConfigResponse
    {
        public List<ConfigRow> ConfigRows { get; set; } = new List<ConfigRow>();
    }
}
