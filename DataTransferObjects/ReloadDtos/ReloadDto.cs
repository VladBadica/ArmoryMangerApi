using ArmoryManagerApi.DataTransferObjects.BulletPurchaseDtos;

namespace ArmoryManagerApi.DataTransferObjects.ReloadDtos;

public class ReloadDto
{
    public long Id { get; set; }

    public long BulletPurchaseId { get; set; }

    public virtual BulletPurchaseDto BulletPurchase { get; set; } = null!;

    public string CreatedAt { get; set; } = null!;

    public string UpdatedAt { get; set; } = null!;
}
