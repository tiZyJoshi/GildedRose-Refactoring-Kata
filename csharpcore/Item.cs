namespace csharpcore
{
    public class Item
    {
        public string Name { get; set; }
        public int SellIn { get; set; }
        public int Quality { get; set; }
    }

    public interface IItem
    {
        string Name { get; }
        int SellIn { get; }
        int Quality { get; }
    }

    public interface IDegradingItem : IItem {}
    public interface IAgedItem : IItem {}
    public interface ILegendaryItem : IItem {}
    public interface IConcertTicket : IItem {}
    public interface IConjuredItem : IItem {}

    public class DegradingItem : Item, IDegradingItem {}
    public class AgedItem : Item, IAgedItem {}
    public class LegendaryItem : Item, ILegendaryItem {}
    public class ConcertTicket : Item, IConcertTicket {}
    public class ConjuredItem : Item, IConjuredItem {}
}
