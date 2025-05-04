using MudBlazor;

namespace TourFlexWeb.Pages.MockUps;

public class Post
{
    public int Id { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string MediaUrl { get; set; } = string.Empty;
    public string MediaType { get; set; } = "text"; // "text", "image", or "video"
    public int Likes { get; set; }
    public int Comments { get; set; }
}

public class Badge
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public Color Color { get; set; } = Color.Default;
}

public class Flag
{
    public int Id { get; set; }
    public string Location { get; set; } = string.Empty;
    public Color Color { get; set; } = Color.Default;
}

public class TravelPlan
{
    public string Destination { get; set; } = string.Empty;
    public string Interest { get; set; } = string.Empty;
}
