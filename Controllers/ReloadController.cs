using ArmoryManagerApi.ViewModels;
using ArmoryManagerApi.Helper;
using ArmoryManagerApi.Models;
using ArmoryManagerApi.Utils;
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
    private readonly ReloadUtils _reloadUtils;
    public ReloadController(ArmoryManagerContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
        _reloadUtils = new ReloadUtils(context);
    }

    [HttpPost]
    public async Task<IActionResult> CreateReload(CreateReloadVM newReload)
    {
        if (!long.TryParse(HttpContext.Request.Headers["UserId"].ToString(), out long userId))
        {
            return BadRequest("Invalid User");
        }

        var reload = _mapper.Map<Models.Reload>(newReload);
        reload.UserId = userId;
        reload.CreatedAt = DateTime.Now.ToString(Constants.DATE_TIME_FORMAT);
        reload.UpdatedAt = DateTime.Now.ToString(Constants.DATE_TIME_FORMAT);

        _context.Reloads.Add(reload);

        var primer = _context.Primers.Find(newReload.PrimerId);
        if (primer == null)
        {
            throw new Exception("Casing puchase id not found");
        }
        if (primer.Remaining < newReload.PrimerCount)
        {
            throw new Exception("Not enough casings remaining");
        }
        primer.Remaining -= newReload.PrimerCount;

        var casing = _context.Casings.Find(newReload.CasingId);
        if (casing == null)
        {
            throw new Exception("Casing puchase id not found");
        }
        if (casing.Remaining < newReload.CasingCount)
        {
            throw new Exception("Not enough casings remaining");
        }
        casing.Remaining -= newReload.CasingCount;


        var powder = _context.Powders.Find(newReload.PowderId);
        if (powder == null)
        {
            throw new Exception("Casing puchase id not found");
        }
        if (powder.Remaining < newReload.PowderCount)
        {
            throw new Exception("Not enough casings remaining");
        }
        powder.Remaining -= newReload.PowderCount;

        await _context.SaveChangesAsync();

        return StatusCode(201);
    }

    [HttpGet]
    public ActionResult<ReloadVM> GetAllReloads()
    {     
        var reloads = _reloadUtils.GetAllReloads();
        var reloadsDto = _mapper.Map<IEnumerable<ReloadVM>>(reloads);

        return Ok(reloadsDto);
    }

    [HttpGet("{id}")]
    public ActionResult<ReloadVM> GetReload(long id)
    {
        var reload = _reloadUtils.GetReload(id);

        if (reload == null)
        {
            throw new Exception("id not found");
        }

        var reloadDto = _mapper.Map<ReloadVM>(reload);
        
        return Ok(reloadDto);
    } 
}
