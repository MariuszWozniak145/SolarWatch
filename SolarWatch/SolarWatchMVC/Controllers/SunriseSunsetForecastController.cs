﻿using Microsoft.AspNetCore.Mvc;
using SolarWatchMVC.Models;
using SolarWatchMVC.Repositories.Interfaces;

namespace SolarWatchMVC.Controllers;

public class SunriseSunsetForecastController : Controller
{
    private readonly ICityRepository _cityRepository;

    public SunriseSunsetForecastController(ICityRepository cityRepository)
    {
        _cityRepository = cityRepository;
    }

    public async Task<IActionResult> Index(DisplaySunriseSunsetForecastModel model)
    {
        var data = new SunriseSunsetForecastModel
        {
            GetSunriseSunsetForecastModel = new GetSunriseSunsetForecastModel() { date = DateOnly.FromDateTime(DateTime.Now) },
            DisplaySunriseSunsetForecastModel = model
        };
        return View(data);
    }

    [HttpGet]
    public async Task<IActionResult> GetSunriseSunsetForecast(SunriseSunsetForecastModel model)
    {
        var cityName = model.GetSunriseSunsetForecastModel.cityName;
        var state = model.GetSunriseSunsetForecastModel.state;
        var country = model.GetSunriseSunsetForecastModel.country;
        var date = model.GetSunriseSunsetForecastModel.date;
        var city = await _cityRepository.GetByDetailsAsync(cityName, state, country);
        return RedirectToAction(nameof(Index), new DisplaySunriseSunsetForecastModel()
        {
            cityName = city?.Name,
            state = city?.State,
            country = city?.Country,
            date = model.GetSunriseSunsetForecastModel.date,
        });
    }
}