namespace SolarWatchMVC.Services.SunriseSunsetDataProvider;

public interface ISunriseSunsetDataProvider
{
    Task<string> GetSunriseSunsetAsync(double lat, double lon, string date);
}
