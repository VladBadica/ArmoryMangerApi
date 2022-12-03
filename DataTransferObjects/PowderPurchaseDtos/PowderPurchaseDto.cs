using ArmoryManagerApi.Models;

namespace ArmoryManagerApi.DataTransferObjects.PowderPurchaseDtos;

public class PowderPurchaseDto
{
    public long Id { get; set; }

    public string? DatePurchased { get; set; }

    public long? Price { get; set; }

    public long InitialCount { get; set; }

    public long Remaining { get; set; }

    public long PowderTemplateId { get; set; }

    public string CreatedAt { get; set; } = null!;

    public string UpdatedAt { get; set; } = null!;

    public virtual PowderTemplate PowderTemplate { get; set; } = null!;
}
