using ArmoryManagerApi.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using ArmoryManagerApi.ViewModels;
using ArmoryManagerApi.Helper;

namespace ArmoryManagerApi.Controllers;

[EnableCors("CorsPolicy")]
[Route("api/primer")]
[ApiController]
[Authorize]
public class PrimerController : ControllerBase
{
    private readonly ArmoryManagerContext _context;
    private readonly IMapper _mapper;

    public PrimerController(ArmoryManagerContext context, IMapper mapper)
	{
        _context = context;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> CreatePrimer(CreatePrimerVM newPrimerDto)
    {
        if (!long.TryParse(HttpContext.Request.Headers["UserId"].ToString(), out long userId))
        {
            return BadRequest("Invalid User");
        }

        var newPrimer = _mapper.Map<Primer>(newPrimerDto);
        newPrimer.UserId = userId;
        newPrimer.Remaining = newPrimer.InitialCount;
        newPrimer.CreatedAt = DateTime.Now.ToString(Constants.DATE_TIME_FORMAT);
        newPrimer.UpdatedAt = DateTime.Now.ToString(Constants.DATE_TIME_FORMAT);

        _context.Primers.Add(newPrimer);

        await _context.SaveChangesAsync();

        return StatusCode(201);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePrimer(long id)
    {
        var primer = _context.Primers.Find(id);

        if (primer == null)
        {
            throw new Exception("Primer puchase id not found");
        }

        _context.Primers.Remove(primer);
        await _context.SaveChangesAsync();

        return Ok(id);
    }

    [HttpGet("{id}")]
    public ActionResult<PrimerVM> GetPrimer(long id)
    {
        var primer = _context.Primers.Find(id);
        if (primer == null)
        {
            throw new Exception("Primer puchase id not found");
        }

        var primerDto = _mapper.Map<PrimerVM>(primer);

        return Ok(primerDto);
    }

    [HttpGet]
	public ActionResult<PrimerVM> GetAllPrimers()
	{
        var primers = _context.Primers.ToList();
        var primersDto = _mapper.Map<IEnumerable<PrimerVM>>(primers);

        return Ok(primersDto);
	}
}