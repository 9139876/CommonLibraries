using System;

namespace CommonLibraries.Graal.Models
{
    public class TickerAndMarket
    {
        public string MarketName { get; set; }

        public string TickerName { get; set; }

        public override bool Equals(object obj)
        {
            return obj is TickerAndMarket tickerAndMarket &&
                   MarketName == tickerAndMarket.MarketName &&
                   TickerName == tickerAndMarket.TickerName;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(MarketName, TickerName);
        }

        public static bool operator ==(TickerAndMarket left, TickerAndMarket right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(TickerAndMarket left, TickerAndMarket right)
        {
            return !(left == right);
        }
    }
}
