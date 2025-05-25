namespace TourFlexWeb.Pages.MockUps.ItineraryPlanner;

public class ItineraryService
{
    private readonly List<ItineraryItem> _items = new();

    public List<ItineraryItem> GetItineraryItems() => _items;

    public void AddItems(IEnumerable<ItineraryItem> items)
    {
        _items.AddRange(items);
    }

    public void UpdateItemCategory(ItineraryItem item, string newCategory)
    {
        var existingItem = _items.FirstOrDefault(i => i.Id == item.Id);
        if (existingItem != null)
        {
            existingItem.Category = newCategory;
        }
    }

}