using Xunit;
using System.Collections.Generic;

namespace csharpcore
{
    //======================================
    //Gilded Rose Requirements Specification
    //======================================

    //Hi and welcome to team Gilded Rose. As you know, we are a small inn with a prime location in a
    //prominent city ran by a friendly innkeeper named Allison. We also buy and sell only the finest goods.
    //Unfortunately, our goods are constantly degrading in quality as they approach their sell by date. We
    //have a system in place that updates our inventory for us. It was developed by a no-nonsense type named
    //Leeroy, who has moved on to new adventures. Your task is to add the new feature to our system so that
    //we can begin selling a new category of items. First an introduction to our system:

	   // - All items have a SellIn value which denotes the number of days we have to sell the item
	   // - All items have a Quality value which denotes how valuable the item is
	   // - At the end of each day our system lowers both values for every item

    //Pretty simple, right? Well this is where it gets interesting:

	   // - Once the sell by date has passed, Quality degrades twice as fast
	   // - The Quality of an item is never negative
	   // - "Aged Brie" actually increases in Quality the older it gets
	   // - The Quality of an item is never more than 50
	   // - "Sulfuras", being a legendary item, never has to be sold or decreases in Quality
	   // - "Backstage passes", like aged brie, increases in Quality as its SellIn value approaches;
	   // Quality increases by 2 when there are 10 days or less and by 3 when there are 5 days or less but
	   // Quality drops to 0 after the concert

    //We have recently signed a supplier of conjured items. This requires an update to our system:

	   // - "Conjured" items degrade in Quality twice as fast as normal items

    //Feel free to make any changes to the UpdateQuality method and add any new code as long as everything
    //still works correctly. However, do not alter the Item class or Items property as those belong to the
    //goblin in the corner who will insta-rage and one-shot you as he doesn't believe in shared code
    //ownership (you can make the UpdateQuality method and Items property static if you like, we'll cover
    //for you).

    //Just for clarification, an item can never have its Quality increase above 50, however "Sulfuras" is a
    //legendary item and as such its Quality is 80 and it never alters.



    public class GildedRoseTest
    {
        // - All items have a SellIn value which denotes the number of days we have to sell the item
        // - All items have a Quality value which denotes how valuable the item is
        // - At the end of each day our system lowers both values for every item
        [Fact]
        public void TestDegradingItem()
        {
            IList<Item> items = new List<Item>
            {
                new DegradingItem
                {
                    Name = "foo", 
                    SellIn = 1, 
                    Quality = 1
                }
            };
            GildedRose app = new GildedRose(items);
            app.UpdateQuality();
            Assert.Equal(0, items[0].SellIn);
            Assert.Equal(0, items[0].Quality);
        }

        // - Once the sell by date has passed, Quality degrades twice as fast
        [Fact]
        public void TestDegradingItemSellDatePassed()
        {
            IList<Item> items = new List<Item>
            {
                new DegradingItem
                {
                    Name = "foo",
                    SellIn = 0,
                    Quality = 2
                }
            };
            GildedRose app = new GildedRose(items);
            app.UpdateQuality();
            Assert.Equal(0, items[0].Quality);
        }

        // - The Quality of an item is never negative
        [Fact]
        public void TestQualityNeverNegative()
        {
            IList<Item> items = new List<Item>
            {
                new DegradingItem
                {
                    Name = "foo",
                    SellIn = 0,
                    Quality = 0
                },
                new AgedItem
                {
                    Name = "foo",
                    SellIn = 0,
                    Quality = 0
                },
                new LegendaryItem
                {
                    Name = "foo",
                    SellIn = 0,
                    Quality = 0
                },
                new ConcertTicket
                {
                    Name = "foo",
                    SellIn = 0,
                    Quality = 0
                },
                new ConjuredItem
                {
                    Name = "foo",
                    SellIn = 0,
                    Quality = 0
                }
            };
            GildedRose app = new GildedRose(items);
            app.UpdateQuality();
            Assert.True(items[0].Quality > -1);
            Assert.True(items[1].Quality > -1);
            Assert.True(items[2].Quality > -1);
            Assert.True(items[3].Quality > -1);
            Assert.True(items[4].Quality > -1);
        }

