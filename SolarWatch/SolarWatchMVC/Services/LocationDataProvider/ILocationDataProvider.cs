namespace SolarWatchMVC.Services.LocationDataProvider;

public interface ILocationDataProvider
{
    Task<string> GetLocationAsync(string city, string? state, string? country);
}
