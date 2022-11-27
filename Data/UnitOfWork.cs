using ArmoryManagerApi.Data.Repositories;
using ArmoryManagerApi.Interfaces;
using AutoMapper;

namespace ArmoryManagerApi.Data;

public class UnitOfWork : IUnitOfWork
{
    private readonly ArmoryManagerContext _context;
    private readonly IMapper _mapper;

    public IBulletTemplateRepository BulletTemplateRepository => new BulletTemplateRepository(_context);
    public IPowderTemplateRepository PowderTemplateRepository => new PowderTemplateRepository(_context);
    public IPrimerTemplateRepository PrimerTemplateRepository => new PrimerTemplateRepository(_context);
    public IBulletPurchaseRepository BulletPurchaseRepository => new BulletPurchaseRepository(_context, _mapper);

    public UnitOfWork(ArmoryManagerContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<bool> SaveAsync()
    {
       return await _context.SaveChangesAsync() > 0;
    }
}
