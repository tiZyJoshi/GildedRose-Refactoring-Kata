using System.Collections.Generic;

namespace csharpcore
{
    public class GildedRose
    {
        IList<Item> Items;
        public GildedRose(IList<Item> Items)
        {
            this.Items = Items;
        }

        public void UpdateQuality()
        {
            foreach (var item in Items)
            {
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
