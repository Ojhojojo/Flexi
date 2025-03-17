namespace TourFlexWebLibrary.Models;

public class Booking
{
    public int Id { get; set; }
    public int ListingId { get; set; }
    public DateTime Date { get; set; }
    public int Guests { get; set; }
    public decimal TotalPrice { get; set; }
}