using ArmoryManagerApi.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ArmoryManagerApi.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using ArmoryManagerApi.DataTransferObjects.PrimerDtos;
using ArmoryManagerApi.Helper;
using ArmoryManagerApi.Data.Repositories;

namespace ArmoryManagerApi.Controllers;

[EnableCors("CorsPolicy")]
[Route("api/primer")]
[ApiController]
[Authorize]
public class PrimerController : ControllerBase
{
    private readonly ArmoryManagerContext _context;
    private readonly IMapper _mapper;
    private readonly IPrimerRepository _primerRepository;

    public PrimerController(ArmoryManagerContext context, IMapper mapper)
	{
        _context = context;
        _mapper = mapper;
        _primerRepository = new PrimerRepository(context);
    }

    [HttpPost]
    public async Task<IActionResult> CreatePrimer(CreatePrimerDto newPrimerDto)
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

        _primerRepository.AddPrimer(newPrimer);

        await _context.SaveChangesAsync();

        return StatusCode(201);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePrimer(long id)
    {
        await _primerRepository.DeletePrimerAsync(id);
        await _context.SaveChangesAsync();

        return Ok(id);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PrimerDto>> GetPrimer(long id)
    {
        var primer = await _primerRepository.GetPrimerAsync(id);
        var primerDto = _mapper.Map<PrimerDto>(primer);

        return Ok(primerDto);
    }

    [HttpGet]
	public async Task<ActionResult<List<PrimerDto>>> GetAllPrimers()
	{
        var primers = await _primerRepository.GetAllPrimersAsync();
        var primersDto = _mapper.Map<IEnumerable<PrimerDto>>(primers);

        return Ok(primersDto);
	}
}