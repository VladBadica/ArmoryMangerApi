using ArmoryManagerApi.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using ArmoryManagerApi.ViewModels;
using ArmoryManagerApi.Helper;
using ArmoryManagerApi.Utils;
using Microsoft.EntityFrameworkCore;

namespace ArmoryManagerApi.Controllers;

[EnableCors("CorsPolicy")]
[Route("api/casing")]
[ApiController]
[Authorize]
public class CasingController : ControllerBase
{
    private readonly ArmoryManagerContext _context;
    private readonly IMapper _mapper;

    public CasingController(ArmoryManagerContext context, IMapper mapper)
	{
        _context = context;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> CreateCasing(CreateCasingVM newCasingDto)
    {
        if (!long.TryParse(HttpContext.Request.Headers["UserId"].ToString(), out long userId))
        {
            return BadRequest("Invalid User");
        }

        var newCasing = _mapper.Map<Casing>(newCasingDto);
        newCasing.UserId = userId;
        newCasing.Remaining = newCasing.InitialCount;
        newCasing.CreatedAt = DateTime.Now.ToString(Constants.DATE_TIME_FORMAT);
        newCasing.UpdatedAt = DateTime.Now.ToString(Constants.DATE_TIME_FORMAT);

        _context.Casings.Add(newCasing);
        await _context.SaveChangesAsync();

        return StatusCode(201);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCasing(long id)
    {
        var casing = _context.Casings.Find(id);

        if (casing == null)
        {
            throw new Exception("Casing puchase id not found");
        }

        _context.Casings.Remove(casing);
        await _context.SaveChangesAsync();

        return Ok(id);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CasingVM>> GetCasing(long id)
    {
        var casing = await _context.Casings.FindAsync(id);

        if (casing == null)
        {
            throw new Exception("Casing puchase id not found");
        }

        var casingDto = _mapper.Map<CasingVM>(casing);

        return Ok(casingDto);
    }

    [HttpGet]
	public async Task<ActionResult<List<CasingVM>>> GetAllCasings()
	{
        var casings = await _context.Casings.ToListAsync();
        var casingsDto = _mapper.Map<IEnumerable<CasingVM>>(casings);

        return Ok(casingsDto);
	}
}