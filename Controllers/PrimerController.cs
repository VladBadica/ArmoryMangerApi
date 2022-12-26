using ArmoryManagerApi.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ArmoryManagerApi.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using ArmoryManagerApi.DataTransferObjects.PrimerDtos;
using ArmoryManagerApi.Helper;

namespace ArmoryManagerApi.Controllers;

[EnableCors("CorsPolicy")]
[Route("api/primer")]
[ApiController]
[Authorize]
public class PrimerController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public PrimerController(IUnitOfWork unitOfWork, IMapper mapper)
	{
        _unitOfWork = unitOfWork;
        _mapper = mapper;
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

        _unitOfWork.PrimerRepository.AddPrimer(newPrimer);

        await _unitOfWork.SaveAsync();

        return StatusCode(201);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePrimer(long id)
    {
        await _unitOfWork.PrimerRepository.DeletePrimerAsync(id);
        await _unitOfWork.SaveAsync();

        return Ok(id);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PrimerDto>> GetPrimer(long id)
    {
        var primer = await _unitOfWork.PrimerRepository.GetPrimerAsync(id);
        var primerDto = _mapper.Map<PrimerDto>(primer);

        return Ok(primerDto);
    }

    [HttpGet]
	public async Task<ActionResult<List<PrimerDto>>> GetAllPrimers()
	{
        var primers = await _unitOfWork.PrimerRepository.GetAllPrimersAsync();
        var primersDto = _mapper.Map<IEnumerable<PrimerDto>>(primers);

        return Ok(primersDto);
	}
}