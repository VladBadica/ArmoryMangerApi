using ArmoryManagerApi.Models;

namespace ArmoryManagerApi.Interfaces;

public interface IPrimerRepository
{
    void AddPrimer(Primer newPrimer);

    Task DeletePrimerAsync(long id);

    Task<IEnumerable<Primer>> GetAllPrimersAsync(); 
    
    Task<Primer> GetPrimerAsync(long id);

    Task ConsumePrimers(long id, long count);
}
