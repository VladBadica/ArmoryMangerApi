using ArmoryManagerApi.Interfaces;
using ArmoryManagerApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ArmoryManagerApi.Data.Repositories;

public class PowderTemplateRepository : IPowderTemplateRepository
{
    private readonly ArmoryManagerContext _context;

    public PowderTemplateRepository(ArmoryManagerContext context)
    {
        _context = context;
    }
    public void AddPowderTemplate(PowderTemplate newPowderTemplate)
    {
        _context.PowderTemplates.Add(newPowderTemplate);
    }

    public async Task DeletePowderTemplateAsync(long id)
    {
        var powderTemplate = await _context.PowderTemplates.FindAsync(id);

        if(powderTemplate == null)
        {
            throw new Exception("id not found");
        }

        _context.PowderTemplates.Remove(powderTemplate);
    }

    public async Task<IEnumerable<PowderTemplate>> GetAllPowderTemplatesAsync()
    {
        return await _context.PowderTemplates.ToListAsync();
    }

    public async Task<PowderTemplate> GetPowderTemplateAsync(long id)
    {
        var powderTemplate = await _context.PowderTemplates.FindAsync(id);

        if (powderTemplate == null)
        {
            throw new Exception("id not found");
        }

        return powderTemplate;
    }
}