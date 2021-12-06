using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibraries.Graal.Models
{
    /// <summary>
    /// Движение
    /// </summary>
    public class Move
    {
        public Move(TendentionPoint begin, TendentionPoint end)
        {
            if(end.Date <= begin.Date)
            {
                throw new InvalidOperationException($"Дата конечной точки {end.Date} должна быть больше даты начальной точки {begin.Date}");
            }

            Begin = begin;
            End = end;
        }

        /// <summary>
        /// Начало
        /// </summary>
        public TendentionPoint Begin { get; private set; }

        /// <summary>
        /// Конец
        /// </summary>
        public TendentionPoint End { get; private set; }

        public decimal PriceMove() => End.Price - Begin.Price;

        public TimeSpan TimeMove() => End.Date - Begin.Date;
    }
}
