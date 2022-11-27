using ArmoryManagerApi.DataTransferObjects;
using ArmoryManagerApi.Interfaces;
using ArmoryManagerApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ArmoryManagerApi.Data.Repositories;

public class BulletTemplateRepository : IBulletTemplateRepository
{
    private readonly ArmoryManagerContext _context;

    public BulletTemplateRepository(ArmoryManagerContext context)
    {
        _context = context;
    }

    public void AddBulletTemplate(BulletTemplate bulletTemplate)
    {
        _context.BulletTemplates.Add(bulletTemplate);
    }

    public async Task DeleteBulletTemplateAsync(long id)
    {
        var bulletTemplate = await _context.BulletTemplates.FindAsync(id);

        if (bulletTemplate == null)
        {
            throw new Exception("Bullet template id was not found");
        }

        _context.BulletTemplates.Remove(bulletTemplate);
    }
    public async Task<IEnumerable<BulletTemplate>> GetAllBulletTemplatesAsync()
    {
        return await _context.BulletTemplates.ToListAsync();
    }

    public async Task<BulletTemplate> GetBulletTemplateAsync(long id)
    {
        var bulletTemplate =  await _context.BulletTemplates.FindAsync(id);

        if (bulletTemplate == null)
        {
            throw new Exception("Bullet template id was not found");
        }

        return bulletTemplate;
    }    
}
