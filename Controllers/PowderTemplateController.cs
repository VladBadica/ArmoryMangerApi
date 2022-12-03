using ArmoryManagerApi.DataTransferObjects.PowderTemplateDtos;
using ArmoryManagerApi.Interfaces;
using ArmoryManagerApi.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace ArmoryManagerApi.Controllers;

[EnableCors("CorsPolicy")]
[Route("api/powder")]
[ApiController]
[Authorize]
[AllowAnonymous]
public class PowderTemplateController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper; 

    public PowderTemplateController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> CreatePowder(CreatePowderTemplateDto newPowderDto)
    {
        if (!long.TryParse(HttpContext.Request.Headers["UserId"].ToString(), out long userId))
        {
            return BadRequest("Invalid User");
        }

        var newPowder = _mapper.Map<PowderTemplate>(newPowderDto);
        //newPowder.UserId = userId;

        _unitOfWork.PowderTemplateRepository.AddPowderTemplate(newPowder);
        await _unitOfWork.SaveAsync();

        return StatusCode(201);
    }

    [HttpDelete]
    public async Task<IActionResult> DeletePowder(long id)
    {
        await _unitOfWork.PowderTemplateRepository.DeletePowderTemplateAsync(id);
        await _unitOfWork.SaveAsync();

        return Ok(id);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllPowders()
    {
        var powderTemplates = await _unitOfWork.PowderTemplateRepository.GetAllPowderTemplatesAsync();
        var powderTemplatesDto = _mapper.Map<IEnumerable<PowderTemplateDto>>(powderTemplates);

        return Ok(powderTemplatesDto);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PowderTemplateDto>> GetPowder(long id)
    {
        var powderTemplate = await _unitOfWork.PowderTemplateRepository.GetPowderTemplateAsync(id);
        var powderTemplateDto = _mapper.Map<PowderTemplateDto>(powderTemplate);

        return Ok(powderTemplateDto);
    }
        
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePowder(long id, PowderTemplateDto updatedPowderDto)
    {
        if(id != updatedPowderDto.Id)
        {
            throw new Exception("id dont match");
        }

        var powderTemplate = await _unitOfWork.PowderTemplateRepository.GetPowderTemplateAsync(id);
        _mapper.Map(updatedPowderDto, powderTemplate);

        await _unitOfWork.SaveAsync();

        return NoContent();
    }  
}
