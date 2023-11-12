using ArmoryManagerApi.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ArmoryManagerApi.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using ArmoryManagerApi.DataTransferObjects.PowderDtos;
using ArmoryManagerApi.Helper;
using ArmoryManagerApi.Data.Repositories;

namespace ArmoryManagerApi.Controllers;

[EnableCors("CorsPolicy")]
[Route("api/powder")]
[ApiController]
[Authorize]
public class PowderController : ControllerBase
{
    private readonly ArmoryManagerContext _context;
    private readonly IMapper _mapper;
    private readonly IPowderRepository _powderRepository;

    public PowderController(ArmoryManagerContext context, IMapper mapper)
	{
        _context = context;
        _mapper = mapper;
        _powderRepository = new PowderRepository(context);
    }

    [HttpPost]
    public async Task<IActionResult> CreatePowder(CreatePowderDto newPowderDto)
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

        _powderRepository.AddPowder(newPowder);

        await _context.SaveChangesAsync();

        return StatusCode(201);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePowder(long id)
    {
        await _powderRepository.DeletePowderAsync(id);
        await _context.SaveChangesAsync();

        return Ok(id);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PowderDto>> GetPowder(long id)
    {
        var powder = await _powderRepository.GetPowderAsync(id);
        var powderDto = _mapper.Map<PowderDto>(powder);

        return Ok(powderDto);
    }

    [HttpGet]
	public async Task<ActionResult<List<PowderDto>>> GetAllPowders()
	{
        var powders = await _powderRepository.GetAllPowdersAsync();
        var powdersDto = _mapper.Map<IEnumerable<PowderDto>>(powders);

        return Ok(powdersDto);
	}
}