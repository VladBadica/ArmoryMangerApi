using ArmoryManagerApi.Models;
using ArmoryManagerApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace ArmoryManagerApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GunsController : ControllerBase
{
    private readonly GunsService _gunsService;

    public GunsController(GunsService gunsService) =>
        _gunsService = gunsService;

    [HttpGet]
    public async Task<List<Gun>> Get() =>
        await _gunsService.GetAsync();

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<Gun>> Get(string id)
    {
        var gun = await _gunsService.GetAsync(id);

        if (gun is null)
        {
            return NotFound();
        }

        return gun;
    }

    [HttpPost]
    public async Task<IActionResult> Post(Gun newGun)
    {
        await _gunsService.CreateAsync(newGun);

        return CreatedAtAction(nameof(Get), new { id = newGun.Id }, newGun);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, Gun updatedGun)
    {
        var gun = await _gunsService.GetAsync(id);

        if (gun is null)
        {
            return NotFound();
        }

        updatedGun.Id = gun.Id;

        await _gunsService.UpdateAsync(id, updatedGun);

        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var gun = await _gunsService.GetAsync(id);

        if (gun is null)
        {
            return NotFound();
        }

        await _gunsService.RemoveAsync(id);

        return NoContent();
    }
}