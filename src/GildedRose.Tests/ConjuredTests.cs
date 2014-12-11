using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;

namespace ConsoleApp.Tests
{
    [TestFixture]
    class ConjuredTests
    {
        private const int MaxQuality = 50;
        private const int SulfurasQuality = 80;
        private const int SulfurasSellIn = 0;
        private const string ManaCake = "Mana Cake";

        private GildedRose _gildedRose;
        private IList<GildedRose.Item> _inventory;
        private ItemBuilder _builder;

        [SetUp]
        public void SetUp()
        {
            _gildedRose = new GildedRose();
            _inventory = _gildedRose.GetInventory();
            _builder = new ItemBuilder();
        }

        [Test]
        public void WhenUpdatingGeneralItem_ShouldDecreaseQualityByTwo()
        {
            const int Quality = MaxQuality;

            var item = _builder.Conjured().WithGeneralName().WithQuality(Quality).Build();

            UpdateItem(item);

            item.Quality.Should().Be(Quality - 2);
        }

        [Test]
        public void WhenUpdatingGeneralItem_AndSellByDateHasPassed_ShouldDecreaseQualityByFour()
        {
            const int Quality = MaxQuality;
            const int Expired = -1;

            var item = _builder.Conjured().WithGeneralName().WithQuality(Quality).WithSellIn(Expired).Build();

            UpdateItem(item);

            item.Quality.Should().Be(Quality - 4);
        }

        [Test]
        public void WhenUpdatingGeneralItem_AndQualityIsZero_ShouldNotDecreaseQuality()
        {
            const int Zero = 0;

            var item = _builder.Conjured().WithGeneralName().WithQuality(Zero).Build();

            UpdateItem(item);

            item.Quality.Should().Be(Zero);
        }

        [Test]
        public void WhenUpdatingBrie_ShouldIncreaseQualityByTwo()
        {
            const int Quality = 20;

            var item = _builder.Conjured().Brie().WithQuality(Quality).Build();

            UpdateItem(item);

            item.Quality.Should().Be(Quality + 2);
        }

        [Test]
        public void WhenUpdatingBrie_AndSellInHasExpired_ShouldIncreaseQualityByFour()
        {
            const int Quality = 20;
            const int Expired = -1;

            var item = _builder.Conjured().Brie().WithSellIn(Expired).WithQuality(Quality).Build();

            UpdateItem(item);

            item.Quality.Should().Be(Quality + 4);
        }

        [Test]
        public void WhenUpdatingBrie_AndQualityIsMax_ShouldNotIncreaseQuality()
        {
            const int Quality = MaxQuality;

            var item = _builder.Conjured().Brie().WithQuality(Quality).Build();

            UpdateItem(item);

            item.Quality.Should().Be(Quality);
        }

        [Test]
        public void WhenUpdatingSulfuras_ShouldNotAlterQuality()
        {
            var item = _builder.Conjured().Sulfuras().WithQuality(SulfurasQuality).Build();

            UpdateItem(item);

            item.Quality.Should().Be(SulfurasQuality);
        }

        [Test]
        public void WhenUpdatingSulfuras_ShouldNotAlterSellIn()
        {
            var item = _builder.Conjured().Sulfuras().WithSellIn(SulfurasSellIn).Build();

            UpdateItem(item);

            item.SellIn.Should().Be(SulfurasSellIn);
        }

        [Test]
        public void WhenUpdatingBackstagePasses_AndSellInGreaterThanTen_ShouldIncreaseQualityByTwo()
        {
            const int Quality = 30;
            const int GreaterThanTen = 20;

            var item = _builder.Conjured().BackstagePass().WithSellIn(GreaterThanTen).WithQuality(Quality).Build();

            UpdateItem(item);

            item.Quality.Should().Be(Quality + 2);
        }

        [Test]
        public void WhenUpdatingBackstagePass_AndSellInIsLessThanOrEqualToTen_ShouldIncreaseQualityByFour()
        {
            const int Quality = 30;
            const int LessThanOrEqualToTen = 10;

            var item = _builder.Conjured().BackstagePass().WithSellIn(LessThanOrEqualToTen).WithQuality(Quality).Build();

            UpdateItem(item);

            item.Quality.Should().Be(Quality + 4);
        }

        [Test]
        public void WhenUpdatingBackstagePass_AndSellInIsLessThanOrEqualToFive_ShouldIncreaseQualityBySix()
        {
            const int Quality = 30;
            const int LessThanOrEqualToFive = 5;

            var item = _builder.Conjured().BackstagePass().WithSellIn(LessThanOrEqualToFive).WithQuality(Quality).Build();

            UpdateItem(item);

            item.Quality.Should().Be(Quality + 6);
        }

        [Test]
        public void WhenUpdatingBackstagePass_AndSellInHasExpired_ShouldChangeQualityToZero()
        {
            const int Quality = 30;
            const int Expired = -1;

            var item = _builder.Conjured().BackstagePass().WithSellIn(Expired).WithQuality(Quality).Build();

            UpdateItem(item);

            item.Quality.Should().Be(0);
        }

        [Test]
        public void WhenUpdatingConjuredManaCake_DecreaseQualityByTwo()
        {
            const int SellIn = 20;
            const int Quality = 30;

            var item = _builder.Conjured().WithName(ManaCake).WithQuality(Quality).WithSellIn(SellIn).Build();

            UpdateItem(item);

            item.SellIn.Should().Be(SellIn - 1);
            item.Quality.Should().Be(Quality - 2);
        }

        private void UpdateItem(GildedRose.Item item)
        {
            _inventory.Add(item);
            _gildedRose.UpdateQuality();
        }
    }
}
