namespace SolarWatchMVC.Services.SunriseSunsetDataProvider;

public class SunriseSunsetApi : ISunriseSunsetDataProvider
{
    public async Task<string> GetSunriseSunsetAsync(double lat, double lon, string date)
    {
        var url = $"https://api.sunrise-sunset.org/json?lat={lat}&lng={lon}&date={date}";
        using var client = new HttpClient();
        var response = await client.GetAsync(url);
        return await response.Content.ReadAsStringAsync();
    }
}
