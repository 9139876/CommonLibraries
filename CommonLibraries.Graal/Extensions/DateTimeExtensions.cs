using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLibraries.Graal.Enums;

namespace CommonLibraries.Graal.Extensions
{
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Возвращает дату, отстоящую от заданной на указанное число единиц времени таймфрейма.
        /// </summary>
        /// <param name="date">Заданная дата.</param>
        /// <param name="timeFrame">Таймфрейм.</param>
        /// <param name="increment">Добавляемое число единиц времени таймфрейма.</param>
        /// <returns>Дата, отстоящая от заданной на указанное число единиц времени таймфрейма</returns>
        public static DateTime AddDate(this DateTime date, TimeFrameEnum timeFrame, int increment)
        {
            switch (timeFrame)
            {
                case (TimeFrameEnum.min1):
                    return date.AddMinutes(increment);
                case (TimeFrameEnum.min4):
                    return date.AddMinutes(increment * 4);
                case (TimeFrameEnum.H1):
                    return date.AddHours(increment);
                case (TimeFrameEnum.D1):
                    return date.AddDays(increment);
                case (TimeFrameEnum.W1):
                    return date.AddDays(increment * 7);
                case (TimeFrameEnum.M1):
                    return date.AddMonths(increment);
                case (TimeFrameEnum.Seasonly):
                    return date.AddMonths(increment * 3);
                case (TimeFrameEnum.Y1):
                    return date.AddYears(increment);

                default:
                    throw new ArgumentException($"Неподходящий таймфрейм - {timeFrame}", nameof(timeFrame));
            }
        }

        /// <summary>
        /// Возвращает разницу между датами в единицах времени таймфрейма.
        /// </summary>
        /// <param name="dt1">Дата 1.</param>
        /// <param name="dt2">Дата 2.</param>
        /// <param name="timeFrame">Таймфрейм.</param>
        /// <returns>Разница между датами в единицах времени таймфрейма.</returns>
        public static int DatesDifferent(DateTime dt1, DateTime dt2, TimeFrameEnum timeFrame)
        {
            var diff = dt2 - dt1;

            switch (timeFrame)
            {
                case (TimeFrameEnum.min1):
                    return (int)diff.TotalMinutes;
                case (TimeFrameEnum.min4):
                    return (int)Math.Ceiling((float)diff.TotalMinutes / 4);
                case (TimeFrameEnum.H1):
                    return (int)diff.TotalHours;
                case (TimeFrameEnum.D1):
                    return (int)diff.TotalDays;
                case (TimeFrameEnum.W1):
                    return (int)Math.Ceiling((float)diff.TotalDays / 7);
                case (TimeFrameEnum.M1):
                    return (int)Math.Ceiling((float)diff.TotalDays / 30);
                case (TimeFrameEnum.Seasonly):
                    return (int)Math.Ceiling((float)diff.TotalDays / 120);
                case (TimeFrameEnum.Y1):
                    return (int)Math.Ceiling((float)diff.TotalDays / 365);

                default:
                    throw new ArgumentException($"Неподходящий таймфрейм - {timeFrame}", nameof(timeFrame));
            }
        }
    }
}
