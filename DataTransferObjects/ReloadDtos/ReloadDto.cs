using ArmoryManagerApi.DataTransferObjects.BulletPurchaseDtos;
using ArmoryManagerApi.DataTransferObjects.PowderPurchaseDtos;
using ArmoryManagerApi.DataTransferObjects.PrimerPurchaseDtos;

namespace ArmoryManagerApi.DataTransferObjects.ReloadDtos;

public class ReloadDto
{
    public long Id { get; set; }

    public long BulletPurchaseId { get; set; }
    
    public long PowderPurchaseId { get; set; }

    public long PrimerPurchaseId { get; set; }

    public virtual BulletPurchaseDto BulletPurchase { get; set; } = null!;

    public virtual PowderPurchaseDto PowderPurchase { get; set; } = null!;

    public virtual PrimerPurchaseDto PrimerPurchase { get; set; } = null!;

    public string CreatedAt { get; set; } = null!;

    public string UpdatedAt { get; set; } = null!;
}
