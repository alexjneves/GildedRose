using System;

namespace GildedRose.Console
{
	public static class ItemExtensions
	{
		public static void AdjustQuality(this GildedRose.Item item, int amount)
		{
			if (IsLegendary(item)) return;
			item.Quality = ComputeNewQuality(item.Quality, amount);
		}

		public static void SetQualityToZero(this GildedRose.Item item)
		{
			if (IsLegendary(item)) return;
			item.Quality = 0;
		}

		public static void AgeOneDay(this GildedRose.Item item)
		{
			if(!item.IsLegendary())
			{
				item.SellIn = item.SellIn - 1;
			}
		}

		public static bool IsTickets(this GildedRose.Item item)
		{
			return item.BaseName() == "Backstage passes to a TAFKAL80ETC concert";
		}

		public static bool IsCheese(this GildedRose.Item item)
		{
			return item.BaseName() == "Aged Brie";
		}

		private static string BaseName(this GildedRose.Item item)
		{
			const string conjured = "Conjured ";
			return item.Name.StartsWith(conjured) ? item.Name.Substring(conjured.Length) : item.Name;
		}

		private static bool IsLegendary(this GildedRose.Item item)
		{
			return item.Name == "Sulfuras, Hand of Ragnaros";
		}

		private static int ComputeNewQuality(int initialQuality, int amount)
		{
			int quality = initialQuality + amount;
			quality = Math.Max(0, Math.Min(50, quality));
			return quality;
		}

		public static bool IsExpired(this GildedRose.Item item)
		{
			return item.SellIn < 0;
		}
	}
}
