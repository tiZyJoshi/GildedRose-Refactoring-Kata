using System.Collections.Generic;

namespace csharpcore
{
    public class GildedRose
    {
        readonly IList<Item> _items;
        public GildedRose(IList<Item> items)
        {
            _items = items;
        }

        public void UpdateQuality()
        {
            foreach (var item in _items)
            {
                if (item is LegendaryItem legendaryItem)
                {
                    legendaryItem.IamLegend();
                }
                if(!(item is ShopItem shopItem))
                {
                    continue;
                }
                shopItem.DecreaseSellIn();
                switch (shopItem)
                {
                    case DegradingItem degradingItem:
                        degradingItem.Degrade();
                        break;
                    case AgedItem agedItem:
                        agedItem.AgeFurther();
                        break;
                    case ConcertTicket concertTicket:
                        concertTicket.Aggrade();
                        break;
                }
            }
        }
    }
}
