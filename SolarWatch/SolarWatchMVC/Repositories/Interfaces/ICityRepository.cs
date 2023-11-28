using SolarWatchMVC.Data;

namespace SolarWatchMVC.Repositories.Interfaces;

public interface ICityRepository
{
    Task<City?> GetByDetailsAsync(string name, string? state, string? country);
}
