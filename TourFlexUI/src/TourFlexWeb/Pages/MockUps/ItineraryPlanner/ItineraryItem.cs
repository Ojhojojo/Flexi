namespace TourFlexWeb.Pages.MockUps.ItineraryPlanner;

public class ItineraryItem
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
}