using ArmoryManagerApi.Interfaces;
using ArmoryManagerApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ArmoryManagerApi.Data.Repositories;

public class CasingPurchaseRepository :ICasingPurchaseRepository
{
    private readonly ArmoryManagerContext _context;

    public CasingPurchaseRepository(ArmoryManagerContext context)
	{
        _context = context;
    }

    public void AddCasingPurchase(CasingPurchase newCasingPurchase)
    {
        _context.CasingPurchases.Add(newCasingPurchase);
    }

    public async Task ConsumeCasings(long id, long count)
    {
        var casingPurchase = await _context.CasingPurchases.FindAsync(id); 
        
        if (casingPurchase == null)
        {
            throw new Exception("Casing puchase id not found");
        }

        if (casingPurchase.Remaining < count)
        {
            throw new Exception("not enough casings remaining");
        }

        casingPurchase.Remaining -= count;
    }

    public async Task DeleteCasingPurchaseAsync(long id)
    {
        var casingPurchase = await _context.CasingPurchases.FindAsync(id);

        if(casingPurchase == null)
        {
            throw new Exception("Casing puchase id not found");
        }

        _context.CasingPurchases.Remove(casingPurchase);
    }
    public async Task<IEnumerable<CasingPurchase>> GetAllCasingPurchasesAsync()
    {
        return await _context.CasingPurchases.ToListAsync();
    }

    public async Task<CasingPurchase> GetCasingPurchaseAsync(long id)
    {
        var casingPurchase = await _context.CasingPurchases.FindAsync(id);

        if (casingPurchase == null)
        {
            throw new Exception("Casing puchase id not found");
        }

        return casingPurchase;
    }   
}
