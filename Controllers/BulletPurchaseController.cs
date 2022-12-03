using ArmoryManagerApi.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ArmoryManagerApi.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using ArmoryManagerApi.DataTransferObjects.BulletPurchaseDtos;
using ArmoryManagerApi.Helper;

namespace ArmoryManagerApi.Controllers;

[EnableCors("CorsPolicy")]
[Route("api/bulletPurchase")]
[ApiController]
[Authorize]
[AllowAnonymous]
public class BulletPurchaseController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public BulletPurchaseController(IUnitOfWork unitOfWork, IMapper mapper)
	{
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> CreateBulletPurchase(CreateBulletPurchaseDto newBulletPurchaseDto)
    {
        if (!long.TryParse(HttpContext.Request.Headers["UserId"].ToString(), out long userId))
        {
            return BadRequest("Invalid User");
        }

        var newBulletPurchase = _mapper.Map<BulletPurchase>(newBulletPurchaseDto);
        newBulletPurchase.UserId = userId;
        newBulletPurchase.Remaining = newBulletPurchase.InitialCount;
        newBulletPurchase.CreatedAt = DateTime.Now.ToString(Constants.DATE_TIME_FORMAT);
        newBulletPurchase.UpdatedAt = DateTime.Now.ToString(Constants.DATE_TIME_FORMAT);

        _unitOfWork.BulletPurchaseRepository.AddBulletPurchase(newBulletPurchase);

        await _unitOfWork.SaveAsync();

        return StatusCode(201);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBulletPurchase(long id)
    {
        await _unitOfWork.BulletPurchaseRepository.DeleteBulletPurchaseAsync(id);
        await _unitOfWork.SaveAsync();

        return Ok(id);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BulletPurchaseDto>> GetBulletPurchase(long id)
    {
        var bulletPurchase = await _unitOfWork.BulletPurchaseRepository.GetBulletPurchaseAsync(id);
        var bulletPurchaseDto = _mapper.Map<BulletPurchaseDto>(bulletPurchase);

        return Ok(bulletPurchaseDto);
    }

    [HttpGet]
	public async Task<ActionResult<List<BulletPurchaseDto>>> GetAllBulletPurchases()
	{
        var bulletPurchases = await _unitOfWork.BulletPurchaseRepository.GetAllBulletPurchasesAsync();
        var bulletPurchasesDto = _mapper.Map<IEnumerable<BulletPurchaseDto>>(bulletPurchases);

        return Ok(bulletPurchasesDto);
	}
}