using ArmoryManagerApi.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ArmoryManagerApi.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using ArmoryManagerApi.DataTransferObjects.CasingPurchaseDtos;
using ArmoryManagerApi.Helper;

namespace ArmoryManagerApi.Controllers;

[EnableCors("CorsPolicy")]
[Route("api/casingPurchase")]
[ApiController]
[Authorize]
public class CasingPurchaseController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CasingPurchaseController(IUnitOfWork unitOfWork, IMapper mapper)
	{
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> CreateCasingPurchase(CreateCasingPurchaseDto newCasingPurchaseDto)
    {
        if (!long.TryParse(HttpContext.Request.Headers["UserId"].ToString(), out long userId))
        {
            return BadRequest("Invalid User");
        }

        var newCasingPurchase = _mapper.Map<CasingPurchase>(newCasingPurchaseDto);
        newCasingPurchase.UserId = userId;
        newCasingPurchase.Remaining = newCasingPurchase.InitialCount;
        newCasingPurchase.CreatedAt = DateTime.Now.ToString(Constants.DATE_TIME_FORMAT);
        newCasingPurchase.UpdatedAt = DateTime.Now.ToString(Constants.DATE_TIME_FORMAT);

        _unitOfWork.CasingPurchaseRepository.AddCasingPurchase(newCasingPurchase);

        await _unitOfWork.SaveAsync();

        return StatusCode(201);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCasingPurchase(long id)
    {
        await _unitOfWork.CasingPurchaseRepository.DeleteCasingPurchaseAsync(id);
        await _unitOfWork.SaveAsync();

        return Ok(id);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CasingPurchaseDto>> GetCasingPurchase(long id)
    {
        var casingPurchase = await _unitOfWork.CasingPurchaseRepository.GetCasingPurchaseAsync(id);
        var casingPurchaseDto = _mapper.Map<CasingPurchaseDto>(casingPurchase);

        return Ok(casingPurchaseDto);
    }

    [HttpGet]
	public async Task<ActionResult<List<CasingPurchaseDto>>> GetAllCasingPurchases()
	{
        var casingPurchases = await _unitOfWork.CasingPurchaseRepository.GetAllCasingPurchasesAsync();
        var casingPurchasesDto = _mapper.Map<IEnumerable<CasingPurchaseDto>>(casingPurchases);

        return Ok(casingPurchasesDto);
	}
}