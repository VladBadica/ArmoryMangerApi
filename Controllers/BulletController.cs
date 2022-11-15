using ArmoryManagerApi.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ArmoryManagerApi.Controllers;

[EnableCors("CorsPolicy")]
[Route("api/bullet")]
[ApiController]
public class BulletController : ControllerBase
{
	private readonly ArmoryManagerContext _context;

	public BulletController(ArmoryManagerContext context)
	{
		_context = context;
	}

	[HttpGet]
	public async Task<ActionResult<List<Bullet>>> GetBullets()
	{
		return NoContent();
	}

	[HttpGet("{Id}")]
	public async Task<ActionResult<Bullet>> GetBullet(long Id)
	{
		var bullet = await _context.Bullets.FindAsync(Id);
		
		if(bullet == null)
		{
			return NotFound();
		}

		return Ok(bullet);
	}

	[HttpPost]
    public async Task<IActionResult> CreateBullet(Bullet newBullet)
    {
		await _context.Bullets.AddAsync(newBullet);
		await _context.SaveChangesAsync();       

        return Ok(newBullet);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateBullet(Bullet updatedBullet)
    {
        var bullet = await _context.Bullets.FindAsync(updatedBullet.Id);
        
		if (bullet == null)
        {
            return NotFound();
        }

		_context.Entry(bullet).CurrentValues.SetValues(updatedBullet);
		_context.SaveChanges();

        return NoContent();
    }
}
