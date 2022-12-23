using ArmoryManagerApi.Data.Repositories;

namespace ArmoryManagerApi.Interfaces;

public interface IUnitOfWork
{
    ICasingTemplateRepository CasingTemplateRepository { get; }

    IPowderTemplateRepository PowderTemplateRepository { get; }

    IPrimerTemplateRepository PrimerTemplateRepository { get; }

    ICasingPurchaseRepository CasingPurchaseRepository { get; }

    IPowderPurchaseRepository PowderPurchaseRepository { get; }

    IPrimerPurchaseRepository PrimerPurchaseRepository { get; }

    IReloadRepository ReloadRepository { get; }

    IUserRepository UserRepository { get; }

    Task<bool> SaveAsync();
}
