using ArmoryManagerApi.Interfaces;
using ArmoryManagerApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ArmoryManagerApi.Data.Repositories;

public class PrimerPurchaseRepository : IPrimerPurchaseRepository
{
    private readonly ArmoryManagerContext _context;

    public PrimerPurchaseRepository(ArmoryManagerContext context)
	{
        _context = context;
    }

    public void AddPrimerPurchase(PrimerPurchase newPrimerPurchase)
    {
        _context.PrimerPurchases.Add(newPrimerPurchase);
    }

    public async Task DeletePrimerPurchaseAsync(long id)
    {
        var primerPurchase = await _context.PrimerPurchases.FindAsync(id);

        if(primerPurchase == null)
        {
            throw new Exception("Primer puchase id not found");
        }

        _context.PrimerPurchases.Remove(primerPurchase);
    }
    public async Task<IEnumerable<PrimerPurchase>> GetAllPrimerPurchasesAsync()
    {
        return await _context.PrimerPurchases.Include(b => b.PrimerTemplate).ToListAsync();
    }

    public async Task<PrimerPurchase> GetPrimerPurchaseAsync(long id)
    {
        var primerPurchase = await _context.PrimerPurchases
            .Include(b => b.PrimerTemplate)
            .Where(b => b.Id == id)
            .FirstAsync();

        if (primerPurchase == null)
        {
            throw new Exception("Primer puchase id not found");
        }

        return primerPurchase;
    }   
}
