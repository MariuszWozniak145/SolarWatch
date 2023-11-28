using Microsoft.EntityFrameworkCore;
using SolarWatchMVC.Databases.Contexts;
using SolarWatchMVC.Repositories;
using SolarWatchMVC.Repositories.Interfaces;
using SolarWatchMVC.Services.LocationDataProvider;

namespace SolarWatchMVC.Configurations;

public static class ServiceCollectionExtension
{
    public static void AddDbContexts(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<SolarWatchContext>(options =>
            options.UseSqlServer(configuration["SolarWatchDbKey"]));
    }

    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<ICityRepository, CityRepository>();
        services.AddScoped<ISunriseSunsetTimesRepository, SunriseSunsetTimesRepository>();
    }

    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<ILocationDataProvider, OpenWeatherMapApi>();
    }
}