using ArmoryManagerApi.Data.Repositories;
using ArmoryManagerApi.Interfaces;
using ArmoryManagerApi.Models;

namespace ArmoryManagerApi.Data;

public class UnitOfWork : IUnitOfWork
{
    private readonly ArmoryManagerContext _context;

    public ICasingTemplateRepository CasingTemplateRepository => new CasingTemplateRepository(_context);
    public IPowderTemplateRepository PowderTemplateRepository => new PowderTemplateRepository(_context);
    public IPrimerTemplateRepository PrimerTemplateRepository => new PrimerTemplateRepository(_context);
    public ICasingRepository CasingRepository => new CasingRepository(_context);
    public IPowderRepository PowderRepository => new PowderRepository(_context);
    public IPrimerRepository PrimerRepository => new PrimerRepository(_context);
    public IReloadRepository ReloadRepository => new ReloadRepository(_context);
    public IUserRepository UserRepository => new UserRepository(_context);

    public UnitOfWork(ArmoryManagerContext context)
    {
        _context = context;
    }

    public async Task<bool> SaveAsync()
    {
       return await _context.SaveChangesAsync() > 0;
    }
}
