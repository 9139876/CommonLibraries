using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibraries.ClientApplication
{
    public static class ProgramUtils
    {
        public static void PreparingServiceFactory<TStartup>() where TStartup : ClientApplicationStartup
        {
            ((TStartup)Activator.CreateInstance(typeof(TStartup))).UseStartup();
        }
    }
}
