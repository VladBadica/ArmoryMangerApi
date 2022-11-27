using ArmoryManagerApi.Models;
using ArmoryManagerApi.DataTransferObjects;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ArmoryManagerApi.Interfaces;
using AutoMapper;

namespace ArmoryManagerApi.Controllers;

[EnableCors("CorsPolicy")]
[Route("api/bulletPurchase")]
[ApiController]
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
    public async Task<IActionResult> CreateBulletPurchase(BulletPurchaseDto newBulletPurchaseDto)
    {
        var newBulletPurchase = _mapper.Map<BulletPurchase>(newBulletPurchaseDto);
        newBulletPurchase.UserId = 1;

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
	public async Task<ActionResult<List<BulletPurchase>>> GetBulletPurchases()
	{
        var bulletPurchases = await _unitOfWork.BulletPurchaseRepository.GetAllBulletPurchasesAsync();
        var bulletPurchasesDto = _mapper.Map<IEnumerable<BulletPurchaseDto>>(bulletPurchases);

        return Ok(bulletPurchasesDto);
	}

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBulletPurchase(long id, BulletPurchaseDto updatedBulletPurchaseDto)
    {       
        if(id != updatedBulletPurchaseDto.Id)
        {
            return BadRequest("Update not allowed");
        }

        var bulletPurchase = await _unitOfWork.BulletPurchaseRepository.GetBulletPurchaseAsync(id);
        _mapper.Map(updatedBulletPurchaseDto, bulletPurchase);

        await _unitOfWork.SaveAsync();	

        return Ok();
    }
}