        // - "Aged Brie" actually increases in Quality the older it gets
        [Fact]
        public void TestAgedItem()
        {
            IList<Item> items = new List<Item>
            {
                new AgedItem
                {
                    Name = "foo",
                    SellIn = 2,
                    Quality = 1
                }
            };
            GildedRose app = new GildedRose(items);
            app.UpdateQuality();
            Assert.Equal(2, items[0].Quality);
        }

        // - The Quality of an item is never more than 50
        // (unless its an epic)
        [Fact]
        public void TestQualityNeverGreaterMaxQuality()
        {
            IList<Item> items = new List<Item>
            {
                new DegradingItem
                {
                    Name = "foo",
                    SellIn = 0,
                    Quality = 0
                },
                new AgedItem
                {
                    Name = "foo",
                    SellIn = 0,
                    Quality = 0
                },
                new ConcertTicket
                {
                    Name = "foo",
                    SellIn = 0,
                    Quality = 0
                },
                new ConjuredItem
                {
                    Name = "foo",
                    SellIn = 0,
                    Quality = 0
                }
            };
            GildedRose app = new GildedRose(items);
            app.UpdateQuality();
            Assert.True(items[0].Quality <= ShopItem.MaxQuality);
            Assert.True(items[1].Quality <= ShopItem.MaxQuality);
            Assert.True(items[2].Quality <= ShopItem.MaxQuality);
            Assert.True(items[3].Quality <= ShopItem.MaxQuality);
        }

        // - "Sulfuras", being a legendary item, never has to be sold or decreases in Quality
        //Just for clarification, an item can never have its Quality increase above 50, however "Sulfuras" is a
        //legendary item and as such its Quality is 80 and it never alters.
        [Fact]
        public void TestEpicItem()
        {
            IList<Item> items = new List<Item>
            {
                new LegendaryItem
                {
                    Name = "foo",
                    SellIn = 2,
                    Quality = 1
                }
            };
            GildedRose app = new GildedRose(items);
            app.UpdateQuality();
            Assert.Equal(2, items[0].SellIn);
            Assert.Equal(80, items[0].Quality);
        }

        // - "Backstage passes", like aged brie, increases in Quality as its SellIn value approaches;
        [Fact]
        public void TestConcertTicketNormal()
        {
            IList<Item> items = new List<Item>
            {
                new ConcertTicket
                {
                    Name = "foo",
                    SellIn = 12,
                    Quality = 10
                }
            };
            GildedRose app = new GildedRose(items);
            app.UpdateQuality();

            Assert.Equal(11, items[0].Quality);
        }

        // Quality increases by 2 when there are 10 days or less and by 3 when there are 5 days or less but
        [Fact]
        public void TestConcertTicketCloseSellIn()
        {
            IList<Item> items = new List<Item>
            {
                new ConcertTicket
                {
                    Name = "foo",
                    SellIn = 9,
                    Quality = 10
                }
            };
            GildedRose app = new GildedRose(items);
            app.UpdateQuality();

            Assert.Equal(12, items[0].Quality);
        }

        // Quality drops to 0 after the concert
        [Fact]
        public void TestConcertTicketVeryCloseSellIn()
        {
            IList<Item> items = new List<Item>
            {
                new ConcertTicket
                {
                    Name = "foo",
                    SellIn = 4,
                    Quality = 10
                }
            };
            GildedRose app = new GildedRose(items);
            app.UpdateQuality();

            Assert.Equal(13, items[0].Quality);
        }

        // - "Conjured" items degrade in Quality twice as fast as normal items
        [Fact]
        public void TestConjuredItem()
        {
            IList<Item> items = new List<Item>
            {
                new ConjuredItem
                {
                    Name = "foo",
                    SellIn = 1,
                    Quality = 2
                }
            };
            GildedRose app = new GildedRose(items);
            app.UpdateQuality();
            Assert.Equal(0, items[0].Quality);
        }

        // - Once the sell by date has passed, Quality degrades twice as fast
        [Fact]
        public void TestConjuredItemSellDatePassed()
        {
            IList<Item> items = new List<Item>
            {
                new ConjuredItem
                {
                    Name = "foo",
                    SellIn = 0,
                    Quality = 4
                }
            };
            GildedRose app = new GildedRose(items);
            app.UpdateQuality();
            Assert.Equal(0, items[0].Quality);
        }
    }
}