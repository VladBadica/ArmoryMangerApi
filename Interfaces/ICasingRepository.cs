using ArmoryManagerApi.Models;

namespace ArmoryManagerApi.Interfaces;

public interface ICasingRepository
{
    void AddCasing(Casing newCasing);

    Task DeleteCasingAsync(long id);

    Task<IEnumerable<Casing>> GetAllCasingsAsync(); 
    
    Task<Casing> GetCasingAsync(long id);

    Task ConsumeCasings(long id, long count);
}
