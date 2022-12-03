using ArmoryManagerApi.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ArmoryManagerApi.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using ArmoryManagerApi.DataTransferObjects.BulletTemplateDtos;

namespace ArmoryManagerApi.Controllers;

[EnableCors("CorsPolicy")]
[Route("api/bullet")]
[ApiController]
[Authorize]
[AllowAnonymous]
public class BulletController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public BulletController(IUnitOfWork unitOfWork, IMapper mapper)
	{
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> CreateBullet(CreateBulletTemplateDto newBulletDto)
    {
        if (!long.TryParse(HttpContext.Request.Headers["UserId"].ToString(), out long userId))
        {
            return BadRequest("Invalid User");
        }

        var newBullet = _mapper.Map<BulletTemplate>(newBulletDto);
        newBullet.UserId = userId;

        _unitOfWork.BulletTemplateRepository.AddBulletTemplate(newBullet);
        await _unitOfWork.SaveAsync();

        return StatusCode(201);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBullet(long id)
    {
        await _unitOfWork.BulletTemplateRepository.DeleteBulletTemplateAsync(id);
        await _unitOfWork.SaveAsync();

        return Ok(id);
    }

    [HttpGet]
    public async Task<ActionResult<List<BulletTemplateDto>>> GetAllBullets()
    {
        var bullets = await _unitOfWork.BulletTemplateRepository.GetAllBulletTemplatesAsync();
        var bulletsDto = _mapper.Map<IEnumerable<BulletTemplateDto>>(bullets);

        return Ok(bulletsDto);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BulletTemplateDto>> GetBullet(long id)
    {
        var bullet = await _unitOfWork.BulletTemplateRepository.GetBulletTemplateAsync(id);
        var bulletDto = _mapper.Map<BulletTemplateDto>(bullet);

        return Ok(bulletDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBullet(long id, BulletTemplateDto updatedBulletDto)
    {       
        if(id != updatedBulletDto.Id)
        {
            return BadRequest("Update not allowed");
        }
        var updatedBullet = await _unitOfWork.BulletTemplateRepository.GetBulletTemplateAsync(id);
        _mapper.Map(updatedBulletDto, updatedBullet);

        await _unitOfWork.SaveAsync();	

        return Ok();
    }
}