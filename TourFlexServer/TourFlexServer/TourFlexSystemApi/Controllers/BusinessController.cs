using Microsoft.AspNetCore.Mvc;
using TourFlexSystemApi.Models;

namespace TourFlexSystemApi.Controllers;

[ApiController]
[Route("api/business")]
public class BusinessController : ControllerBase
{
    [HttpGet("stats")]
    public IActionResult GetStats()
    {
        var stats = new DashboardStats
        {
            Bookings = 5,
            Listings = 3,
            SubscriptionTier = "Basic"
        };
        return Ok(stats);
    }

    [HttpGet("listings")]
    public IActionResult GetListings()
    {
        var listings = new List<Listing>
            {
                new() { Id = 1, Title = "Winter Kayak Tour", Price = 60m, Discount = 20, Status = "Active" },
                new() { Id = 2, Title = "Māori Culture Walk", Price = 45m, Discount = 25, Status = "Active" },
                new() { Id = 3, Title = "Queenstown Ski Lesson", Price = 80m, Discount = 30, Status = "Inactive" }
            };
        return Ok(listings);
    }
}