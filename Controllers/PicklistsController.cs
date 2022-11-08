using ArmoryManagerApi.Models;
using ArmoryManagerApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace ArmoryManagerApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PicklistsController : ControllerBase
{
    public readonly PicklistService _picklistService;

    public PicklistsController(PicklistService picklistService)
    {
        _picklistService = picklistService;
    }

    [HttpGet]
    public async Task<List<Picklist>> Get() =>
            await _picklistService.GetAsync();

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<Picklist>> Get(string id)
    {
        var picklist = await _picklistService.GetAsync(id);

        if(picklist is null)
        {
            return NotFound();
        }

        return picklist;
    }

    [HttpGet("{name}")]
    public async Task<List<Picklist>> GetByName(string name) =>
        await _picklistService.GetAsyncByName(name);

    [HttpPost]
    public async Task<ActionResult<Picklist>> Post(Picklist newPicklist)
    {
        await _picklistService.CreateAsync(newPicklist);

        return CreatedAtAction(nameof(Get), new { id = newPicklist.Id }, newPicklist);
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var picklist = await _picklistService.GetAsync(id);
        if(picklist is null)
        {
            return NotFound();
        }

        await _picklistService.RemoveAsync(id);

        return NoContent();
    }
}
