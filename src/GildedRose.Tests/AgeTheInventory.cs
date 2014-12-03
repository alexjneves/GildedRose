using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;

namespace ConsoleApp.Tests
{
    [TestFixture]
    public class AgeTheInventory
    {
        private const int MaxQuality = 50;
        private const int SulfurasQuality = 80;
        private const int SulfurasSellIn = 0;

        private GildedRose _gildedRose;
        private IList<GildedRose.Item> _inventory;
        private ItemBuilder _builder;

        [Test]
        public void YouShouldProbablyPutTestsHere()
        {
        }

        [SetUp]
        public void SetUp()
        {
            _gildedRose = new GildedRose();
            _inventory = _gildedRose.GetInventory();
            _builder = new ItemBuilder();
        }

        [Test]
        public void WhenUpdatingGeneralItem_ShouldDecreaseSellInByOne()
        {
            const int SellIn = 20;

            var item = _builder.WithGeneralName().WithSellIn(SellIn).Build();

            UpdateItem(item);

            item.SellIn.Should().Be(SellIn - 1);
        }

        [Test]
        public void WhenUpdatingGeneralItem_ShouldDecreaseQualityByOne()
        {
            const int Quality = MaxQuality;

            var item = _builder.WithGeneralName().WithQuality(Quality).Build();

            UpdateItem(item);

            item.Quality.Should().Be(Quality - 1);
        }

        [Test]
        public void WhenUpdatingGeneralItem_AndSellByDateHasPassed_ShouldDecreaseQualityByTwo()
        {
            const int Quality = MaxQuality;
            const int Expired = -1;

            var item = _builder.WithGeneralName().WithQuality(Quality).WithSellIn(Expired).Build();

            UpdateItem(item);

            item.Quality.Should().Be(Quality - 2);
        }

        [Test]
        public void WhenUpdatingGeneralItem_AndQualityIsZero_ShouldNotDecreaseQuality()
        {
            const int Zero = 0;

            var item = _builder.WithGeneralName().WithQuality(Zero).Build();

            UpdateItem(item);

            item.Quality.Should().Be(Zero);
        }

        [Test]
        public void WhenUpdatingBrie_ShouldIncreaseQualityByOne()
        {
            const int Quality = 20;

            var item = _builder.Brie().WithQuality(Quality).Build();

            UpdateItem(item);

            item.Quality.Should().Be(Quality + 1);
        }

        [Test]
        public void WhenUpdatingBrie_AndSellInHasExpired_ShouldIncreaseQualityByTwo()
        {
            const int Quality = 20;
            const int Expired = -1;

            var item = _builder.Brie().WithSellIn(Expired).WithQuality(Quality).Build();

            UpdateItem(item);

            item.Quality.Should().Be(Quality + 2);
        }

        [Test]
        public void WhenUpdatingBrie_AndQualityIsMax_ShouldNotIncreaseQuality()
        {
            const int Quality = MaxQuality;

            var item = _builder.Brie().WithQuality(Quality).Build();

            UpdateItem(item);

            item.Quality.Should().Be(Quality);
        }

        [Test]
        public void WhenUpdatingSulfuras_ShouldNotAlterQuality()
        {
            var item = _builder.Sulfuras().WithQuality(SulfurasQuality).Build();

            UpdateItem(item);

            item.Quality.Should().Be(SulfurasQuality);
        }

        [Test]
        public void WhenUpdatingSulfuras_ShouldNotAlterSellIn()
        {
            var item = _builder.Sulfuras().WithSellIn(SulfurasSellIn).Build();

            UpdateItem(item);

            item.SellIn.Should().Be(SulfurasSellIn);
        }

        [Test]
        public void WhenUpdatingBackstagePasses_AndSellInGreaterThanTen_ShouldIncreaseQualityByOne()
        {
            const int Quality = 30;
            const int GreaterThanTen = 20;

            var item = _builder.BackstagePass().WithSellIn(GreaterThanTen).WithQuality(Quality).Build();

            UpdateItem(item);

            item.Quality.Should().Be(Quality + 1);
        }

        [Test]
        public void WhenUpdatingBackstagePass_AndSellInIsLessThanOrEqualToTen_ShouldIncreaseQualityByTwo()
        {
            const int Quality = 30;
            const int LessThanOrEqualToTen = 10;

            var item = _builder.BackstagePass().WithSellIn(LessThanOrEqualToTen).WithQuality(Quality).Build();

            UpdateItem(item);

            item.Quality.Should().Be(Quality + 2);
        }

        [Test]
        public void WhenUpdatingBackstagePass_AndSellInIsLessThanOrEqualToFive_ShouldIncreaseQualityByThree()
        {
            const int Quality = 30;
            const int LessThanOrEqualToFive = 5;

            var item = _builder.BackstagePass().WithSellIn(LessThanOrEqualToFive).WithQuality(Quality).Build();

            UpdateItem(item);

            item.Quality.Should().Be(Quality + 3);
        }

        [Test]
        public void WhenUpdatingBackstagePass_AndSellInHasExpired_ShouldChangeQualityToZero()
        {
            const int Quality = 30;
            const int Expired = -1;

            var item = _builder.BackstagePass().WithSellIn(Expired).WithQuality(Quality).Build();

            UpdateItem(item);

            item.Quality.Should().Be(0);
        }

        [Test]
        public void WhenUpdatingConjuredManaCake_DecreaseQualityByTwo()
        {
            const string ConjuredManaCake = "Conjured Mana Cake";
            const int SellIn = 20;
            const int Quality = 30;

            var item = _builder.WithName(ConjuredManaCake).WithQuality(Quality).WithSellIn(SellIn).Build();

            UpdateItem(item);

            item.SellIn.Should().Be(SellIn - 1);
            item.Quality.Should().Be(Quality - 1);
        }

        private void UpdateItem(GildedRose.Item item)
        {
            _inventory.Add(item);
            _gildedRose.UpdateQuality();
        }

    }

    public class ItemBuilder
    {
        public string Name { get; set; }
        public int SellIn { get; set; }
        public int Quality { get; set; }

        public ItemBuilder()
        {
            Name = "Elixir of the Mongoose";
            Quality = 50;
            SellIn = 20;
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

        public GildedRose.Item Build()
        {
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