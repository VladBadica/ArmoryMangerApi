using ArmoryManagerApi.Models;

namespace ArmoryManagerApi.Interfaces;

public interface IPrimerTemplateRepository
{
    void AddPrimerTemplate(PrimerTemplate primerTemplate);

    Task DeletePrimerTemplateAsync(long id);

    Task<IEnumerable<PrimerTemplate>> GetAllPrimerTemplatesAsync();

    Task<PrimerTemplate> GetPrimerTemplateAsync(long id);

}