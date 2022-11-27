using ArmoryManagerApi.Interfaces;
using ArmoryManagerApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ArmoryManagerApi.Data.Repositories;

public class PrimerTemplateRepository : IPrimerTemplateRepository
{
    private readonly ArmoryManagerContext _context;

    public PrimerTemplateRepository(ArmoryManagerContext context)
    {
        _context = context;
    }
    public void AddPrimerTemplate(PrimerTemplate primerTemplate)
    {
        _context.PrimerTemplates.Add(primerTemplate);
    }

    public async Task DeletePrimerTemplateAsync(long id)
    {
       var primerTemplate = await _context.PrimerTemplates.FindAsync(id);

        if (primerTemplate == null)
        {
            throw new Exception("id not found");
        }

        _context.PrimerTemplates.Remove(primerTemplate);
    }

    public async Task<IEnumerable<PrimerTemplate>> GetAllPrimerTemplatesAsync()
    {
        return await _context.PrimerTemplates.ToListAsync();
    }

    public async Task<PrimerTemplate> GetPrimerTemplateAsync(long id)
    {
        var primerTemplate = await _context.PrimerTemplates.FindAsync(id);

        if(primerTemplate == null)
        {
            throw new Exception("id not found");
        }

        return primerTemplate;
    }
}