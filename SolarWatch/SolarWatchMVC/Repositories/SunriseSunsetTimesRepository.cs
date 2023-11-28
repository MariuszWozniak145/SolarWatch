using SolarWatchMVC.Databases.Contexts;
using SolarWatchMVC.Models;
using SolarWatchMVC.Repositories.Interfaces;

namespace SolarWatchMVC.Repositories;

public class SunriseSunsetTimesRepository : BaseRepository<SunriseSunsetTimes>, ISunriseSunsetTimesRepository
{
    public SunriseSunsetTimesRepository(SolarWatchContext dbContext) : base(dbContext) { }
}