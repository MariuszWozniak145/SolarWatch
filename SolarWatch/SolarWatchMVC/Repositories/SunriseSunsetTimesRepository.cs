using Microsoft.EntityFrameworkCore;
using SolarWatchMVC.Data;
using SolarWatchMVC.Databases.Contexts;
using SolarWatchMVC.Repositories.Interfaces;

namespace SolarWatchMVC.Repositories;

public class SunriseSunsetTimesRepository : BaseRepository<SunriseSunsetTimes>, ISunriseSunsetTimesRepository
{
    public SunriseSunsetTimesRepository(SolarWatchContext dbContext) : base(dbContext) { }

    public async Task<SunriseSunsetTimes?> GetByDetailsAsync(Guid cityId, string date)
    {
        return await _dbContext.SunriseSunsetTimes.FirstOrDefaultAsync(sunriseSunsetTimes => sunriseSunsetTimes.Date == date && sunriseSunsetTimes.CityId == cityId);
    }
}