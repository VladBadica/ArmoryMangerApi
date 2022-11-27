using ArmoryManagerApi.DataTransferObjects;
using ArmoryManagerApi.Interfaces;
using ArmoryManagerApi.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ArmoryManagerApi.Data.Repositories;

public class BulletPurchaseRepository :IBulletPurchaseRepository
{
    private readonly ArmoryManagerContext _context;
    private readonly IMapper _mapper;

    public BulletPurchaseRepository(ArmoryManagerContext context, IMapper mapper)
	{
        _context = context;
        _mapper = mapper;
    }

    public void AddBulletPurchase(BulletPurchase newBulletPurchase)
    {
        _context.BulletPurchases.Add(newBulletPurchase);
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
        return await _context.BulletPurchases.Include(b => b.BulletTemplate).ToListAsync();
    }

    public async Task<BulletPurchase> GetBulletPurchaseAsync(long id)
    {
        var bulletPurchase = await _context.BulletPurchases
            .Include(b => b.BulletTemplateId)
            .Where(b => b.Id == id)
            .FirstAsync();

        if (bulletPurchase == null)
        {
            throw new Exception("Bullet puchase id not found");
        }

        return bulletPurchase;
    }   
}
