/*
 * Hi and welcome to team Gilded Rose. As you know, we are a small inn with a prime location in a prominent city
 * run by a friendly innkeeper named Allison. We also buy and sell only the finest goods. Unfortunately, our
 * goods are constantly degrading in quality as they approach their sell by date. We have a system in place that
 * updates our inventory for us. It was developed by a no-nonsense type named Leeroy, who has moved on to new
 * adventures. Your task is to add the new feature to our system so that we can begin selling a new category of
 * items. First an introduction to our system:

	- All items have a SellIn value which denotes the number of days we have to sell the item
	- All items have a Quality value which denotes how valuable the item is
	- At the end of each day our system lowers both values for every item

 * Pretty simple, right? Well this is where it gets interesting:

	- Once the sell by date has passed, Quality changes twice as fast
	- The Quality of an item is never negative
	- "Aged Brie" actually increases in Quality the older it gets
	- The Quality of an item is never more than 50
	- "Sulfuras", being a legendary item, never has to be sold or changes in Quality
	- "Backstage passes", like aged brie, increases in Quality as it's SellIn value approaches; Quality
		increases by 2 when there are 10 days or less and by 3 when there are 5 days or less but Quality
		drops to 0 after the concert

 * We have recently signed a supplier of conjured items. This requires an update to our system:

	- "Conjured" items change in Quality twice as fast as normal items. Conjured is an adjective; it can
		be applied to any item to make it conjured (for example, "Conjured Mana Cakes"). This modifies
		the normal rule for that item.

 * Feel free to make any changes to the UpdateQuality method and add any new code as long as everything
 * still works correctly. However, do not alter the Item class or Items property as those belong to the
 * goblin in the corner who will insta-rage and one-shot you as he doesn't believe in shared code ownership
 * (we have kindly placed this code into HereBeDragons.cs, so that you won't accidentally change it. This
 * also makes it easier for the goblin to tell when to insta-rage). Your work needs to be completed by
 * 30 minutes ago, but we will accept 30 minutes from now.

 * Just for clarification, an item can never have its Quality increase above 50, however "Sulfuras" is a
 * legendary item and as such its Quality is 80 and it never alters.

 * */

using System.Collections.Generic;
using System.Text;

namespace ConsoleApp
{
	public partial class GildedRose
	{
	    private const int MaxQuality = 50;
	    private const int MinQuality = 0;

	    private const string Brie = "Aged Brie";
	    private const string ConcertTicket = "Backstage passes to a TAFKAL80ETC concert";
	    private const string Sulfuras = "Sulfuras, Hand of Ragnaros";

	    public void UpdateQuality()
		{
		    foreach (var item in _innventory)
		    {
		        if (item.Name != Brie && item.Name != ConcertTicket)
		        {
		            if (item.Quality > MinQuality)
		            {
		                if (item.Name != Sulfuras)
		                {
		                    item.Quality--;
		                }
		            }
		        }
		        else
		        {
		            if (item.Quality < MaxQuality)
		            {
		                item.Quality++;

		                if (item.Name == ConcertTicket)
		                {
		                    if (item.SellIn < 11)
		                    {
		                        if (item.Quality < MaxQuality)
		                        {
		                            item.Quality++;
		                        }
		                    }

		                    if (item.SellIn < 6)
		                    {
		                        if (item.Quality < MaxQuality)
		                        {
		                            item.Quality++;
		                        }
		                    }
		                }
		            }
		        }

		        if (item.Name != Sulfuras)
		        {
		            item.SellIn--;
		        }

		        if (item.SellIn < 0)
		        {
		            if (item.Name != Brie)
		            {
		                if (item.Name != ConcertTicket)
		                {
		                    if (item.Quality > MinQuality)
		                    {
		                        if (item.Name != Sulfuras)
		                        {
		                            item.Quality--;
		                        }
		                    }
		                }
		                else
		                {
		                    item.Quality = MinQuality;
		                }
		            }
		            else
		            {
		                if (item.Quality < MaxQuality)
		                {
		                    item.Quality++;
		                }
		            }
		        }
		    }
		}

	    private void UpdateGeneralItem(Item item)
	    {
	        
	    }

	    public IList<Item> GetInventory()
	    {
	        return _innventory;
	    }

	    public void DumpDebugInfo(StringBuilder log)
		{
			foreach(var item in _innventory)
			{
				log.AppendFormat("[{1}, {2}] {0} should sell in {2} and has quality {1}.", item.Name, item.Quality, item.SellIn);
				log.AppendLine();
			}
		}
	}
}
