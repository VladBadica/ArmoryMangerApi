using ArmoryManagerApi.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ArmoryManagerApi.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using ArmoryManagerApi.DataTransferObjects.CasingDtos;
using ArmoryManagerApi.Helper;
using ArmoryManagerApi.Data.Repositories;

namespace ArmoryManagerApi.Controllers;

[EnableCors("CorsPolicy")]
[Route("api/casing")]
[ApiController]
[Authorize]
public class CasingController : ControllerBase
{
    private readonly ArmoryManagerContext _context;
    private readonly ICasingRepository _casingRepository;
    private readonly IMapper _mapper;

    public CasingController(ArmoryManagerContext context, IMapper mapper)
	{
        _context = context;
        _mapper = mapper;
        _casingRepository = new CasingRepository(context);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCasing(CreateCasingDto newCasingDto)
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

        _casingRepository.AddCasing(newCasing);

        await _context.SaveChangesAsync();

        return StatusCode(201);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCasing(long id)
    {
        await _casingRepository.DeleteCasingAsync(id);
        await _context.SaveChangesAsync();

        return Ok(id);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CasingDto>> GetCasing(long id)
    {
        var casing = await _casingRepository.GetCasingAsync(id);
        var casingDto = _mapper.Map<CasingDto>(casing);

        return Ok(casingDto);
    }

    [HttpGet]
	public async Task<ActionResult<List<CasingDto>>> GetAllCasings()
	{
        var casings = await _casingRepository.GetAllCasingsAsync();
        var casingsDto = _mapper.Map<IEnumerable<CasingDto>>(casings);

        return Ok(casingsDto);
	}
}