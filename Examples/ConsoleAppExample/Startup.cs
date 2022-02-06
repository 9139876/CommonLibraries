namespace ConsoleAppExample
{
    internal class Startup : CommonLibraries.ClientApplication.ClientApplicationStartup
    {
        protected override void ConfigureServices()
        {
            //Program.Configuration = ServicesFactory.GetInstance<IConfiguration>();

            CommonLibraries.ClientApplication.ServicesFactory.RegisterAssemblyServiceAndRepositoryByMember<QuotesService.Api.PlaceboRegistration>();
        }
    }
}
