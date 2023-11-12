using ArmoryManagerApi.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using ArmoryManagerApi.ViewModels;
using ArmoryManagerApi.Helper;

namespace ArmoryManagerApi.Controllers;

[EnableCors("CorsPolicy")]
[Route("api/powder")]
[ApiController]
[Authorize]
public class PowderController : ControllerBase
{
    private readonly ArmoryManagerContext _context;
    private readonly IMapper _mapper;

    public PowderController(ArmoryManagerContext context, IMapper mapper)
	{
        _context = context;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> CreatePowder(CreatePowderVM newPowderDto)
    {
        if (!long.TryParse(HttpContext.Request.Headers["UserId"].ToString(), out long userId))
        {
            return BadRequest("Invalid User");
        }

        var newPowder = _mapper.Map<Powder>(newPowderDto);
        newPowder.UserId = userId;
        newPowder.Remaining = newPowder.InitialCount;
        newPowder.CreatedAt = DateTime.Now.ToString(Constants.DATE_TIME_FORMAT);
        newPowder.UpdatedAt = DateTime.Now.ToString(Constants.DATE_TIME_FORMAT);

        _context.Powders.Add(newPowder);
        await _context.SaveChangesAsync();

        return StatusCode(201);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePowder(long id)
    {
        var powder = _context.Powders.Find(id);

        if (powder == null)
        {
            throw new Exception("Powder puchase id not found");
        }

        _context.Powders.Remove(powder);
        await _context.SaveChangesAsync();

        return Ok(id);
    }

    [HttpGet("{id}")]
    public ActionResult<PowderVM> GetPowder(long id)
    {
        var powder = _context.Powders.Find(id);
        if (powder == null)
        {
            throw new Exception("Powder puchase id not found");
        }

        var powderDto = _mapper.Map<PowderVM>(powder);

        return Ok(powderDto);
    }

    [HttpGet]
	public ActionResult<List<PowderVM>> GetAllPowders()
	{
        var powders = _context.Powders.ToList();
        var powdersDto = _mapper.Map<IEnumerable<PowderVM>>(powders);

        return Ok(powdersDto);
	}
}