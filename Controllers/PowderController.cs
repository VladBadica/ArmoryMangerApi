using ArmoryManagerApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ArmoryManagerApi.Controllers;

[Route("api/powder")]
[ApiController]
public class PowderController : ControllerBase
{
    private readonly ArmoryManagerContext _context;

    public PowderController(ArmoryManagerContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetPowders()
    {
        return Ok(await _context.Powders.ToListAsync());
    }

    [HttpGet("{Id}")]
    public async Task<ActionResult<Powder>> GetPowder(long Id)
    {
        var powder = await _context.Powders.FindAsync(Id);

        if (powder is null)
        {
            return NotFound();
        }

        return Ok(powder);
    }

    [HttpPost]
    public async Task<IActionResult> CreatePowder(Powder newPowder)
    {
        await _context.Powders.AddAsync(newPowder);
        await _context.SaveChangesAsync();

        return Ok(newPowder);
    }

    [HttpPut]
    public async Task<IActionResult> UpdatePowder(Powder updatedPowder)
    {
        var powder = await _context.Powders.FindAsync(updatedPowder.Id);

        if (powder is null)
        {
            return NotFound();
        }
        _context.Entry(powder).CurrentValues.SetValues(updatedPowder);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
