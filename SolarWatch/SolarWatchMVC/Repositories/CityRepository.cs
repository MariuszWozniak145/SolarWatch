using Microsoft.EntityFrameworkCore;
using SolarWatchMVC.Data;
using SolarWatchMVC.Databases.Contexts;
using SolarWatchMVC.Repositories.Interfaces;

namespace SolarWatchMVC.Repositories;

public class CityRepository : BaseRepository<City>, ICityRepository
{
    public CityRepository(SolarWatchContext dbContext) : base(dbContext) { }

    public async Task<City?> GetByDetailsAsync(string name, string? state, string? country)
    {
        if (name == null) return null;
        var query = await _dbContext.Cities.ToListAsync();

        query = query.Where(c => c.Name.ToLower() == name.ToLower()).ToList();

        if (!string.IsNullOrWhiteSpace(state)) query = query.Where(c => c.State.ToLower() == state.ToLower()).ToList();

        if (!string.IsNullOrWhiteSpace(country)) query = query.Where(c => c.Country.ToLower() == country.ToLower()).ToList();

        return query.FirstOrDefault();
    }
}
