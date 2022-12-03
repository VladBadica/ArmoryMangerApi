using ArmoryManagerApi.DataTransferObjects.BulletTemplateDtos;

namespace ArmoryManagerApi.DataTransferObjects.BulletPurchaseDtos;

public class BulletPurchaseDto
{
    public long Id { get; set; }

    public string? DatePurchased { get; set; }

    public double? Price { get; set; }

    public long InitialCount { get; set; }

    public long Remaining { get; set; }

    public long BulletTemplateId { get; set; }

    public virtual BulletTemplateDto BulletTemplate { get; set; } = null!;

    public string CreatedAt { get; set; } = null!;

    public string UpdatedAt { get; set; } = null!;
}
