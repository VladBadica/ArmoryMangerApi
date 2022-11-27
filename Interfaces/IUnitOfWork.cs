namespace ArmoryManagerApi.Interfaces;

public interface IUnitOfWork
{
    IBulletTemplateRepository BulletTemplateRepository { get; }

    IPowderTemplateRepository PowderTemplateRepository { get; }

    IPrimerTemplateRepository PrimerTemplateRepository { get; }

    IBulletPurchaseRepository BulletPurchaseRepository { get; }

    Task<bool> SaveAsync();
}
