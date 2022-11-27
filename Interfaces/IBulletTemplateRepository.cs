using ArmoryManagerApi.Models;

namespace ArmoryManagerApi.Interfaces;

public interface IBulletTemplateRepository
{
    void AddBulletTemplate(BulletTemplate bulletTemplate);

    Task DeleteBulletTemplateAsync(long id);

    Task<IEnumerable<BulletTemplate>> GetAllBulletTemplatesAsync();

    Task<BulletTemplate> GetBulletTemplateAsync(long id);


}
