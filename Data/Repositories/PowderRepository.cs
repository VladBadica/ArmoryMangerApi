using ArmoryManagerApi.Interfaces;
using ArmoryManagerApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ArmoryManagerApi.Data.Repositories;

public class PowderRepository : IPowderRepository
{
    private readonly ArmoryManagerContext _context;

    public PowderRepository(ArmoryManagerContext context)
	{
        _context = context;
    }

    public void AddPowder(Powder newPowder)
    {
        _context.Powders.Add(newPowder);
    }

    public async Task ConsumePowders(long id, long count)
    {
        var powder = await _context.Powders.FindAsync(id);

        if (powder == null)
        {
            throw new Exception("Casing puchase id not found");
        }

        if (powder.Remaining < count)
        {
            throw new Exception("not enough casings remaining");
        }

        powder.Remaining -= count;
    }

    public async Task DeletePowderAsync(long id)
    {
        var powder = await _context.Powders.FindAsync(id);

        if(powder == null)
        {
            throw new Exception("Powder puchase id not found");
        }

        _context.Powders.Remove(powder);
    }
    public async Task<IEnumerable<Powder>> GetAllPowdersAsync()
    {
        return await _context.Powders.ToListAsync();
    }

    public async Task<Powder> GetPowderAsync(long id)
    {
        var powder = await _context.Powders.FindAsync(id);

        if (powder == null)
        {
            throw new Exception("Powder puchase id not found");
        }

        return powder;
    }   
}
