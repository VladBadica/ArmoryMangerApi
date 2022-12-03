using ArmoryManagerApi.Models;

namespace ArmoryManagerApi.Interfaces;

public interface IReloadRepository
{
    void AddReload(Reload newReload);

    Task<IEnumerable<Reload>> GetAllReloadsAsync();

    Task<Reload> GetReloadAsync(long id);

    Task DeleteReloadAsync(long id);
}
