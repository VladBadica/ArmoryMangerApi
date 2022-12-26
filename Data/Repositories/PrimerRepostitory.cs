using ArmoryManagerApi.Interfaces;
using ArmoryManagerApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ArmoryManagerApi.Data.Repositories;

public class PrimerRepository : IPrimerRepository
{
    private readonly ArmoryManagerContext _context;

    public PrimerRepository(ArmoryManagerContext context)
	{
        _context = context;
    }

    public void AddPrimer(Primer newPrimer)
    {
        _context.Primers.Add(newPrimer);
    }

    public async Task ConsumePrimers(long id, long count)
    {
        var primer = await _context.Primers.FindAsync(id);

        if (primer == null)
        {
            throw new Exception("Casing puchase id not found");
        }

        if (primer.Remaining < count)
        {
            throw new Exception("not enough casings remaining");
        }

        primer.Remaining -= count;
    }

    public async Task DeletePrimerAsync(long id)
    {
        var primer = await _context.Primers.FindAsync(id);

        if(primer == null)
        {
            throw new Exception("Primer puchase id not found");
        }

        _context.Primers.Remove(primer);
    }
    public async Task<IEnumerable<Primer>> GetAllPrimersAsync()
    {
        return await _context.Primers.ToListAsync();
    }

    public async Task<Primer> GetPrimerAsync(long id)
    {
        var primer = await _context.Primers.FindAsync(id);

        if (primer == null)
        {
            throw new Exception("Primer puchase id not found");
        }

        return primer;
    }   
}
