using ArmoryManagerApi.Interfaces;
using ArmoryManagerApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ArmoryManagerApi.Data.Repositories;

public class PowderPurchaseRepository : IPowderPurchaseRepository
{
    private readonly ArmoryManagerContext _context;

    public PowderPurchaseRepository(ArmoryManagerContext context)
	{
        _context = context;
    }

    public void AddPowderPurchase(PowderPurchase newPowderPurchase)
    {
        _context.PowderPurchases.Add(newPowderPurchase);
    }

    public async Task DeletePowderPurchaseAsync(long id)
    {
        var powderPurchase = await _context.PowderPurchases.FindAsync(id);

        if(powderPurchase == null)
        {
            throw new Exception("Powder puchase id not found");
        }

        _context.PowderPurchases.Remove(powderPurchase);
    }
    public async Task<IEnumerable<PowderPurchase>> GetAllPowderPurchasesAsync()
    {
        return await _context.PowderPurchases.Include(b => b.PowderTemplate).ToListAsync();
    }

    public async Task<PowderPurchase> GetPowderPurchaseAsync(long id)
    {
        var powderPurchase = await _context.PowderPurchases
            .Include(b => b.PowderTemplate)
            .Where(b => b.Id == id)
            .FirstAsync();

        if (powderPurchase == null)
        {
            throw new Exception("Powder puchase id not found");
        }

        return powderPurchase;
    }   
}
