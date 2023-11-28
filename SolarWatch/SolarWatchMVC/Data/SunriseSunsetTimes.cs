namespace SolarWatchMVC.Data;

public class SunriseSunsetTimes
{
    public Guid Id { get; init; }
    public City City { get; init; }
    public Guid CityId { get; init; }
    public string Date { get; init; }
    public string Sunrise { get; init; }
    public string Sunset { get; init; }

    public SunriseSunsetTimes(string date, Guid cityId, string sunrise, string sunset)
    {
        Id = Guid.NewGuid();
        CityId = cityId;
        Date = date;
        Sunrise = sunrise;
        Sunset = sunset;
    }
}