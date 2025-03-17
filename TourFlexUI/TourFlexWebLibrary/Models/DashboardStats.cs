namespace TourFlex.Shared.Models;

public class DashboardStats
{
    public int Bookings { get; set; }
    public int Listings { get; set; }
    public required string SubscriptionTier { get; set; }
}