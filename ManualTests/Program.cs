using CommonLibraries.Config;
using CommonLibraries.Graal.Enums;
using CommonLibraries.Graal.Models;
using Microsoft.Extensions.Configuration;
using System;

namespace ManualTests
{
    class Program
    {
        static void Main(string[] args)
        {
            //var Configuration = new ConfigurationBuilder()
            //    .LoadConfiguration(loadFromConfigService: true, reloadAppSettingsOnChange: true, requiredConfigService: true)
            //    .Build();

            //Console.WriteLine("Hello World!");

            var tendention = new Tendention();

            var point1 = new PriceTime()
            {

            };



            var tp = new TendentionPoint()
            {
                Date = new DateTime(2015, 10, 15),
                Price = (decimal)75.115,
                TendentionPointType = TendentionPointTypeEnum.Top
            };

            Console.WriteLine(tp);
            Console.WriteLine(tp.GetString(true, 0));
            
        }
    }
}
