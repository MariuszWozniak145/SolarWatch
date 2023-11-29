using SolarWatchMVC.Data;

namespace SolarWatchMVC.Repositories.Interfaces;

public interface ISunriseSunsetTimesRepository : IBaseRepository<SunriseSunsetTimes>
{
    public Task<SunriseSunsetTimes?> GetByDetailsAsync(Guid cityId, string date);
}
