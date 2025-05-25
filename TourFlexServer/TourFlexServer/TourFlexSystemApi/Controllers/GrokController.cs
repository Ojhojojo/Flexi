using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.Caching.Memory;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace TourFlexSystemApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GrokController : ControllerBase
{
    private readonly HttpClient _httpClient;
    private readonly IMemoryCache _cache;
    private readonly ILogger<GrokController> _logger;
    private static readonly Regex InvalidChars = new Regex("[<>{}]", RegexOptions.Compiled);
    private static readonly Regex TravelKeywords = new Regex(@"\b(travel|itinerary|destination|trip|vacation|tour|flight|hotel|activity|plan|visit|explore|journey|adventure)\b", RegexOptions.Compiled | RegexOptions.IgnoreCase);

    public GrokController(IHttpClientFactory httpClientFactory, IMemoryCache cache, ILogger<GrokController> logger)
    {
        _httpClient = httpClientFactory.CreateClient("XaiApi");
        _cache = cache;
        _logger = logger;
    }

    /// <summary>
    /// GetGrokResponse for generic chat api response
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost("travel")]
    [EnableRateLimiting("GrokApi")]
    public async Task<IActionResult> GetGrokResponse([FromBody] GrokRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Prompt))
        {
            _logger.LogWarning("Prompt is empty or null.");
            return BadRequest("Prompt is required.");
        }

        if (request.Prompt.Length > 1000)
        {
            _logger.LogWarning("Prompt exceeds maximum length: {Length}", request.Prompt.Length);
            return BadRequest("Prompt cannot exceed 1000 characters.");
        }

        if (InvalidChars.IsMatch(request.Prompt))
        {
            _logger.LogWarning("Prompt contains invalid characters: {Prompt}", request.Prompt);
            return BadRequest("Prompt contains invalid characters (<, >, {, }).");
        }

        var cacheKey = $"GrokResponse_{request.Prompt.GetHashCode()}";
        if (_cache.TryGetValue(cacheKey, out string cachedResponse))
        {
            _logger.LogInformation("Returning cached response for prompt: {Prompt}", request.Prompt);
            return Ok(new { Response = cachedResponse });
        }

        try
        {
            _logger.LogInformation("Sending prompt to xAI API: {Prompt}", request.Prompt);

            var chatRequest = new
            {
                model = "grok-beta",
                messages = new[]
                {
                        new { role = "system", content = "You are Grok, a helpful AI assistant." },
                        new { role = "user", content = request.Prompt }
                    },
                max_tokens = 500,
                temperature = 0.7
            };

            var response = await _httpClient.PostAsJsonAsync("chat/completions", chatRequest);
            _logger.LogInformation("Received response from xAI API: Status {StatusCode}", response.StatusCode);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<ChatCompletionResponse>();
            var content = result?.Choices?.FirstOrDefault()?.Message?.Content ?? "No response received.";
            _logger.LogInformation("Response content: {Content}", content);

            _cache.Set(cacheKey, content, TimeSpan.FromMinutes(10));
            return Ok(new { Response = content });
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
            _logger.LogError(ex, "Unexpected error occurred.");
            return StatusCode(500, $"Error: {ex.Message}");
        }
    }

    /// <summary>
    /// GetGrokTravelResponse for travel related chat api response
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    [EnableRateLimiting("GrokApi")]
    public async Task<IActionResult> GetGrokTravelResponse([FromBody] GrokRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Prompt))
        {
            _logger.LogWarning("Prompt is empty or null.");
            return BadRequest("Prompt is required.");
        }

        if (request.Prompt.Length > 500)
        {
            _logger.LogWarning("Prompt exceeds maximum length: {Length}", request.Prompt.Length);
            return BadRequest("Prompt cannot exceed 500 characters.");
        }

        if (InvalidChars.IsMatch(request.Prompt))
        {
            _logger.LogWarning("Prompt contains invalid characters: {Prompt}", request.Prompt);
            return BadRequest("Prompt contains invalid characters (<, >, {, }).");
        }

        if (!TravelKeywords.IsMatch(request.Prompt))
        {
            _logger.LogWarning("Prompt is not travel-related: {Prompt}", request.Prompt);
            return BadRequest("Prompt must be related to travel or itinerary planning (e.g., destinations, activities, trips).");
        }

        var cacheKey = $"GrokResponse_{request.Prompt.GetHashCode()}";
        if (_cache.TryGetValue(cacheKey, out string cachedResponse))
        {
            _logger.LogInformation("Returning cached response for prompt: {Prompt}", request.Prompt);
            return Ok(new { Response = cachedResponse });
        }

        try
        {
            _logger.LogInformation("Sending prompt to xAI API: {Prompt}", request.Prompt);

            var chatRequest = new
            {
                model = "grok-beta",
                messages = new[]
                {
                        new { role = "system", content = "You are a travel assistant specializing in itinerary planning, destination recommendations, and travel activities. Provide concise, helpful answers with clear formatting (e.g., bullet points, headings). Focus on travel-related information." },
                        new { role = "user", content = request.Prompt }
                    },
                max_tokens = 500,
                temperature = 0.7
            };

            var response = await _httpClient.PostAsJsonAsync("chat/completions", chatRequest);
            _logger.LogInformation("Received response from xAI API: Status {StatusCode}", response.StatusCode);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<ChatCompletionResponse>();
            var content = result?.Choices?.FirstOrDefault()?.Message?.Content ?? "No response received.";
            _logger.LogInformation("Response content: {Content}", content);

            _cache.Set(cacheKey, content, TimeSpan.FromMinutes(10));
            return Ok(new { Response = content });
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
            _logger.LogError(ex, "Unexpected error occurred.");
            return StatusCode(500, $"Error: {ex.Message}");
        }
    }

}

public class GrokRequest
{
    public string Prompt { get; set; }
}

public class ChatCompletionResponse
{
    [JsonPropertyName("choices")]
    public List<Choice> Choices { get; set; }
}

public class Choice
{
    [JsonPropertyName("message")]
    public Message Message { get; set; }
}

public class Message
{
    [JsonPropertyName("content")]
    public string Content { get; set; }
}

