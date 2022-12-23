using ArmoryManagerApi.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ArmoryManagerApi.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using ArmoryManagerApi.DataTransferObjects.CasingTemplateDtos;

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
    public async Task<IActionResult> CreateCasing(CreateCasingTemplateDto newCasingDto)
    {
        if (!long.TryParse(HttpContext.Request.Headers["UserId"].ToString(), out long userId))
        {
            return BadRequest("Invalid User");
        }

        var newCasing = _mapper.Map<CasingTemplate>(newCasingDto);
        newCasing.UserId = userId;

        _unitOfWork.CasingTemplateRepository.AddCasingTemplate(newCasing);
        await _unitOfWork.SaveAsync();

        return StatusCode(201);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCasing(long id)
    {
        await _unitOfWork.CasingTemplateRepository.DeleteCasingTemplateAsync(id);
        await _unitOfWork.SaveAsync();

        return Ok(id);
    }

    [HttpGet]
    public async Task<ActionResult<List<CasingTemplateDto>>> GetAllCasings()
    {
        var casings = await _unitOfWork.CasingTemplateRepository.GetAllCasingTemplatesAsync();
        var casingsDto = _mapper.Map<IEnumerable<CasingTemplateDto>>(casings);

        return Ok(casingsDto);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CasingTemplateDto>> GetCasing(long id)
    {
        var casing = await _unitOfWork.CasingTemplateRepository.GetCasingTemplateAsync(id);
        var casingDto = _mapper.Map<CasingTemplateDto>(casing);

        return Ok(casingDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCasing(long id, CasingTemplateDto updatedCasingDto)
    {       
        if(id != updatedCasingDto.Id)
        {
            return BadRequest("Update not allowed");
        }
        var updatedCasing = await _unitOfWork.CasingTemplateRepository.GetCasingTemplateAsync(id);
        _mapper.Map(updatedCasingDto, updatedCasing);

        await _unitOfWork.SaveAsync();	

        return NoContent();
    }
}