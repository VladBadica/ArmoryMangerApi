namespace ArmoryManagerApi.DataTransferObjects.ReloadDtos;

public class CreateReloadDto
{
    public long BulletPurchaseId { get; set; }

    public long BulletCount { get; set; }

    public long PowderPurchaseId { get; set; }

    public long PowderCount { get; set; }

    public long PrimerPurchaseId { get; set; }

    public long PrimerCount { get; set; }
}
