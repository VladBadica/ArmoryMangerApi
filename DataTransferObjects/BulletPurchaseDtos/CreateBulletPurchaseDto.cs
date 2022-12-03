namespace ArmoryManagerApi.DataTransferObjects.BulletPurchaseDtos;

public class CreateBulletPurchaseDto
{
    public string? DatePurchased { get; set; }

    public double? Price { get; set; }

    public long InitialCount { get; set; }

    public long BulletTemplateId { get; set; }
}
