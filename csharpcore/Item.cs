using System;
using System.Runtime.InteropServices;

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
        protected static readonly int MaxQuality = 50;

        public void DecreaseSellIn()
        {
            // prevent overflow in 5.9m years!
            SellIn = Math.Max(-1, SellIn - 1);
        }
    }

    public class DegradingItem : ShopItem
    {
        protected virtual int GetDegration()
        {
            if (SellIn < 0)
            {
                return 2;
            }

            return 1;
        }

        public void Degrade()
        {
            Quality = Math.Max(0, Quality - GetDegration());
        }
    }

    public class AgedItem : ShopItem
    {
        public void AgeFurther()
        {
            Quality = Math.Min(MaxQuality, Quality + 1);
        }
    }

    public class EpicItem : Item
    {
        public void BeholdMyEpicness() {}
    }

    public class ConcertTicket : ShopItem
    {
        private static readonly int CloseSellIn = 11;
        private static readonly int VeryCloseSellIn = 6;

        private int GetAggration()
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

        public void Aggrade()
        {
            if (SellIn < 0)
            {
                Quality = 0;
                return;
            }

            Quality = Math.Min(MaxQuality, Quality + GetAggration());
        }
    }

    public class ConjuredItem : DegradingItem
    {
        protected override int GetDegration()
        {
            return base.GetDegration()*2;
        }
    }
}
