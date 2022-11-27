using ArmoryManagerApi.Models;

namespace ArmoryManagerApi.Interfaces;

public interface IBulletPurchaseRepository
{
    void AddBulletPurchase(BulletPurchase newBulletPurchase);

    Task DeleteBulletPurchaseAsync(long id);

    Task<IEnumerable<BulletPurchase>> GetAllBulletPurchasesAsync(); 
    
    Task<BulletPurchase> GetBulletPurchaseAsync(long id);
}
