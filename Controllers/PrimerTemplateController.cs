using ArmoryManagerApi.DataTransferObjects.PrimerTemplateDtos;
using ArmoryManagerApi.Interfaces;
using ArmoryManagerApi.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace ArmoryManagerApi.Controllers;

[EnableCors("CorsPolicy")]
[Route("api/primer")]
[ApiController]
[Authorize]
public class PrimerTemplateController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public PrimerTemplateController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> CreatePrimer(CreatePrimerTemplateDto newPrimerDto)
    {
        if (!long.TryParse(HttpContext.Request.Headers["UserId"].ToString(), out long userId))
        {
            return BadRequest("Invalid User");
        }

        var newPrimer = _mapper.Map<PrimerTemplate>(newPrimerDto);
        newPrimer.UserId = userId;

        _unitOfWork.PrimerTemplateRepository.AddPrimerTemplate(newPrimer);
        await _unitOfWork.SaveAsync();

        return StatusCode(201);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePrimer(long id)
    {
        await _unitOfWork.PrimerTemplateRepository.DeletePrimerTemplateAsync(id);
        await _unitOfWork.SaveAsync();

        return Ok(id);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllPrimers()
    {
        var primerTemplates = await _unitOfWork.PrimerTemplateRepository.GetAllPrimerTemplatesAsync();
        var primerTemplatesDto = _mapper.Map<IEnumerable<PrimerTemplateDto>>(primerTemplates);

        return Ok(primerTemplatesDto);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PrimerTemplateDto>> GetPrimer(long id)
    {
        var primer = await _unitOfWork.PrimerTemplateRepository.GetPrimerTemplateAsync(id);
        var primerDto = _mapper.Map<PrimerTemplateDto>(primer);

        return Ok(primerDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePrimer(long id, PrimerTemplateDto updatedPrimerDto)
    {
        if(id != updatedPrimerDto.Id)
        {
            throw new Exception("id not matching");
        }

        var primer = await _unitOfWork.PrimerTemplateRepository.GetPrimerTemplateAsync(id);
        _mapper.Map(updatedPrimerDto, primer); 

        await _unitOfWork.SaveAsync();

        return NoContent();
    }
}
