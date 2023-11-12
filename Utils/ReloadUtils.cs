using ArmoryManagerApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ArmoryManagerApi.Utils;

public class ReloadUtils
{
    private readonly ArmoryManagerContext _context;

    public ReloadUtils(ArmoryManagerContext context)
    {
        _context = context;
    }

    public IEnumerable<Reload> GetAllReloads()
    {
        return _context.Reloads
            .Include(r => r.Casing)
            .Include(r => r.Powder)
            .Include(r => r.Primer)
            .ToList();
    }

    public Reload GetReload(long id)
    {
        return _context.Reloads
            .Include(r => r.Casing)
            .Include(r => r.Powder)
            .Include(r => r.Primer)
            .Where(r => r.Id == id)
            .First();
    }
}
