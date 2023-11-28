using System.ComponentModel.DataAnnotations;

namespace SolarWatchMVC.Models;

public class GetSunriseSunsetForecastModel
{
    [Required(ErrorMessage = "Please enter the city name")]
    public string cityName { get; set; }
    public string? state { get; set; }
    public string? country { get; set; }
    [Required(ErrorMessage = "Please enter valid date")]
    public DateOnly date { get; set; }
}
