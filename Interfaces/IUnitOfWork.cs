using ArmoryManagerApi.Data.Repositories;

namespace ArmoryManagerApi.Interfaces;

public interface IUnitOfWork
{
    IBulletTemplateRepository BulletTemplateRepository { get; }

    IPowderTemplateRepository PowderTemplateRepository { get; }

    IPrimerTemplateRepository PrimerTemplateRepository { get; }

    IBulletPurchaseRepository BulletPurchaseRepository { get; }

    IPowderPurchaseRepository PowderPurchaseRepository { get; }

    IPrimerPurchaseRepository PrimerPurchaseRepository { get; }

    IReloadRepository ReloadRepository { get; }

    IUserRepository UserRepository { get; }

    Task<bool> SaveAsync();
}
