using ArmoryManagerApi.Interfaces;
using ArmoryManagerApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ArmoryManagerApi.Data.Repositories;

public class ReloadRepository : IReloadRepository
{
    private readonly ArmoryManagerContext _context;

    public ReloadRepository(ArmoryManagerContext context)
    {
        _context = context;
    }

    public void AddReload(Reload newReload)
    {
        _context.Reloads.Add(newReload);
    }

    public async Task<IEnumerable<Reload>> GetAllReloadsAsync()
    {
        return await _context.Reloads
            .Include(r => r.Casing)
            .Include(r => r.Powder)
            .Include(r => r.Primer)
            .ToListAsync();
    }

    public async Task<Reload> GetReloadAsync(long id)
    {
        var reload = await _context.Reloads
            .Include(r => r.Casing)
            .Include(r => r.Powder)
            .Include(r => r.Primer)
            .Where(r => r.Id == id)
            .FirstAsync();

        if (reload == null)
        {
            throw new Exception("id not found");
        }

        return reload;
    }
}
