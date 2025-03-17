using Microsoft.AspNetCore.Mvc;
using TourFlexSystemApi.Models;

namespace TourFlexSystemApi.Controllers;

[ApiController]
[Route("api/listings")]
public class ListingsController : ControllerBase
{
    [HttpGet("featured")]
    public IActionResult GetFeatured()
    {
        var featured = new List<Listing>
            {
                new() { Id = 1, Title = "Winter Kayak Tour", Price = 60m, Discount = 20, Status = "Active" },
                new() { Id = 2, Title = "M?ori Culture Walk", Price = 45m, Discount = 25, Status = "Active" }
            };
        return Ok(featured);
    }

    [HttpGet("{id}")]
    public IActionResult GetListing(int id)
    {
        var listings = new List<Listing>
            {
                new() { Id = 1, Title = "Winter Kayak Tour", Price = 60m, Discount = 20, Status = "Active" },
                new() { Id = 2, Title = "M?ori Culture Walk", Price = 45m, Discount = 25, Status = "Active" }
            };
        var listing = listings.FirstOrDefault(l => l.Id == id);
        return listing != null ? Ok(listing) : NotFound();
    }

    [HttpPost("book")]
    public IActionResult Book([FromBody] Booking booking)
    {
        // Mock response—later, save to DB
        return Ok(new { Message = "Booking successful", BookingId = 123 });
    }
}