using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;

namespace ConsoleApp.Tests
{
    [TestFixture]
    public class AgeTheInventory
    {
        private const string Sulfuras = "Sulfuras, Hand of Ragnaros";
        private const string Brie = "Aged Brie";
        private const int MaxQuality = 50;

        private GildedRose _gildedRose;
        private IList<GildedRose.Item> _inventory;
        private ItemBuilder _builder;

        [TestFixtureSetUp]
        public void SetUp()
        {
            _gildedRose = new GildedRose();
            _inventory = _gildedRose.GetInventory();
            _builder = new ItemBuilder();
        }

        [TestFixtureTearDown]
        public void TearDown()
        {
        }

        [Test]
        public void YouShouldProbablyPutTestsHere()
        {
        }

        [Test]
        public void WhenUpdatingGeneralItem_SellInShouldBeDecreasedByOne()
        {
            const int Twenty = 20;

            var generalItem = _builder.WithGeneralName().WithSellIn(Twenty).Build();

            UpdateItem(generalItem);

            generalItem.SellIn.Should().Be(Twenty - 1);
        }

        [Test]
        public void WhenUpdatingGeneralItem_AndSellInIsGreaterThanZero_QualityShouldBeDecreasedByOne()
        {
            const int GreaterThanZero = 20;

            var generalItem = _builder.WithGeneralName().WithQuality(MaxQuality).WithSellIn(GreaterThanZero).Build();

            UpdateItem(generalItem);

            generalItem.Quality.Should().Be(MaxQuality - 1);
        }

        [Test]
        public void WhenUpdatingGeneralItem_AndSellInIsLessThanOrEqualToZero_QualityShouldBeDecreasedByTwo()
        {
            const int LessThanOrEqualToZero = 0;

            var generalItem = _builder.WithGeneralName().WithQuality(MaxQuality).WithSellIn(LessThanOrEqualToZero).Build();

            UpdateItem(generalItem);

            generalItem.Quality.Should().Be(MaxQuality - 2);
        }

        [Test]
        public void WhenQualityOfItemIsZero_ShouldNotDecreaseQuality()
        {
            const int Zero = 0;

            var item = _builder.WithQuality(Zero).Build();

            UpdateItem(item);

            item.Quality.Should().Be(Zero);
        }

        [Test]
        public void WhenUpdatingSulfuras_SellInAndQuality_ShouldRemainTheSame()
        {
            const int SellIn = 0;

            var item = _builder.WithName(Sulfuras).WithQuality(MaxQuality).WithSellIn(SellIn).Build();

            UpdateItem(item);

            item.Quality.Should().Be(MaxQuality);
            item.SellIn.Should().Be(SellIn);
        }

        //[Test]
        //public void WhenUpdatingBrie_ShouldIncreaseQuality()
        //{
        //    const int Quality = 20;

        //    var brie = _builder.WithName(Brie).WithQuality(Quality).Build();

        //    UpdateItem(brie);

        //    brie.Quality.Should().Be(Quality + 1);
        //}

        [Test]
        public void WhenUpdatingBrie_WithMaxQuality_ShouldNotIncreaseQuality()
        {
            var item = _builder.WithName(Brie).WithQuality(MaxQuality).Build();

            UpdateItem(item);

            item.Quality.Should().Be(MaxQuality);
        }

        private void UpdateItem(GildedRose.Item item)
        {
            _inventory.Add(item);
            _gildedRose.UpdateQuality();
        }
    }

    class ItemBuilder
    {
        public string Name { get; set; }
        public int Quality { get; set; }
        public int SellIn { get; set; }

        public ItemBuilder()
        {
            Name = "Elixir of the Mongoose";
            Quality = 50;
            SellIn = 20;
        }

        public ItemBuilder WithGeneralName()
        {
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