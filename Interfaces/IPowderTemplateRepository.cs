using ArmoryManagerApi.Models;

namespace ArmoryManagerApi.Interfaces;

public interface IPowderTemplateRepository
{
    void AddPowderTemplate(PowderTemplate newPowderTemplate);

    Task DeletePowderTemplateAsync(long id);

    Task<IEnumerable<PowderTemplate>> GetAllPowderTemplatesAsync();

    Task<PowderTemplate> GetPowderTemplateAsync(long id);

}