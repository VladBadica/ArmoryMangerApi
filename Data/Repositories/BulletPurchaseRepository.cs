using ArmoryManagerApi.Interfaces;
using ArmoryManagerApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ArmoryManagerApi.Data.Repositories;

public class BulletPurchaseRepository :IBulletPurchaseRepository
{
    private readonly ArmoryManagerContext _context;

    public BulletPurchaseRepository(ArmoryManagerContext context)
	{
        _context = context;
    }

    public void AddBulletPurchase(BulletPurchase newBulletPurchase)
    {
        _context.BulletPurchases.Add(newBulletPurchase);
    }

    public async Task ConsumeBullets(long id, long count)
    {
        var bulletPurchase = await _context.BulletPurchases.FindAsync(id); 
        
        if (bulletPurchase == null)
        {
            throw new Exception("Bullet puchase id not found");
        }

        if (bulletPurchase.Remaining < count)
        {
            throw new Exception("not enough bullets remaining");
        }

        bulletPurchase.Remaining -= count;
    }

    public async Task DeleteBulletPurchaseAsync(long id)
    {
        var bulletPurchase = await _context.BulletPurchases.FindAsync(id);

        if(bulletPurchase == null)
        {
            throw new Exception("Bullet puchase id not found");
        }

        _context.BulletPurchases.Remove(bulletPurchase);
    }
    public async Task<IEnumerable<BulletPurchase>> GetAllBulletPurchasesAsync()
    {
        return await _context.BulletPurchases.ToListAsync();
    }

    public async Task<BulletPurchase> GetBulletPurchaseAsync(long id)
    {
        var bulletPurchase = await _context.BulletPurchases.FindAsync(id);

        if (bulletPurchase == null)
        {
            throw new Exception("Bullet puchase id not found");
        }

        return bulletPurchase;
    }   
}
