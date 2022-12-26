using ArmoryManagerApi.Data.Repositories;

namespace ArmoryManagerApi.Interfaces;

public interface IUnitOfWork
{
    ICasingTemplateRepository CasingTemplateRepository { get; }

    IPowderTemplateRepository PowderTemplateRepository { get; }

    IPrimerTemplateRepository PrimerTemplateRepository { get; }

    ICasingRepository CasingRepository { get; }

    IPowderRepository PowderRepository { get; }

    IPrimerRepository PrimerRepository { get; }

    IReloadRepository ReloadRepository { get; }

    IUserRepository UserRepository { get; }

    Task<bool> SaveAsync();
}
