using ArmoryManagerApi.Models;

namespace ArmoryManagerApi.Interfaces;

public interface IPowderRepository
{
    void AddPowder(Powder newPowder);

    Task DeletePowderAsync(long id);

    Task<IEnumerable<Powder>> GetAllPowdersAsync(); 
    
    Task<Powder> GetPowderAsync(long id);

    Task ConsumePowders(long id, long count);
}
