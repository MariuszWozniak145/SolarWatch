using Microsoft.EntityFrameworkCore;
using SolarWatchMVC.Databases.Contexts;

namespace SolarWatchMVC.Configurations;

public static class ServiceCollectionExtension
{
    public static void AddDbContexts(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<SolarWatchContext>(options =>
            options.UseSqlServer(configuration["SolarWatchDbKey"]));
    }
}