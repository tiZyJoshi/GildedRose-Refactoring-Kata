using System;

namespace csharpcore
{
    public class Item
    {
        public string Name { get; set; }
        public int SellIn { get; set; }
        public int Quality { get; set; }
    }

    public abstract class ShopItem : Item
    {
        public static readonly int MaxQuality = 50;

        public void DecreaseSellIn()
        {
            // prevent overflow in 5.9m years!
            SellIn = Math.Max(-1, SellIn - 1);
        }
    }

    public class DegradingItem : ShopItem
    {
        protected virtual int LookUpQualityDecrease()
        {
            if (SellIn < 0)
            {
                return 2;
            }

            return 1;
        }

        public void DecreaseQuality()
        {
            Quality = Math.Max(0, Quality - LookUpQualityDecrease());
        }
    }

    public class AgedItem : ShopItem
    {
        public int LookUpQualityIncrease()
        {
            if (SellIn < 0)
            {
                return 2;
            }

            return 1;
        }

        public void AgeFurther()
        {
            Quality = Math.Min(MaxQuality, Quality + LookUpQualityIncrease());
        }
    }

    public class LegendaryItem : Item
    {
        public void IamLegend()
        {
            Quality = 80;
        }
    }

    public class ConcertTicket : ShopItem
    {
        private const int CloseSellIn = 11;
        private const int VeryCloseSellIn = 6;

        private int LookUpQualityDelta()
        {
            if (SellIn < VeryCloseSellIn)
            {
                return 3;
            }

            if (SellIn < CloseSellIn)
            {
                return 2;
            }

            return 1;
        }

        public void IncreaseQuality()
        {
            if (SellIn < 0)
            {
                Quality = 0;
                return;
            }

            Quality = Math.Min(MaxQuality, Quality + LookUpQualityDelta());
        }
    }

    public class ConjuredItem : DegradingItem
    {
        protected override int LookUpQualityDecrease()
        {
            return base.LookUpQualityDecrease()*2;
        }
    }
}
