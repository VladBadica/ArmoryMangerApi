using ArmoryManagerApi.Interfaces;
using ArmoryManagerApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ArmoryManagerApi.Data.Repositories;

public class CasingRepository :ICasingRepository
{
    private readonly ArmoryManagerContext _context;

    public CasingRepository(ArmoryManagerContext context)
	{
        _context = context;
    }

    public void AddCasing(Casing newCasing)
    {
        _context.Casings.Add(newCasing);
    }

    public async Task ConsumeCasings(long id, long count)
    {
        var casing = await _context.Casings.FindAsync(id); 
        
        if (casing == null)
        {
            throw new Exception("Casing puchase id not found");
        }

        if (casing.Remaining < count)
        {
            throw new Exception("not enough casings remaining");
        }

        casing.Remaining -= count;
    }

    public async Task DeleteCasingAsync(long id)
    {
        var casing = await _context.Casings.FindAsync(id);

        if(casing == null)
        {
            throw new Exception("Casing puchase id not found");
        }

        _context.Casings.Remove(casing);
    }
    public async Task<IEnumerable<Casing>> GetAllCasingsAsync()
    {
        return await _context.Casings.ToListAsync();
    }

    public async Task<Casing> GetCasingAsync(long id)
    {
        var casing = await _context.Casings.FindAsync(id);

        if (casing == null)
        {
            throw new Exception("Casing puchase id not found");
        }

        return casing;
    }   
}
