using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;

namespace ConsoleApp.Tests
{
    [TestFixture]
    public class AgeTheInventory
    {
        private const int DefaultSellIn = 20;
        private const int MaxQuality = 50;
        private const string Brie = "Aged Brie";
        private const string Sulfuras = "Sulfuras, Hand of Ragnaros";
        private const string BackstagePasses = "Backstage passes to a TAFKAL80ETC concert";

        private GildedRose _gildedRose;
        private IList<GildedRose.Item> _inventory;
        private ItemBuilder _itemBuilder;

        [Test]
        public void YouShouldProbablyPutTestsHere()
        {
        }

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            _gildedRose = new GildedRose();
            _inventory = _gildedRose.GetInventory();
        }

        [SetUp]
        public void SetUp()
        {
            _itemBuilder = new ItemBuilder();
        }

        [Test]
        public void WhenUpdatingGeneralItem_ShouldDecreaseSellInByOne()
        {
            const int SellIn = DefaultSellIn;

            var item = _itemBuilder.WithGeneralName().Build();
            
            UpdateItem(item);

            item.SellIn.Should().Be(SellIn - 1);
        }

        [Test]
        public void WhenUpdatingGeneralItem_AndSellInIsGreaterThanZero_ShouldDecreaseQualityByOne()
        {
            const int GreaterThanZero = DefaultSellIn;
            const int Quality = MaxQuality;

            var item = _itemBuilder.WithGeneralName().WithSellIn(GreaterThanZero).WithQuality(Quality).Build();

            UpdateItem(item);

            item.Quality.Should().Be(Quality - 1);
        }

        [Test]
        public void WhenUpdatingGeneralItem_AndSellInIsEqualToZero_ShouldDecreaseQualityByTwo()
        {
            const int Zero = 0;
            const int Quality = MaxQuality;

            var item = _itemBuilder.WithGeneralName().WithSellIn(Zero).WithQuality(Quality).Build();

            UpdateItem(item);

            item.Quality.Should().Be(Quality - 2);
        }

        [Test]
        public void WhenUpdatingGeneralItem_AndSellInIsLessThanZero_ShouldDecreaseQualityByTwo()
        {
            const int LessThanZero = -1;
            const int Quality = MaxQuality;

            var item = _itemBuilder.WithGeneralName().WithSellIn(LessThanZero).WithQuality(Quality).Build();

            UpdateItem(item);

            item.Quality.Should().Be(Quality - 2);
        }

        [Test]
        public void WhenUpdatingGeneralItem_WithQualityOfZero_ShouldNotDecreaseQuality()
        {
            const int Zero = 0;

            var item = _itemBuilder.WithGeneralName().WithQuality(Zero).Build();

            UpdateItem(item);

            item.Quality.Should().Be(Zero);
        }

        [Test]
        public void WhenUpdatingBrie_AndQualityIsLessThanMax_ShouldIncreaseQualityByOne()
        {
            const int Quality = 20;

            var item = _itemBuilder.WithName(Brie).WithQuality(Quality).Build();

            UpdateItem(item);

            item.Quality.Should().Be(Quality + 1);
        }

        [Test]
        public void WhenUpdatingBrie_AndQualityIsMax_ShouldNotIncreaseQuality()
        {
            const int Quality = MaxQuality;

            var item = _itemBuilder.WithName(Brie).WithQuality(Quality).Build();

            UpdateItem(item);

            item.Quality.Should().Be(Quality);
        }

        [Test]
        public void WhenUpdatingBrie_AndSellInDateHasPassed_ShouldIncreaseQualityByTwo()
        {
            const int Quality = 20;
            const int Expired = 0;

            var item = _itemBuilder.WithName(Brie).WithQuality(Quality).WithSellIn(Expired).Build();

            UpdateItem(item);

            item.Quality.Should().Be(Quality + 2);
        }

        [Test]
        public void WhenUpdatingSulfuras_ShouldNotChangeSellInOrQuality()
        {
            const int Quality = 80;
            const int SellIn = 0;

            var item = _itemBuilder.WithName(Sulfuras).WithQuality(Quality).WithSellIn(SellIn).Build();

            UpdateItem(item);

            item.Quality.Should().Be(Quality);
            item.SellIn.Should().Be(SellIn);
        }

        [Test]
        public void WhenUpdatingBackstagePasses_AndSellInIsEqualToTen_ShouldIncreaseQualityByTwo()
        {
            const int Quality = 20;
            const int SellIn = 10;

            var item = _itemBuilder.WithName(BackstagePasses).WithQuality(Quality).WithSellIn(SellIn).Build();

            UpdateItem(item);

            item.Quality.Should().Be(Quality + 2);
        }

        [Test]
        public void WhenUpdatingBackstagePasses_AndSellInIsEqualToFive_ShouldIncreaseQualityByThree()
        {
            const int Quality = 20;
            const int SellIn = 5;

            var item = _itemBuilder.WithName(BackstagePasses).WithQuality(Quality).WithSellIn(SellIn).Build();

            UpdateItem(item);

            item.Quality.Should().Be(Quality + 3);
        }

        [Test]
        public void WhenUpdatingBackstagePasses_AndSellInIsEqualToZero_ShouldDecreaseQualityToZero()
        {
            const int Quality = 20;
            const int SellIn = 0;

            var item = _itemBuilder.WithName(BackstagePasses).WithQuality(Quality).WithSellIn(SellIn).Build();

            UpdateItem(item);

            item.Quality.Should().Be(0);
        }

        private void UpdateItem(GildedRose.Item item)
        {
            _inventory.Add(item);
            _gildedRose.UpdateQuality();
        }
    }

    public class ItemBuilder
    {
        private const string GeneralName = "Elixir of the Mongoose";
        private const int MaxQuality = 50;
        private const int DefaultSellIn = 20;

        public string Name { get; set; }
        public int Quality { get; set; }
        public int SellIn { get; set; }

        public ItemBuilder()
        {
            Name = GeneralName;
            Quality = MaxQuality;
            SellIn = DefaultSellIn;
        }

        public ItemBuilder WithGeneralName()
        {
            Name = GeneralName;
            return this;
        }

        public ItemBuilder WithName(string name)
        {
            Name = name;
            return this;
        }

        public ItemBuilder WithQuality(int quality)
        {
            Quality = quality;
            return this;
        }

        public ItemBuilder WithSellIn(int sellIn)
        {
            SellIn = sellIn;
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
    }
}