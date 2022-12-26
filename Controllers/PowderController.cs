using ArmoryManagerApi.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ArmoryManagerApi.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using ArmoryManagerApi.DataTransferObjects.PowderDtos;
using ArmoryManagerApi.Helper;

namespace ArmoryManagerApi.Controllers;

[EnableCors("CorsPolicy")]
[Route("api/powder")]
[ApiController]
[Authorize]
public class PowderController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public PowderController(IUnitOfWork unitOfWork, IMapper mapper)
	{
        _unitOfWork = unitOfWork;
        _mapper = mapper;
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

        _unitOfWork.PowderRepository.AddPowder(newPowder);

        await _unitOfWork.SaveAsync();

        return StatusCode(201);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePowder(long id)
    {
        await _unitOfWork.PowderRepository.DeletePowderAsync(id);
        await _unitOfWork.SaveAsync();

        return Ok(id);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PowderDto>> GetPowder(long id)
    {
        var powder = await _unitOfWork.PowderRepository.GetPowderAsync(id);
        var powderDto = _mapper.Map<PowderDto>(powder);

        return Ok(powderDto);
    }

    [HttpGet]
	public async Task<ActionResult<List<PowderDto>>> GetAllPowders()
	{
        var powders = await _unitOfWork.PowderRepository.GetAllPowdersAsync();
        var powdersDto = _mapper.Map<IEnumerable<PowderDto>>(powders);

        return Ok(powdersDto);
	}
}