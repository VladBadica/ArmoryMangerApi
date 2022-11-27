using ArmoryManagerApi.Models;

namespace ArmoryManagerApi.DataTransferObjects;

public class BulletPurchaseDto
{
    public long Id { get; set; }

    public string? DatePurchased { get; set; }

    public double? Price { get; set; }

    public long InitialCount { get; set; }

    public long Remaining { get; set; }

    public long? BulletTemplateId { get; set; }

    public virtual BulletTemplate? BulletTemplate { get; set; }
}
