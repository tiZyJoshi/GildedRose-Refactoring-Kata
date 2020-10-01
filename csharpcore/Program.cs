using System;
using System.Collections.Generic;

namespace csharpcore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("OMGHAI!");

            var items = new List<(IItem item, DateTime dateCreated)>{
                (item: new DegradingItem {Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20}, dateCreated:DateTime.Today),
                (item: new AgedItem {Name = "Aged Brie", SellIn = 2, Quality = 0}, dateCreated:DateTime.Today),
                (item: new DegradingItem {Name = "Elixir of the Mongoose", SellIn = 5, Quality = 7}, dateCreated: DateTime.Today),
                (item: new LegendaryItem {Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80}, dateCreated: DateTime.Today),
                (item: new LegendaryItem {Name = "Sulfuras, Hand of Ragnaros", SellIn = -1, Quality = 80}, dateCreated: DateTime.Today),
                (item: new ConcertTicket
                {
                    Name = "Backstage passes to a TAFKAL80ETC concert",
                    SellIn = 15,
                    Quality = 20
                }, dateCreated: DateTime.Today),
                (item: new ConcertTicket
                {
                    Name = "Backstage passes to a TAFKAL80ETC concert",
                    SellIn = 10,
                    Quality = 49
                }, dateCreated: DateTime.Today),
                (item: new ConcertTicket
                {
                    Name = "Backstage passes to a TAFKAL80ETC concert",
                    SellIn = 5,
                    Quality = 49
                }, dateCreated: DateTime.Today),
				// this conjured item does not work properly yet
                (item: new ConjuredItem {Name = "Conjured Mana Cake", SellIn = 3, Quality = 6}, dateCreated: DateTime.Today)
            };

            for (var i = 0; i < 31; i++)
            {
                Console.WriteLine("-------- day " + i + " --------");
                Console.WriteLine("name, sellIn, quality");
                var result = GildedRose.UpdateQuality(DateTime.Today.AddDays(i), items);
                foreach (var item in result)
                {
                    System.Console.WriteLine(item.Name + ", " + item.SellIn + ", " + item.Quality);
                }
                Console.WriteLine("");
            }
        }
    }
}
