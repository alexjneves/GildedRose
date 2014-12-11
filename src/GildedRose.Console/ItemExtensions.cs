namespace ConsoleApp
{
    public static class ItemExtensions
    {
        public static bool IsConjured(this GildedRose.Item item)
        {
            return item.Name.Contains("Conjured");
        }
    }
}