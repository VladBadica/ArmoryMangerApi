using ArmoryManagerApi.DataTransferObjects.CasingPurchaseDtos;
using ArmoryManagerApi.DataTransferObjects.PowderPurchaseDtos;
using ArmoryManagerApi.DataTransferObjects.PrimerPurchaseDtos;

namespace ArmoryManagerApi.DataTransferObjects.ReloadDtos;

public class ReloadDto
{
    public long Id { get; set; }

    public long CasingPurchaseId { get; set; }
    
    public long PowderPurchaseId { get; set; }

    public long PrimerPurchaseId { get; set; }

    public virtual CasingPurchaseDto CasingPurchase { get; set; } = null!;

    public virtual PowderPurchaseDto PowderPurchase { get; set; } = null!;

    public virtual PrimerPurchaseDto PrimerPurchase { get; set; } = null!;

    public string CreatedAt { get; set; } = null!;

    public string UpdatedAt { get; set; } = null!;
}
