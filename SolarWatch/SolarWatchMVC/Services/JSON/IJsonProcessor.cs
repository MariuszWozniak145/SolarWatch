using SolarWatchMVC.Data;

namespace SolarWatchMVC.Services.JSON;

public interface IJsonProcessor
{
    Task<City> GetLocationAsync(string data);
    Task<SunriseSunsetTimes> GetSunriseSunsetAsync(string data, string date, City city);
}
