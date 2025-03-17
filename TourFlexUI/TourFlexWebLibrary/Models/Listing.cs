namespace TourFlexWebLibrary.Models;

public class Listing
{
    public int Id { get; set; }
    public string Title { get; set; }
    public decimal Price { get; set; }
    public int Discount { get; set; } // Percentage
    public string Status { get; set; } // "Active", "Inactive"
}