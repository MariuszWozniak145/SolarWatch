using System.ComponentModel.DataAnnotations;

namespace SolarWatchMVC.Models;

public class DisplaySunriseSunsetForecastModel
{
    [Required]
    public string CityName { get; init; }
    public string? State { get; init; }
    public string? Country { get; init; }
    [Required]
    public double Latitude { get; init; }
    [Required]
    public double Longitude { get; init; }
    [Required]
    public string Sunrise { get; init; }
    [Required]
    public string Sunset { get; init; }
    [Required]
    public string Date { get; init; }
}
