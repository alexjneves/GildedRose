namespace ConsoleApp.Tests
{
    public class ItemBuilder
    {
        private string Name { get; set; }
        private int SellIn { get; set; }
        private int Quality { get; set; }
        
        private bool _conjured;

        public ItemBuilder()
        {
            Name = "Elixir of the Mongoose";
            Quality = 50;
            SellIn = 20;
            _conjured = false;
        }

        public ItemBuilder WithGeneralName()
        {
            Name = "Elixir of the Mongoose";
            return this;
        }

        public ItemBuilder WithName(string name)
        {
            Name = name;
            return this;
        }

        public ItemBuilder WithSellIn(int sellIn)
        {
            SellIn = sellIn;
            return this;
        }

        public ItemBuilder WithQuality(int quality)
        {
            Quality = quality;
            return this;
        }

        public ItemBuilder Conjured()
        {
            _conjured = true;
            return this;
        }

        public GildedRose.Item Build()
        {
            if (_conjured)
            {
                Name = "Conjured " + Name;
            }

            return new GildedRose.Item
            {
                Name = Name,
                Quality = Quality,
                SellIn = SellIn
            };
        }

        public ItemBuilder Brie()
        {
            Name = "Aged Brie";
            return this;
        }

        public ItemBuilder Sulfuras()
        {
            Name = "Sulfuras, Hand of Ragnaros";
            SellIn = 0;
            Quality = 80;
            return this;
        }

        public ItemBuilder BackstagePass()
        {
            Name = "Backstage passes to a TAFKAL80ETC concert";
            return this;
        }
    }
}