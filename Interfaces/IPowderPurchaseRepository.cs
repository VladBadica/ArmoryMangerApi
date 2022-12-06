using ArmoryManagerApi.Models;

namespace ArmoryManagerApi.Interfaces;

public interface IPowderPurchaseRepository
{
    void AddPowderPurchase(PowderPurchase newPowderPurchase);

    Task DeletePowderPurchaseAsync(long id);

    Task<IEnumerable<PowderPurchase>> GetAllPowderPurchasesAsync(); 
    
    Task<PowderPurchase> GetPowderPurchaseAsync(long id);

    Task ConsumePowders(long id, long count);
}
