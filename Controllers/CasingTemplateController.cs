using ArmoryManagerApi.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using ArmoryManagerApi.ViewModels;

namespace ArmoryManagerApi.Controllers;

[EnableCors("CorsPolicy")]
[Route("api/template/casing")]
[ApiController]
[Authorize]
public class CasingTemplateController : ControllerBase
{
    private readonly ArmoryManagerContext _context;
    private readonly IMapper _mapper;

    public CasingTemplateController(ArmoryManagerContext context, IMapper mapper)
	{
        _context = context;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> CreateCasing(CreateCasingTemplateVM newCasingDto)
    {
        if (!long.TryParse(HttpContext.Request.Headers["UserId"].ToString(), out long userId))
        {
            return BadRequest("Invalid User");
        }

        var newCasing = _mapper.Map<CasingTemplate>(newCasingDto);
        newCasing.UserId = userId;

        _context.CasingTemplates.Add(newCasing);
        await _context.SaveChangesAsync();

        return StatusCode(201);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCasing(long id)
    {
        var casingTemplate = _context.CasingTemplates.Find(id);

        if (casingTemplate == null)
        {
            throw new Exception("Casing template id was not found");
        }

        _context.CasingTemplates.Remove(casingTemplate);
        await _context.SaveChangesAsync();

        return Ok(id);
    }

    [HttpGet]
    public ActionResult<List<CasingTemplateVM>> GetAllCasings()
    {
        var casings = _context.CasingTemplates.ToList();
        var casingsDto = _mapper.Map<IEnumerable<CasingTemplateVM>>(casings);

        return Ok(casingsDto);
    }

    [HttpGet("{id}")]
    public ActionResult<CasingTemplateVM> GetCasing(long id)
    {
        var casingTemplate = _context.CasingTemplates.Find(id);

        if (casingTemplate == null)
        {
            throw new Exception("Casing template id was not found");
        }

        var casingDto = _mapper.Map<CasingTemplateVM>(casingTemplate);

        return Ok(casingDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCasing(long id, CasingTemplateVM updatedCasingDto)
    {       
        if(id != updatedCasingDto.Id)
        {
            return BadRequest("Update not allowed");
        }

        var updatedCasing = _context.CasingTemplates.Find(id);
        if (updatedCasing == null)
        {
            throw new Exception("Casing template id was not found");
        }
        _mapper.Map(updatedCasingDto, updatedCasing);

        await _context.SaveChangesAsync();	

        return NoContent();
    }
}