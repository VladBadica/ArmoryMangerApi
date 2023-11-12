using ArmoryManagerApi.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ArmoryManagerApi.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using ArmoryManagerApi.DataTransferObjects.CasingTemplateDtos;
using ArmoryManagerApi.Data.Repositories;

namespace ArmoryManagerApi.Controllers;

[EnableCors("CorsPolicy")]
[Route("api/template/casing")]
[ApiController]
[Authorize]
public class CasingTemplateController : ControllerBase
{
    private readonly ArmoryManagerContext _context;
    private readonly ICasingTemplateRepository _templateRepository;
    private readonly IMapper _mapper;

    public CasingTemplateController(ArmoryManagerContext context, IMapper mapper)
	{
        _context = context;
        _mapper = mapper;
        _templateRepository = new CasingTemplateRepository(context);
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

        _templateRepository.AddCasingTemplate(newCasing);
        await _context.SaveChangesAsync();

        return StatusCode(201);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCasing(long id)
    {
        await _templateRepository.DeleteCasingTemplateAsync(id);
        await _context.SaveChangesAsync();

        return Ok(id);
    }

    [HttpGet]
    public async Task<ActionResult<List<CasingTemplateDto>>> GetAllCasings()
    {
        var casings = await _templateRepository.GetAllCasingTemplatesAsync();
        var casingsDto = _mapper.Map<IEnumerable<CasingTemplateDto>>(casings);

        return Ok(casingsDto);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CasingTemplateDto>> GetCasing(long id)
    {
        var casing = await _templateRepository.GetCasingTemplateAsync(id);
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
        var updatedCasing = await _templateRepository.GetCasingTemplateAsync(id);
        _mapper.Map(updatedCasingDto, updatedCasing);

        await _context.SaveChangesAsync();	

        return NoContent();
    }
}