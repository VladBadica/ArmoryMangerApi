using ArmoryManagerApi.ViewModels;
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

    public PrimerTemplateController(ArmoryManagerContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> CreatePrimer(CreatePrimerTemplateVM newPrimerTemplateDto)
    {
        if (!long.TryParse(HttpContext.Request.Headers["UserId"].ToString(), out long userId))
        {
            return BadRequest("Invalid User");
        }

        var newPrimerTemplate = _mapper.Map<PrimerTemplate>(newPrimerTemplateDto);
        newPrimerTemplate.UserId = userId;

        _context.PrimerTemplates.Add(newPrimerTemplate);
        await _context.SaveChangesAsync();

        return StatusCode(201);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePrimer(long id)
    {
        var primerTemplate = _context.PrimerTemplates.Find(id);
        if (primerTemplate == null)
        {
            throw new Exception("id not found");
        }

        _context.PrimerTemplates.Remove(primerTemplate);
        await _context.SaveChangesAsync();

        return Ok(id);
    }

    [HttpGet]
    public ActionResult<List<PrimerTemplateVM>> GetAllPrimers()
    {
        var primerTemplates = _context.PrimerTemplates.ToList();
        var primerTemplatesDto = _mapper.Map<IEnumerable<PrimerTemplateVM>>(primerTemplates);

        return Ok(primerTemplatesDto);
    }

    [HttpGet("{id}")]
    public ActionResult<PrimerTemplateVM> GetPrimer(long id)
    {
        var primerTemplate = _context.PrimerTemplates.Find(id);
        if (primerTemplate == null)
        {
            throw new Exception("id not found");
        }

        return Ok(_mapper.Map<PrimerTemplateVM>(primerTemplate));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePrimer(long id, PrimerTemplateVM updatedPrimerDto)
    {
        if(id != updatedPrimerDto.Id)
        {
            throw new Exception("id not matching");
        }

        var primerTemplate = _context.PrimerTemplates.Find(id);
        if (primerTemplate == null)
        {
            throw new Exception("id not found");
        }
        
        _mapper.Map(updatedPrimerDto, primerTemplate); 

        await _context.SaveChangesAsync();

        return NoContent();
    }
}
