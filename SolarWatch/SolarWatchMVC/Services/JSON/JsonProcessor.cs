using SolarWatchMVC.Data;
using System.Text.Json;
using System.Text;

namespace SolarWatchMVC.Services.JSON;

public class JsonProcessor : IJsonProcessor
{
    public async Task<City> GetLocationAsync(string data)
    {
        using MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(data));
        using JsonDocument json = await JsonDocument.ParseAsync(stream);
        var name = json.RootElement[0].GetProperty("name").GetString();
        var state = json.RootElement[0].TryGetProperty("state", out var stateElement) ? stateElement.GetString() : null;
        var country = json.RootElement[0].TryGetProperty("country", out var countryElement) ? countryElement.GetString() : null;
        var lat = json.RootElement[0].GetProperty("lat").GetDouble();
        var lon = json.RootElement[0].GetProperty("lon").GetDouble();
        return new(name, state, country, lat, lon);
    }

    public async Task<SunriseSunsetTimes> GetSunriseSunsetAsync(string data, string date, City city)
    {
        using MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(data));
        using JsonDocument json = await JsonDocument.ParseAsync(stream);
        var results = json.RootElement.GetProperty("results");
        var sunrise = results.GetProperty("sunrise").GetString();
        var sunset = results.GetProperty("sunset").GetString();
        return new(date, city.Id, sunrise, sunset);
    }
}