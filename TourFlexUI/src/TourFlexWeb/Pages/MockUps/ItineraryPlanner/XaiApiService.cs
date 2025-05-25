namespace TourFlexWeb.Pages.MockUps.ItineraryPlanner;

public class XaiApiService
{ // Mocked for MVP; replace with actual xAI API call
    public async Task<IList<ItineraryItem>> GenerateItineraryAsync(string destination, int budget, string interests)
    {
        // Simulate API delay
        await Task.Delay(1000);

        // Mocked response
        return new List<ItineraryItem>
        {
            new () { Title = $"Flight to {destination}", Description = "Economy class, round trip", Category = "Flights" },
            new () { Title = "City Hotel", Description = "3-star hotel in downtown", Category = "Hotels" },
            new() { Title = "Cultural Tour", Description = $"Guided tour based on {interests}", Category = "Activities" }
        };
    }

}