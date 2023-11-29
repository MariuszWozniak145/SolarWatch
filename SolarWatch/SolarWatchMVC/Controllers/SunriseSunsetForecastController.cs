using Microsoft.AspNetCore.Mvc;
using SolarWatchMVC.Data;
using SolarWatchMVC.Models;
using SolarWatchMVC.Repositories.Interfaces;
using SolarWatchMVC.Services.JSON;
using SolarWatchMVC.Services.LocationDataProvider;
using SolarWatchMVC.Services.SunriseSunsetDataProvider;

namespace SolarWatchMVC.Controllers;

public class SunriseSunsetForecastController : Controller
{
    private readonly ICityRepository _cityRepository;
    private readonly ISunriseSunsetTimesRepository _sunriseSunsetTimesRepository;
    private readonly ILocationDataProvider _locationDataProvider;
    private readonly ISunriseSunsetDataProvider _sunriseSunsetDataProvider;
    private readonly IJsonProcessor _jsonProcessor;

    public SunriseSunsetForecastController(ICityRepository cityRepository,
        ISunriseSunsetTimesRepository sunriseSunsetTimesRepository,
        ILocationDataProvider locationDataProvider,
        ISunriseSunsetDataProvider sunriseSunsetDataProvider,
        IJsonProcessor jsonProcessor)
    {
        _cityRepository = cityRepository;
        _sunriseSunsetTimesRepository = sunriseSunsetTimesRepository;
        _locationDataProvider = locationDataProvider;
        _sunriseSunsetDataProvider = sunriseSunsetDataProvider;
        _jsonProcessor = jsonProcessor;
    }

    public async Task<IActionResult> Index(DisplaySunriseSunsetForecastModel model)
    {
        ViewBag.Errors = TempData["Errors"] ?? Array.Empty<string>();
        var data = new SunriseSunsetForecastModel
        {
            GetSunriseSunsetForecastModel = new GetSunriseSunsetForecastModel() { Date = DateOnly.FromDateTime(DateTime.Now) },
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

        var cityName = model.GetSunriseSunsetForecastModel.CityName;
        var state = model.GetSunriseSunsetForecastModel.State;
        var country = model.GetSunriseSunsetForecastModel.Country;
        var date = model.GetSunriseSunsetForecastModel.Date.ToString("yyyy-MM-dd");

        var city = await GetCity(cityName, state, country);
        if (city == null)
        {
            TempData["Errors"] = new List<string> { "There is no city with the given data" };
            return RedirectToAction(nameof(Index), new DisplaySunriseSunsetForecastModel());
        }

        var forecast = await GetForecast(city, date);
        if (forecast == null)
        {
            TempData["Errors"] = new List<string> { "There is no iformation about sunrise and sunset for the given data" };
            return RedirectToAction(nameof(Index), new DisplaySunriseSunsetForecastModel());
        }

        return RedirectToAction(nameof(Index), new DisplaySunriseSunsetForecastModel()
        {
            CityName = city.Name,
            State = city.State,
            Country = city.Country,
            Latitude = city.Latitude,
            Longitude = city.Longitude,
            Sunrise = forecast.Sunrise,
            Sunset = forecast.Sunset,
            Date = forecast.Date,
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

    private async Task<SunriseSunsetTimes?> GetForecast(City city, string date)
    {
        var forecast = await _sunriseSunsetTimesRepository.GetByDetailsAsync(city.Id, date);
        if (forecast == null)
        {
            var sunriseSunsetData = await _sunriseSunsetDataProvider.GetSunriseSunsetAsync(city.Latitude, city.Longitude, date);
            if (sunriseSunsetData == "[]") return null;
            forecast = await _jsonProcessor.GetSunriseSunsetAsync(sunriseSunsetData, date, city);
            await _sunriseSunsetTimesRepository.AddAsync(forecast);
        }
        return forecast;
    }
}
