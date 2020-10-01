using System;
using System.Collections.Generic;
using System.Linq;

namespace csharpcore
{
    public static class GildedRose
    {
        public static IEnumerable<IItem> UpdateQuality(DateTime date, IEnumerable<(IItem item, DateTime dateCreated)> items)
        {
            return items
                .Select(i =>
                {
                    var (item, dateCreated) = i;
                    var timeSpan = date - dateCreated;

                    return item switch
                    {
                        IDegradingItem degradingItem => CalcDegradingItem(degradingItem, timeSpan),
                        IAgedItem agedItem => CalcAgedItem(agedItem, timeSpan),
                        ILegendaryItem legendaryItem => CalcLegendaryItem(legendaryItem),
                        IConcertTicket concertTicket => CalcConcertTicket(concertTicket, timeSpan),
                        IConjuredItem conjuredItem => CalcConjuredItem(conjuredItem, timeSpan),
                        _ => item // throw new InvalidOperationException()
                    };
                });
        }

        private static int Clamp(int value, int min, int max)
        {
            return Math.Max(min, Math.Min(value, max));
        }

        private static IItem CalcDegradingItem(IItem item, TimeSpan timeSpan)
        {
            var days = timeSpan.Days;
            var daysRange = Enumerable.Range(item.SellIn - days, Math.Max(0, days)).ToList();
            var daysBeforeSellInIsZero = Enumerable.Range(0, item.SellIn).Intersect(daysRange).Count();
            var daysAfterSellInIsZero = Enumerable.Range(-days, days).Intersect(daysRange).Count();
            return new DegradingItem
            {
                Name = item.Name,
                SellIn = item.SellIn - days,
                Quality = Clamp(item.Quality - daysBeforeSellInIsZero - daysAfterSellInIsZero * 2, 0, 50)
            };
        }

        private static IItem CalcAgedItem(IItem item, TimeSpan timeSpan)
        {
            var days = timeSpan.Days;
            var daysRange = Enumerable.Range(item.SellIn - days, Math.Max(0, days)).ToList();
            var daysBeforeSellInIsZero = Enumerable.Range(0, item.SellIn).Intersect(daysRange).Count();
            var daysAfterSellInIsZero = Enumerable.Range(-days, days).Intersect(daysRange).Count();
            return new AgedItem
            {
                Name = item.Name,
                SellIn = item.SellIn - days,
                Quality = Clamp(item.Quality + daysBeforeSellInIsZero + daysAfterSellInIsZero * 2, 0, 50)
            };
        }

        private static IItem CalcLegendaryItem(IItem item)
        {
            return new LegendaryItem
            {
                Name = item.Name,
                SellIn = item.SellIn,
                Quality = 80
            };
        }

        private static IItem CalcConcertTicket(IItem item, TimeSpan timeSpan)
        {
            var days = timeSpan.Days;
            var daysRange = Enumerable.Range(item.SellIn - days, Math.Max(0, days)).ToList();
            var daysBeforeCloseSellIn = Enumerable.Range(11, Math.Max(0, item.SellIn - 11)).Intersect(daysRange).Count();
            var daysDuringCloseSellIn = Enumerable.Range(6, 5).Intersect(daysRange).Count();
            var daysDuringVeryCloseSellIn = Enumerable.Range(0, 5).Intersect(daysRange).Count();
            var daysAfterSellInIsZero = Enumerable.Range(-days, days).Intersect(daysRange).Count();
            return new ConcertTicket
            {
                Name = item.Name,
                SellIn = item.SellIn - days,
                Quality = daysAfterSellInIsZero > 0 ? 0 : Clamp(item.Quality + daysBeforeCloseSellIn + daysDuringCloseSellIn * 2 + daysDuringVeryCloseSellIn * 3, 0, 50)
            };
        }

        private static IItem CalcConjuredItem(IItem item, TimeSpan timeSpan)
        {
            var days = timeSpan.Days;
            var daysRange = Enumerable.Range(item.SellIn - days, Math.Max(0, days)).ToList();
            var daysBeforeSellInIsZero = Enumerable.Range(0, item.SellIn).Intersect(daysRange).Count();
            var daysAfterSellInIsZero = Enumerable.Range(-days, days).Intersect(daysRange).Count();
            return new ConjuredItem
            {
                Name = item.Name,
                SellIn = item.SellIn - days,
                Quality = Clamp(item.Quality - daysBeforeSellInIsZero * 2 - daysAfterSellInIsZero * 4, 0, 50)
            };
        }
    }
}
