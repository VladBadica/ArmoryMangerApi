using ArmoryManagerApi.Models;

namespace ArmoryManagerApi.Interfaces;

public interface IPrimerPurchaseRepository
{
    void AddPrimerPurchase(PrimerPurchase newPrimerPurchase);

    Task DeletePrimerPurchaseAsync(long id);

    Task<IEnumerable<PrimerPurchase>> GetAllPrimerPurchasesAsync(); 
    
    Task<PrimerPurchase> GetPrimerPurchaseAsync(long id);
}
