using System.Collections.Generic;

namespace csharpcore
{
    public class GildedRose
    {
        private readonly IList<Item> _items;

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
                    continue;
                }

                if(!(item is ShopItem shopItem))
                {
                    continue;
                }

                shopItem.DecreaseSellIn();

                switch (shopItem)
                {
                    case DegradingItem degradingItem:
                        degradingItem.DecreaseQuality();
                        break;
                    case AgedItem agedItem:
                        agedItem.AgeFurther();
                        break;
                    case ConcertTicket concertTicket:
                        concertTicket.IncreaseQuality();
                        break;
                }
            }
        }
    }
}
