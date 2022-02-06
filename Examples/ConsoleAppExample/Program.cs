using System;
using CommonLibraries.ClientApplication;
using CommonLibraries.Core.Extensions;
using QuotesService.Api.Services;

namespace ConsoleAppExample
{
    class Program
    {
        static void Main(string[] args)
        {
            //ProgramUtils.PreparingServiceFactory<Startup>();

            //var service = ServicesFactory.GetInstance<IGetQuotesRemoteCallService>();

            //var a = service.GetQuotesInfo(new QuotesService.Api.Models.RequestResponse.TickerMarketTimeFrame()
            //{
            //    MarketName = "NASDAQ",
            //    TickerName = "Apple (Nasdaq)",
            //    TimeFrame = CommonLibraries.Graal.Enums.TimeFrameEnum.D1

            //}).Result;

            var type = typeof(CommonLibraries.WebApiPack.Controllers.PingApiController);


            Console.WriteLine("Hello World!");
            //Console.WriteLine(a.Serialize());
        }
    }
}
