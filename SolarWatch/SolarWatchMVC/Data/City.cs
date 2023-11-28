namespace SolarWatchMVC.Data;

public class City
{
    public Guid Id { get; init; }
    public string Name { get; set; }
    public string? State { get; set; }
    public string? Country { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public List<SunriseSunsetTimes> SunriseSunsetTimes { get; init; } = new();

    public City(string name, string? state, string? country, double latitude, double longitude)
    {
        Id = Guid.NewGuid();
        Name = name;
        State = state;
        Country = country;
        Latitude = latitude;
        Longitude = longitude;
    }
}
