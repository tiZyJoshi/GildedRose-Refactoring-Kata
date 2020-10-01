using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

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
            var items = new List<(IItem item, DateTime dateCreated)>
            {
                (item: new DegradingItem
                {
                    Name = "+5 Dexterity Vest", 
                    SellIn = 1, 
                    Quality = 1
                }, dateCreated:DateTime.Today)
            };
            var result = GildedRose.UpdateQuality(DateTime.Today.AddDays(1), items).ToList();

            Assert.Equal(0, result[0].SellIn);
            Assert.Equal(0, result[0].Quality);
        }

        // - Once the sell by date has passed, Quality degrades twice as fast
        [Fact]
        public void TestDegradingItemSellDatePassed()
        {
            var items = new List<(IItem item, DateTime dateCreated)>
            {
                (item: new DegradingItem
                {
                    Name = "+5 Dexterity Vest",
                    SellIn = 0,
                    Quality = 2
                }, dateCreated:DateTime.Today)
            };

            var result = GildedRose.UpdateQuality(DateTime.Today.AddDays(1), items).ToList();

            Assert.Equal(0, result[0].Quality);
        }

        // - The Quality of an item is never negative
        [Fact]
        public void TestQualityNeverNegative()
        {
            var items = new List<(IItem item, DateTime dateCreated)>
            {
                (item: new DegradingItem
                {
                    Name = "+5 Dexterity Vest",
                    SellIn = 0,
                    Quality = 0
                }, dateCreated: DateTime.Today),
                (item: new AgedItem
                {
                    Name = "Aged Brie",
                    SellIn = 0,
                    Quality = 0
                }, dateCreated: DateTime.Today),
                (item: new LegendaryItem
                {
                    Name = "Sulfuras, Hand of Ragnaros",
                    SellIn = 0,
                    Quality = 0
                }, dateCreated: DateTime.Today),
                (item: new ConcertTicket
                {
                    Name = "Backstage passes to a TAFKAL80ETC concert",
                    SellIn = 0,
                    Quality = 0
                }, dateCreated: DateTime.Today),
                (item: new ConjuredItem
                {
                    Name = "Conjured Mana Cake",
                    SellIn = 0,
                    Quality = 0
                }, dateCreated: DateTime.Today)
            };

            var result = GildedRose.UpdateQuality(DateTime.Today.AddDays(1), items).ToList();

            Assert.True(result[0].Quality > -1);
            Assert.True(result[1].Quality > -1);
            Assert.True(result[2].Quality > -1);
            Assert.True(result[3].Quality > -1);
            Assert.True(result[4].Quality > -1);
        }

        // - "Aged Brie" actually increases in Quality the older it gets
        [Fact]
        public void TestAgedItem()
        {
            var items = new List<(IItem item, DateTime dateCreated)>
            {
                (item: new AgedItem
                {
                    Name = "Aged Brie",
                    SellIn = 2,
                    Quality = 1
                }, dateCreated:DateTime.Today)
            };

            var result = GildedRose.UpdateQuality(DateTime.Today.AddDays(1), items).ToList();

            Assert.Equal(2, result[0].Quality);
        }

        // - The Quality of an item is never more than 50
        // (unless its an epic)
        [Fact]
        public void TestQualityNeverGreaterMaxQuality()
        {
            var items = new List<(IItem item, DateTime dateCreated)>
            {
                (item: new DegradingItem
                {
                    Name = "+5 Dexterity Vest",
                    SellIn = 0,
                    Quality = 0
                }, dateCreated: DateTime.Today),
                (item: new AgedItem
                {
                    Name = "Aged Brie",
                    SellIn = 0,
                    Quality = 0
                }, dateCreated: DateTime.Today),
                (item: new ConcertTicket
                {
                    Name = "Backstage passes to a TAFKAL80ETC concert",
                    SellIn = 0,
                    Quality = 0
                }, dateCreated: DateTime.Today),
                (item: new ConjuredItem
                {
                    Name = "Conjured Mana Cake",
                    SellIn = 0,
                    Quality = 0
                }, dateCreated: DateTime.Today)
            };

            var result = GildedRose.UpdateQuality(DateTime.Today.AddDays(1), items).ToList();

            Assert.True(result[0].Quality <= 50);
            Assert.True(result[1].Quality <= 50);
            Assert.True(result[2].Quality <= 50);
            Assert.True(result[3].Quality <= 50);
        }

        // - "Sulfuras", being a legendary item, never has to be sold or decreases in Quality
        //Just for clarification, an item can never have its Quality increase above 50, however "Sulfuras" is a
        //legendary item and as such its Quality is 80 and it never alters.
        [Fact]
        public void TestLegendaryItem()
        {
            var items = new List<(IItem item, DateTime dateCreated)>
            {
                (item: new LegendaryItem
                {
                    Name = "Sulfuras, Hand of Ragnaros",
                    SellIn = 2,
                    Quality = 1
                }, dateCreated:DateTime.Today)
            };

            var result = GildedRose.UpdateQuality(DateTime.Today.AddDays(1), items).ToList();

            Assert.Equal(2, result[0].SellIn);
            Assert.Equal(80, result[0].Quality);
        }

        // - "Backstage passes", like aged brie, increases in Quality as its SellIn value approaches;
        [Fact]
        public void TestConcertTicketNormal()
        {
            var items = new List<(IItem item, DateTime dateCreated)>
            {
                (item: new ConcertTicket
                {
                    Name = "Backstage passes to a TAFKAL80ETC concert",
                    SellIn = 12,
                    Quality = 10
                }, dateCreated:DateTime.Today)
            };

            var result = GildedRose.UpdateQuality(DateTime.Today.AddDays(1), items).ToList();

            Assert.Equal(11, result[0].Quality);
        }

        // Quality increases by 2 when there are 10 days or less and by 3 when there are 5 days or less but
        [Fact]
        public void TestConcertTicketCloseSellIn()
        {
            var items = new List<(IItem item, DateTime dateCreated)>
            {
                (item: new ConcertTicket
                {
                    Name = "Backstage passes to a TAFKAL80ETC concert",
                    SellIn = 9,
                    Quality = 10
                }, dateCreated:DateTime.Today)
            };

            var result = GildedRose.UpdateQuality(DateTime.Today.AddDays(1), items).ToList();

            Assert.Equal(12, result[0].Quality);
        }

        // Quality drops to 0 after the concert
        [Fact]
        public void TestConcertTicketVeryCloseSellIn()
        {
            var items = new List<(IItem item, DateTime dateCreated)>
            {
                (item: new ConcertTicket
                {
                    Name = "Backstage passes to a TAFKAL80ETC concert",
                    SellIn = 4,
                    Quality = 10
                }, dateCreated:DateTime.Today)
            };

            var result = GildedRose.UpdateQuality(DateTime.Today.AddDays(1), items).ToList();

            Assert.Equal(13, result[0].Quality);
        }

        // - "Conjured" items degrade in Quality twice as fast as normal items
        [Fact]
        public void TestConjuredItem()
        {
            var items = new List<(IItem item, DateTime dateCreated)>
            {
                (item: new ConjuredItem
                {
                    Name = "Conjured Mana Cake",
                    SellIn = 1,
                    Quality = 2
                }, dateCreated:DateTime.Today)
            };

            var result = GildedRose.UpdateQuality(DateTime.Today.AddDays(1), items).ToList();

            Assert.Equal(0, result[0].Quality);
        }

        // - Once the sell by date has passed, Quality degrades twice as fast
        [Fact]
        public void TestConjuredItemSellDatePassed()
        {
            var items = new List<(IItem item, DateTime dateCreated)>
            {
                (item: new ConjuredItem
                {
                    Name = "Conjured Mana Cake",
                    SellIn = 0,
                    Quality = 4
                }, dateCreated:DateTime.Today)
            };

            var result = GildedRose.UpdateQuality(DateTime.Today.AddDays(1), items).ToList();

            Assert.Equal(0, result[0].Quality);
        }
    }
}