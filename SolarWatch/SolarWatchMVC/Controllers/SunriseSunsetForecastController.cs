using Microsoft.AspNetCore.Mvc;
using SolarWatchMVC.Data;
using SolarWatchMVC.Models;
using SolarWatchMVC.Repositories.Interfaces;
using SolarWatchMVC.Services.JSON;
using SolarWatchMVC.Services.LocationDataProvider;

namespace SolarWatchMVC.Controllers;

public class SunriseSunsetForecastController : Controller
{
    private readonly ICityRepository _cityRepository;
    private readonly ILocationDataProvider _locationDataProvider;
    private readonly IJsonProcessor _jsonProcessor;

    public SunriseSunsetForecastController(ICityRepository cityRepository,
        ILocationDataProvider locationDataProvider,
        IJsonProcessor jsonProcessor)
    {
        _cityRepository = cityRepository;
        _locationDataProvider = locationDataProvider;
        _jsonProcessor = jsonProcessor;
    }

    public async Task<IActionResult> Index(DisplaySunriseSunsetForecastModel model)
    {
        ViewBag.Errors = TempData["Errors"] ?? Array.Empty<string>();
        var data = new SunriseSunsetForecastModel
        {
            GetSunriseSunsetForecastModel = new GetSunriseSunsetForecastModel() { date = DateOnly.FromDateTime(DateTime.Now) },
            DisplaySunriseSunsetForecastModel = model,
        };
        return View(data);
    }

    [HttpGet]
    public async Task<IActionResult> GetSunriseSunsetForecast(SunriseSunsetForecastModel model)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
            TempData["Errors"] = errors;
            return RedirectToAction(nameof(Index), new DisplaySunriseSunsetForecastModel());
        }

        var cityName = model.GetSunriseSunsetForecastModel.cityName;
        var state = model.GetSunriseSunsetForecastModel.state;
        var country = model.GetSunriseSunsetForecastModel.country;
        var date = model.GetSunriseSunsetForecastModel.date.ToString("yyyy-MM-dd");

        var city = await GetCity(cityName, state, country);
        if (city == null)
        {
            TempData["Errors"] = new List<string> { "There is no city with the given data" };
            return RedirectToAction(nameof(Index), new DisplaySunriseSunsetForecastModel());
        }

        return RedirectToAction(nameof(Index), new DisplaySunriseSunsetForecastModel()
        {
            cityName = city?.Name,
            state = city?.State,
            country = city?.Country,
            date = date,
        });
    }

    private async Task<City?> GetCity(string cityName, string? state, string? country)
    {
        var city = await _cityRepository.GetByDetailsAsync(cityName, state, country);
        if (city == null)
        {
            var cityData = await _locationDataProvider.GetLocationAsync(cityName, state, country);
            if (cityData == "[]") return null;
            city = await _jsonProcessor.GetLocationAsync(cityData);
            await _cityRepository.AddAsync(city);
        }
        return city;
    }
}
