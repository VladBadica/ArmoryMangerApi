using ArmoryManagerApi.Models;

namespace ArmoryManagerApi.Interfaces;

public interface ICasingPurchaseRepository
{
    void AddCasingPurchase(CasingPurchase newCasingPurchase);

    Task DeleteCasingPurchaseAsync(long id);

    Task<IEnumerable<CasingPurchase>> GetAllCasingPurchasesAsync(); 
    
    Task<CasingPurchase> GetCasingPurchaseAsync(long id);

    Task ConsumeCasings(long id, long count);
}
