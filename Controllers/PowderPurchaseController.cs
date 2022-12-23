using ArmoryManagerApi.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ArmoryManagerApi.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using ArmoryManagerApi.DataTransferObjects.PowderPurchaseDtos;
using ArmoryManagerApi.Helper;

namespace ArmoryManagerApi.Controllers;

[EnableCors("CorsPolicy")]
[Route("api/powderPurchase")]
[ApiController]
[Authorize]
public class PowderPurchaseController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public PowderPurchaseController(IUnitOfWork unitOfWork, IMapper mapper)
	{
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> CreatePowderPurchase(CreatePowderPurchaseDto newPowderPurchaseDto)
    {
        if (!long.TryParse(HttpContext.Request.Headers["UserId"].ToString(), out long userId))
        {
            return BadRequest("Invalid User");
        }

        var newPowderPurchase = _mapper.Map<PowderPurchase>(newPowderPurchaseDto);
        newPowderPurchase.UserId = userId;
        newPowderPurchase.Remaining = newPowderPurchase.InitialCount;
        newPowderPurchase.CreatedAt = DateTime.Now.ToString(Constants.DATE_TIME_FORMAT);
        newPowderPurchase.UpdatedAt = DateTime.Now.ToString(Constants.DATE_TIME_FORMAT);

        _unitOfWork.PowderPurchaseRepository.AddPowderPurchase(newPowderPurchase);

        await _unitOfWork.SaveAsync();

        return StatusCode(201);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePowderPurchase(long id)
    {
        await _unitOfWork.PowderPurchaseRepository.DeletePowderPurchaseAsync(id);
        await _unitOfWork.SaveAsync();

        return Ok(id);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PowderPurchaseDto>> GetPowderPurchase(long id)
    {
        var powderPurchase = await _unitOfWork.PowderPurchaseRepository.GetPowderPurchaseAsync(id);
        var powderPurchaseDto = _mapper.Map<PowderPurchaseDto>(powderPurchase);

        return Ok(powderPurchaseDto);
    }

    [HttpGet]
	public async Task<ActionResult<List<PowderPurchaseDto>>> GetAllPowderPurchases()
	{
        var powderPurchases = await _unitOfWork.PowderPurchaseRepository.GetAllPowderPurchasesAsync();
        var powderPurchasesDto = _mapper.Map<IEnumerable<PowderPurchaseDto>>(powderPurchases);

        return Ok(powderPurchasesDto);
	}
}