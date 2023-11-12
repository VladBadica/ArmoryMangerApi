using ArmoryManagerApi.ViewModels;
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

    public PowderTemplateController(ArmoryManagerContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> CreatePowder(CreatePowderTemplateVM newPowderDto)
    {
        if (!long.TryParse(HttpContext.Request.Headers["UserId"].ToString(), out long userId))
        {
            return BadRequest("Invalid User");
        }

        var newPowderTemplate = _mapper.Map<PowderTemplate>(newPowderDto);
        newPowderTemplate.UserId = userId;

        _context.PowderTemplates.Add(newPowderTemplate);
        await _context.SaveChangesAsync();

        return StatusCode(201);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePowder(long id)
    {
        var powderTemplate = _context.PowderTemplates.Find(id);

        if (powderTemplate == null)
        {
            throw new Exception("id not found");
        }

        _context.PowderTemplates.Remove(powderTemplate);
        await _context.SaveChangesAsync();

        return Ok(id);
    }

    [HttpGet]
    public ActionResult<List<PowderTemplateVM>> GetAllPowders()
    {
        var powderTemplates = _context.PowderTemplates.ToList();
        var powderTemplatesDto = _mapper.Map<IEnumerable<PowderTemplateVM>>(powderTemplates);

        return Ok(powderTemplatesDto);
    }

    [HttpGet("{id}")]
    public ActionResult<PowderVM> GetPowder(long id)
    {
        var powderTemplate = _context.PowderTemplates.Find(id);
        if (powderTemplate == null)
        {
            throw new Exception("id not found");
        }

        var powderTemplateDto = _mapper.Map<PowderTemplateVM>(powderTemplate);

        return Ok(powderTemplateDto);
    }
        
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePowder(long id, PowderTemplateVM updatedPowderDto)
    {
        if(id != updatedPowderDto.Id)
        {
            throw new Exception("id dont match");
        }

        var powderTemplate = _context.PowderTemplates.Find(id);

        if (powderTemplate == null)
        {
            throw new Exception("id not found");
        }
        _mapper.Map(updatedPowderDto, powderTemplate);

        await _context.SaveChangesAsync();

        return NoContent();
    }  
}
