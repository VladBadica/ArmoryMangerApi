using ArmoryManagerApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ArmoryManagerApi.Controllers;

[Route("api/primer")]
[ApiController]
public class PrimerController : ControllerBase
{
    private readonly ArmoryManagerContext _context;

    public PrimerController(ArmoryManagerContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetPrimers()
    {
        return Ok(await _context.Primers.ToListAsync());
    }

    [HttpGet("{Id}")]
    public async Task<ActionResult<Primer>> GetPrimer(long Id)
    {
        var primer = await _context.Primers.FindAsync(Id);

        if (primer is null)
        {
            return NotFound();
        }

        return Ok(primer);
    }

    [HttpPost]
    public async Task<IActionResult> CreatePrimer(Primer newPrimer)
    {
        await _context.Primers.AddAsync(newPrimer);
        await _context.SaveChangesAsync();

        return Ok(newPrimer);
    }

    [HttpPut]
    public async Task<IActionResult> UpdatePrimer(Primer updatedPrimer)
    {
        var primer = await _context.Primers.FindAsync(updatedPrimer.Id);

        if (primer is null)
        {
            return NotFound();
        }
        _context.Entry(primer).CurrentValues.SetValues(updatedPrimer);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
