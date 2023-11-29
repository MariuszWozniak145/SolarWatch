using System.ComponentModel.DataAnnotations;

namespace SolarWatchMVC.Models;

public class DisplaySunriseSunsetForecastModel
{
    [Required]
    public string cityName { get; init; }
    public string? state { get; init; }
    public string? country { get; init; }
    [Required]
    public string date { get; init; }
}
