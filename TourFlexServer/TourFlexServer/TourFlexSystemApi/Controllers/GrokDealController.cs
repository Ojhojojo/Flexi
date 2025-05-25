using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Text.RegularExpressions;

namespace TourFlexSystemApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GrokDealController : ControllerBase
{
    private readonly HttpClient _httpClient;
    private readonly IMemoryCache _cache;
    private readonly ILogger<GrokController> _logger;
    private static readonly Regex InvalidChars = new Regex("[<>{}]", RegexOptions.Compiled);
    private static readonly Regex TravelKeywords = new Regex(@"\b(travel|itinerary|destination|trip|vacation|tour|flight|hotel|activity|plan|visit|explore|journey|adventure)\b", RegexOptions.Compiled | RegexOptions.IgnoreCase);
    private static readonly Regex HotelPattern = new Regex(@"(stay at|book|hotel|accommodation)\s+([A-Za-z\s]+)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
    private static readonly Regex ActivityPattern = new Regex(@"(do|visit|experience|tour)\s+([A-Za-z\s]+)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
    private static readonly Regex FlightPattern = new Regex(@"(flight to|fly to|travel to)\s+([A-Za-z\s]+)", RegexOptions.Compiled | RegexOptions.IgnoreCase);

    public GrokDealController(IHttpClientFactory httpClientFactory, IMemoryCache cache, ILogger<GrokController> logger)
    {
        _httpClient = httpClientFactory.CreateClient("XaiApi");
        _cache = cache;
        _logger = logger;
    }

    [HttpPost("deals")]
    public async Task<IActionResult> GetCheapestDeals([FromBody] DealRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Query))
        {
            _logger.LogWarning("Deal query is empty or null.");
            return BadRequest("Query is required.");
        }

        if (request.Query.Length > 500)
        {
            _logger.LogWarning("Query exceeds maximum length: {Length}", request.Query.Length);
            return BadRequest("Query cannot exceed 500 characters.");
        }

        if (InvalidChars.IsMatch(request.Query))
        {
            _logger.LogWarning("Query contains invalid characters: {Query}", request.Query);
            return BadRequest("Query contains invalid characters (<, >, {, }).");
        }

        var cacheKey = $"DealResponse_{request.Query.GetHashCode()}";
        if (_cache.TryGetValue(cacheKey, out string cachedResponse))
        {
            _logger.LogInformation("Returning cached deal response for query: {Query}", request.Query);
            return Ok(new { Response = cachedResponse, Links = ExtractLinks(cachedResponse) });
        }

        try
        {
            _logger.LogInformation("Sending deal query to xAI API: {Query}", request.Query);

            var chatRequest = new
            {
                model = "grok-beta",
                messages = new[]
                {
                        new { role = "system", content = "You are a travel deal expert. Search the web for the cheapest travel deals (flights, hotels, activities) matching the user's query. Provide a concise response with specific prices and providers if available, using clear formatting (e.g., bullet points, headings). If no deals are found, suggest alternative strategies to find cheap travel options." },
                        new { role = "user", content = request.Query }
                    },
                max_tokens = 500,
                temperature = 0.7
            };

            var response = await _httpClient.PostAsJsonAsync("chat/completions", chatRequest);
            _logger.LogInformation("Received response from xAI API: Status {StatusCode}", response.StatusCode);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            _logger.LogInformation("Raw xAI API response: {ResponseContent}", responseContent);

            var result = await response.Content.ReadFromJsonAsync<ChatCompletionResponse>();
            if (result == null || result.Choices == null || !result.Choices.Any())
            {
                _logger.LogWarning("xAI API returned empty or invalid response: {ResponseContent}", responseContent);
                return Ok(new { Response = "Sorry, I couldn't find any deals. Please try a different query or check providers like Trip.com or Skyscanner directly.", Links = new List<Link>() });
            }

            var content = result.Choices.FirstOrDefault()?.Message?.Content ?? "Sorry, I couldn't find any deals. Please try a different query or check providers like Trip.com or Skyscanner directly.";
            _logger.LogInformation("Deal response content: {Content}", content);

            _cache.Set(cacheKey, content, TimeSpan.FromMinutes(10));
            return Ok(new { Response = content, Links = ExtractLinks(content) });
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "HTTP error occurred while calling xAI API. Status: {StatusCode}", ex.StatusCode);
            if (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return NotFound("xAI API endpoint not found.");
            }
            if (ex.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
            {
                return StatusCode(429, "Rate limit exceeded. Try again later.");
            }
            if (ex.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                return Unauthorized("Invalid or expired API key.");
            }
            return StatusCode(500, $"HTTP error: {ex.Message}");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error occurred while processing xAI API response.");
            return StatusCode(500, $"Error: {ex.Message}");
        }
    }

    private List<Link> ExtractLinks(string content)
    {
        var links = new List<Link>();

        var hotelMatches = HotelPattern.Matches(content);
        foreach (Match match in hotelMatches)
        {
            if (match.Groups.Count > 2)
            {
                var hotelName = match.Groups[2].Value.Trim();
                var bookingLink = $"https://www.booking.com/search.html?ss={Uri.EscapeDataString(hotelName)}&affiliate_id=YOUR_BOOKING_AFFILIATE_ID";
                links.Add(new Link { Type = "Hotel", Name = hotelName, Url = bookingLink, Provider = "Booking.com" });
                var tripLink = $"https://www.trip.com/hotels/?search={Uri.EscapeDataString(hotelName)}&affid=YOUR_TRIP_AFFILIATE_ID";
                links.Add(new Link { Type = "Hotel", Name = hotelName, Url = tripLink, Provider = "Trip.com" });
            }
        }

        var activityMatches = ActivityPattern.Matches(content);
        foreach (Match match in activityMatches)
        {
            if (match.Groups.Count > 2)
            {
                var activityName = match.Groups[2].Value.Trim();
                var viatorLink = $"https://www.viator.com/searchResults/all?text={Uri.EscapeDataString(activityName)}&affiliate_id=YOUR_VIATOR_AFFILIATE_ID";
                links.Add(new Link { Type = "Activity", Name = activityName, Url = viatorLink, Provider = "Viator" });
                var tripLink = $"https://www.trip.com/things-to-do/search?keywords={Uri.EscapeDataString(activityName)}&affid=YOUR_TRIP_AFFILIATE_ID";
                links.Add(new Link { Type = "Activity", Name = activityName, Url = tripLink, Provider = "Trip.com" });
            }
        }

        var flightMatches = FlightPattern.Matches(content);
        foreach (Match match in flightMatches)
        {
            if (match.Groups.Count > 2)
            {
                var destination = match.Groups[2].Value.Trim();
                var tripLink = $"https://www.trip.com/flights/?search={Uri.EscapeDataString(destination)}&affid=YOUR_TRIP_AFFILIATE_ID";
                links.Add(new Link { Type = "Flight", Name = destination, Url = tripLink, Provider = "Trip.com" });
            }
        }

        return links;
    }
}

public class DealRequest
{
    public string Query { get; set; }
}

public class Link
{
    public string Type { get; set; }
    public string Name { get; set; }
    public string Url { get; set; }
    public string Provider { get; set; }
}

