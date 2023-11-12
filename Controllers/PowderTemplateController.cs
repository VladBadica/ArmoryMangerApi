using ArmoryManagerApi.Data.Repositories;
using ArmoryManagerApi.DataTransferObjects.PowderTemplateDtos;
using ArmoryManagerApi.Interfaces;
using ArmoryManagerApi.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace ArmoryManagerApi.Controllers;

[EnableCors("CorsPolicy")]
[Route("api/template/powder")]
[ApiController]
[Authorize]
public class PowderTemplateController : ControllerBase
{
    private readonly ArmoryManagerContext _context;
    private readonly IMapper _mapper; 
    private readonly IPowderTemplateRepository _powderTemplateRepository;

    public PowderTemplateController(ArmoryManagerContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
        _powderTemplateRepository = new PowderTemplateRepository(context);
    }

    [HttpPost]
    public async Task<IActionResult> CreatePowder(CreatePowderTemplateDto newPowderDto)
    {
        if (!long.TryParse(HttpContext.Request.Headers["UserId"].ToString(), out long userId))
        {
            return BadRequest("Invalid User");
        }

        var newPowder = _mapper.Map<PowderTemplate>(newPowderDto);
        newPowder.UserId = userId;

        _powderTemplateRepository.AddPowderTemplate(newPowder);
        await _context.SaveChangesAsync();

        return StatusCode(201);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePowder(long id)
    {
        await _powderTemplateRepository.DeletePowderTemplateAsync(id);
        await _context.SaveChangesAsync();

        return Ok(id);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllPowders()
    {
        var powderTemplates = await _powderTemplateRepository.GetAllPowderTemplatesAsync();
        var powderTemplatesDto = _mapper.Map<IEnumerable<PowderTemplateDto>>(powderTemplates);

        return Ok(powderTemplatesDto);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PowderTemplateDto>> GetPowder(long id)
    {
        var powderTemplate = await _powderTemplateRepository.GetPowderTemplateAsync(id);
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

        var powderTemplate = await _powderTemplateRepository.GetPowderTemplateAsync(id);
        _mapper.Map(updatedPowderDto, powderTemplate);

        await _context.SaveChangesAsync();

        return NoContent();
    }  
}
