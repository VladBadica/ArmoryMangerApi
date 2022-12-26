using ArmoryManagerApi.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ArmoryManagerApi.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using ArmoryManagerApi.DataTransferObjects.CasingDtos;
using ArmoryManagerApi.Helper;

namespace ArmoryManagerApi.Controllers;

[EnableCors("CorsPolicy")]
[Route("api/casing")]
[ApiController]
[Authorize]
public class CasingController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CasingController(IUnitOfWork unitOfWork, IMapper mapper)
	{
        _unitOfWork = unitOfWork;
        _mapper = mapper;
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

        _unitOfWork.CasingRepository.AddCasing(newCasing);

        await _unitOfWork.SaveAsync();

        return StatusCode(201);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCasing(long id)
    {
        await _unitOfWork.CasingRepository.DeleteCasingAsync(id);
        await _unitOfWork.SaveAsync();

        return Ok(id);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CasingDto>> GetCasing(long id)
    {
        var casing = await _unitOfWork.CasingRepository.GetCasingAsync(id);
        var casingDto = _mapper.Map<CasingDto>(casing);

        return Ok(casingDto);
    }

    [HttpGet]
	public async Task<ActionResult<List<CasingDto>>> GetAllCasings()
	{
        var casings = await _unitOfWork.CasingRepository.GetAllCasingsAsync();
        var casingsDto = _mapper.Map<IEnumerable<CasingDto>>(casings);

        return Ok(casingsDto);
	}
}