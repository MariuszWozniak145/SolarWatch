using Microsoft.EntityFrameworkCore;
using SolarWatchMVC.Data;

namespace SolarWatchMVC.Databases.Contexts;

public class SolarWatchContext : DbContext
{
    public SolarWatchContext(DbContextOptions<SolarWatchContext> options) : base(options) { }

    public DbSet<City> Cities { get; set; }
    public DbSet<SunriseSunsetTimes> SunriseSunsetTimes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<City>(city =>
        {
            city.HasKey(city => city.Id);
            city.HasMany(city => city.SunriseSunsetTimes)
            .WithOne(sunriseSunsetTimes => sunriseSunsetTimes.City)
            .HasForeignKey(sunriseSunsetTimes => sunriseSunsetTimes.CityId)
            .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<SunriseSunsetTimes>(sunriseSunsetTimes =>
        {
            sunriseSunsetTimes.HasKey(sunriseSunsetTimes => sunriseSunsetTimes.Id);
        });
    }
}
