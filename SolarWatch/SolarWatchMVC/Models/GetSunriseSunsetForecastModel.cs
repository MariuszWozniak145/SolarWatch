using System.ComponentModel.DataAnnotations;

namespace SolarWatchMVC.Models;

public class GetSunriseSunsetForecastModel
{
    [Required(ErrorMessage = "Please enter the city name")]
    public string CityName { get; set; }
    public string? State { get; set; }
    public string? Country { get; set; }
    [Required(ErrorMessage = "Please enter valid date")]
    public DateOnly Date { get; set; }
}
