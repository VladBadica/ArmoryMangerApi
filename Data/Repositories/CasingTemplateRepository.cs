using ArmoryManagerApi.Interfaces;
using ArmoryManagerApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ArmoryManagerApi.Data.Repositories;

public class CasingTemplateRepository : ICasingTemplateRepository
{
    private readonly ArmoryManagerContext _context;

    public CasingTemplateRepository(ArmoryManagerContext context)
    {
        _context = context;
    }

    public void AddCasingTemplate(CasingTemplate casingTemplate)
    {
        _context.CasingTemplates.Add(casingTemplate);
    }

    public async Task DeleteCasingTemplateAsync(long id)
    {
        var casingTemplate = await _context.CasingTemplates.FindAsync(id);

        if (casingTemplate == null)
        {
            throw new Exception("Casing template id was not found");
        }

        _context.CasingTemplates.Remove(casingTemplate);
    }

    public async Task<IEnumerable<CasingTemplate>> GetAllCasingTemplatesAsync()
    {
        return await _context.CasingTemplates.ToListAsync();
    }

    public async Task<CasingTemplate> GetCasingTemplateAsync(long id)
    {
        var casingTemplate =  await _context.CasingTemplates.FindAsync(id);

        if (casingTemplate == null)
        {
            throw new Exception("Casing template id was not found");
        }

        return casingTemplate;
    }    
}
