using ArmoryManagerApi.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ArmoryManagerApi.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using ArmoryManagerApi.DataTransferObjects.PrimerPurchaseDtos;
using ArmoryManagerApi.Helper;

namespace ArmoryManagerApi.Controllers;

[EnableCors("CorsPolicy")]
[Route("api/primerPurchase")]
[ApiController]
[Authorize]
[AllowAnonymous]
public class PrimerPurchaseController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public PrimerPurchaseController(IUnitOfWork unitOfWork, IMapper mapper)
	{
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> CreatePrimerPurchase(CreatePrimerPurchaseDto newPrimerPurchaseDto)
    {
        if (!long.TryParse(HttpContext.Request.Headers["UserId"].ToString(), out long userId))
        {
            return BadRequest("Invalid User");
        }

        var newPrimerPurchase = _mapper.Map<PrimerPurchase>(newPrimerPurchaseDto);
        newPrimerPurchase.UserId = userId;
        newPrimerPurchase.Remaining = newPrimerPurchase.InitialCount;
        newPrimerPurchase.CreatedAt = DateTime.Now.ToString(Constants.DATE_TIME_FORMAT);
        newPrimerPurchase.UpdatedAt = DateTime.Now.ToString(Constants.DATE_TIME_FORMAT);

        _unitOfWork.PrimerPurchaseRepository.AddPrimerPurchase(newPrimerPurchase);

        await _unitOfWork.SaveAsync();

        return StatusCode(201);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePrimerPurchase(long id)
    {
        await _unitOfWork.PrimerPurchaseRepository.DeletePrimerPurchaseAsync(id);
        await _unitOfWork.SaveAsync();

        return Ok(id);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PrimerPurchaseDto>> GetPrimerPurchase(long id)
    {
        var primerPurchase = await _unitOfWork.PrimerPurchaseRepository.GetPrimerPurchaseAsync(id);
        var primerPurchaseDto = _mapper.Map<PrimerPurchaseDto>(primerPurchase);

        return Ok(primerPurchaseDto);
    }

    [HttpGet]
	public async Task<ActionResult<List<PrimerPurchaseDto>>> GetAllPrimerPurchases()
	{
        var primerPurchases = await _unitOfWork.PrimerPurchaseRepository.GetAllPrimerPurchasesAsync();
        var primerPurchasesDto = _mapper.Map<IEnumerable<PrimerPurchaseDto>>(primerPurchases);

        return Ok(primerPurchasesDto);
	}
}