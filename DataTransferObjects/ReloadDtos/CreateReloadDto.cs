namespace ArmoryManagerApi.DataTransferObjects.ReloadDtos;

public class CreateReloadDto
{
    public long CasingPurchaseId { get; set; }

    public long CasingCount { get; set; }

    public long PowderPurchaseId { get; set; }

    public long PowderCount { get; set; }

    public long PrimerPurchaseId { get; set; }

    public long PrimerCount { get; set; }
}
