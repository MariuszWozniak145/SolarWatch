namespace SolarWatchMVC.Services.LocationDataProvider;

public class OpenWeatherMapApi : ILocationDataProvider
{
    private readonly IConfiguration _config;

    public OpenWeatherMapApi(IConfiguration config)
    {
        _config = config;
    }

    public async Task<string> GetLocationAsync(string city, string? state, string? country)
    {
        var apiKey = _config["OpenWeatherApiKey"];
        var responseLimit = 1;
        var url = $"http://api.openweathermap.org/geo/1.0/direct?q={city},{state},{country}&limit={responseLimit}&appid={apiKey}";

        using var client = new HttpClient();

        var response = await client.GetAsync(url);
        return await response.Content.ReadAsStringAsync();
    }
}
