using CommonLibraries.Core.Models;
using CommonLibraries.Graal.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibraries.Graal.Models
{
    public class Tendention
    {
        [JsonProperty]
        private LinkedList<TendentionPoint> _tendentionPoints;

        private bool _needNormalize = false;

        private TendentionPoint _lastError;

        public Tendention()
        {
            _tendentionPoints = new();
        }

        public StandartResponse Add(PriceTime priceTime)
        {
            _needNormalize = true;

            var point = new TendentionPoint()
            {
                Date = priceTime.Date,
                Price = priceTime.Price
            };

            var successResult = new StandartResponse() { IsSuccess = true };

            if (!_tendentionPoints.Any())
            {
                _tendentionPoints.AddLast(point);
                return successResult;
            }

            if (_tendentionPoints.Last.Value.Date < point.Date)
            {
                _tendentionPoints.AddLast(point);
                return successResult;
            }

            if (_tendentionPoints.First.Value.Date > point.Date)
            {
                _tendentionPoints.AddFirst(point);
                return successResult;
            }

            if (_tendentionPoints.Any(x => x.Date == point.Date))
            {
                return new StandartResponse()
                {
                    IsSuccess = false,
                    Message = $"Тенденция уже имеет точку с датой {point.Date}"
                };
            }

            var current = _tendentionPoints.First;

            while (current != _tendentionPoints.Last)
            {
                if (point.Date > current.Value.Date && point.Date < current.Next.Value.Date)
                {
                    _tendentionPoints.AddAfter(current, point);

                    return successResult;
                }

                current = current.Next;
            }

            return new StandartResponse()
            {
                IsSuccess = false,
                Message = $"Неизвестная ошибка - не удалось добавить точку {point}"
            };
        }

        public StandartResponse Remove(TendentionPoint point)
        {
            if (_tendentionPoints.Contains(point))
            {
                _tendentionPoints.Remove(point);
                _needNormalize = true;

                return new StandartResponse() { IsSuccess = true };
            }

            return new StandartResponse()
            {
                IsSuccess = false,
                Message = $"Тенденция не содержит точку {point}"
            };
        }

        public TendentionPoint[] GetPoints()
        {
            Normalize();

            return IsNormal() ? _tendentionPoints.ToArray() : null;
        }

        public List<Move> GetMoves()
        {
            var moves = new List<Move>();

            var points = GetPoints();

            if (points == null || points.Length < 2)
            {
                return moves;
            }

            for (int i = 0; i < points.Length - 1; i++)
            {
                for (int j = i + 1; j < points.Length; j++)
                {
                    moves.Add(new Move(points[i], points[j]));
                }
            }

            return moves;
        }

        public TendentionPoint GetLastError()
        {
            if (_needNormalize)
            {
                Normalize();
            }

            return _lastError;
        }

        public bool IsNormal() => _lastError == null;

        private void Normalize()
        {
            if (_needNormalize == false)
            {
                return;
            }

            _lastError = null;

            if (_tendentionPoints.Count < 2)
            {
                _needNormalize = false;
                return;
            }

            var current = _tendentionPoints.First;

            if (current.Value.Price > current.Next.Value.Price)
            {
                current.Value.TendentionPointType = TendentionPointTypeEnum.Top;
            }
            else if (current.Value.Price < current.Next.Value.Price)
            {
                current.Value.TendentionPointType = TendentionPointTypeEnum.Bottom;
            }
            else
            {
                _lastError = current.Next.Value;
                return;
            }

            while (current != _tendentionPoints.Last)
            {
                if (current.Value.TendentionPointType == TendentionPointTypeEnum.Top && current.Value.Price > current.Next.Value.Price)
                {
                    current.Next.Value.TendentionPointType = TendentionPointTypeEnum.Bottom;
                }
                else if (current.Value.TendentionPointType == TendentionPointTypeEnum.Bottom && current.Value.Price < current.Next.Value.Price)
                {
                    current.Next.Value.TendentionPointType = TendentionPointTypeEnum.Top;
                }
                else
                {
                    _lastError = current.Next.Value;
                    return;
                }

                current = current.Next;
            }

            _needNormalize = false;
        }
    }
}
