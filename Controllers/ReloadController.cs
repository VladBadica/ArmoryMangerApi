using ArmoryManagerApi.Data.Repositories;
using ArmoryManagerApi.DataTransferObjects.ReloadDtos;
using ArmoryManagerApi.Helper;
using ArmoryManagerApi.Interfaces;
using ArmoryManagerApi.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace ArmoryManagerApi.Controllers;

[EnableCors("CorsPolicy")]
[Route("api/reload")]
[ApiController]
[Authorize]
public class ReloadController : ControllerBase
{
    private readonly ArmoryManagerContext _context;
    private readonly IMapper _mapper;
    private readonly IReloadRepository _reloadRepository;
    private readonly ICasingRepository _casingRepository;
    private readonly IPrimerRepository _primerRepository;
    private readonly IPowderRepository _powderRepository;
    public ReloadController(ArmoryManagerContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
        _reloadRepository = new ReloadRepository(context);
        _casingRepository = new CasingRepository(context);
        _primerRepository = new PrimerRepository(context);
        _powderRepository = new PowderRepository(context);
    }

    [HttpPost]
    public async Task<IActionResult> CreateReload(CreateReloadDto newReload)
    {
        if (!long.TryParse(HttpContext.Request.Headers["UserId"].ToString(), out long userId))
        {
            return BadRequest("Invalid User");
        }

        var reload = _mapper.Map<Reload>(newReload);
        reload.UserId = userId;
        reload.CreatedAt = DateTime.Now.ToString(Constants.DATE_TIME_FORMAT);
        reload.UpdatedAt = DateTime.Now.ToString(Constants.DATE_TIME_FORMAT);

        _reloadRepository.AddReload(reload);

        await _casingRepository.ConsumeCasings(newReload.CasingId, newReload.CasingCount);
        await _primerRepository.ConsumePrimers(newReload.PrimerId, newReload.PrimerCount);
        await _powderRepository.ConsumePowders(newReload.PowderId, newReload.PowderCount);

        await _context.SaveChangesAsync();

        return StatusCode(201);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllReloads()
    {     
        var reloads = await _reloadRepository.GetAllReloadsAsync();
        var reloadsDto = _mapper.Map<IEnumerable<ReloadDto>>(reloads);

        return Ok(reloadsDto);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetReload(long id)
    {
        var reload = await _reloadRepository.GetReloadAsync(id);
        var reloadDto = _mapper.Map<ReloadDto>(reload);
        
        return Ok(reloadDto);
    } 
}
