using System;

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
