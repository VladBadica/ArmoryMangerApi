using ArmoryManagerApi.Data.Repositories;
using ArmoryManagerApi.DataTransferObjects.PrimerTemplateDtos;
using ArmoryManagerApi.Interfaces;
using ArmoryManagerApi.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace ArmoryManagerApi.Controllers;

[EnableCors("CorsPolicy")]
[Route("api/template/primer")]
[ApiController]
[Authorize]
public class PrimerTemplateController : ControllerBase
{
    private readonly ArmoryManagerContext _context;
    private readonly IMapper _mapper;
    private readonly IPrimerTemplateRepository _primerTemplateRepository;

    public PrimerTemplateController(ArmoryManagerContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
        _primerTemplateRepository = new PrimerTemplateRepository(context);
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

        _primerTemplateRepository.AddPrimerTemplate(newPrimer);
        await _context.SaveChangesAsync();

        return StatusCode(201);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePrimer(long id)
    {
        await _primerTemplateRepository.DeletePrimerTemplateAsync(id);
        await _context.SaveChangesAsync();

        return Ok(id);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllPrimers()
    {
        var primerTemplates = await _primerTemplateRepository.GetAllPrimerTemplatesAsync();
        var primerTemplatesDto = _mapper.Map<IEnumerable<PrimerTemplateDto>>(primerTemplates);

        return Ok(primerTemplatesDto);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PrimerTemplateDto>> GetPrimer(long id)
    {
        var primer = await _primerTemplateRepository.GetPrimerTemplateAsync(id);
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

        var primer = await _primerTemplateRepository.GetPrimerTemplateAsync(id);
        _mapper.Map(updatedPrimerDto, primer); 

        await _context.SaveChangesAsync();

        return NoContent();
    }
}
