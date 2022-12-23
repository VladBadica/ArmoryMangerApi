using ArmoryManagerApi.Models;

namespace ArmoryManagerApi.Interfaces;

public interface ICasingTemplateRepository
{
    void AddCasingTemplate(CasingTemplate casingTemplate);

    Task DeleteCasingTemplateAsync(long id);

    Task<IEnumerable<CasingTemplate>> GetAllCasingTemplatesAsync();

    Task<CasingTemplate> GetCasingTemplateAsync(long id);


}
