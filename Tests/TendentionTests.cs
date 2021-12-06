using CommonLibraries.Graal.Enums;
using CommonLibraries.Graal.Models;
using System;
using Xunit;

namespace Tests
{
    public class TendentionTests
    {
        [Fact]
        public void NormalWork()
        {
            var point1 = new PriceTime()
            {
                Date = new DateTime(2000, 1, 1),
                Price = 30
            };

            var point2 = new PriceTime()
            {
                Date = new DateTime(2000, 1, 2),
                Price = 20
            };

            var point3 = new PriceTime()
            {
                Date = new DateTime(2000, 1, 3),
                Price = 30
            };

            var tendention = new Tendention();
            tendention.Add(point2);
            tendention.Add(point1);
            tendention.Add(point3);

            var points = tendention.GetPoints();
            var moves = tendention.GetMoves();

            Assert.Null(tendention.GetLastError());
            Assert.NotEmpty(points);
            Assert.NotEmpty(moves);

            Assert.Equal(TendentionPointTypeEnum.Top, points[0].TendentionPointType);
            Assert.Equal(TendentionPointTypeEnum.Bottom, points[1].TendentionPointType);
            Assert.Equal(TendentionPointTypeEnum.Top, points[2].TendentionPointType);

            Assert.Equal(moves[0].Begin, points[0]);
            Assert.Equal(moves[0].End, points[1]);
        }
    }
}
