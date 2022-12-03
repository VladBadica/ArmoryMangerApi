namespace ArmoryManagerApi.DataTransferObjects.PowderPurchaseDtos;

public class CreatePowderPurchaseDto
{
    public string? DatePurchased { get; set; }

    public long? Price { get; set; }

    public long InitialCount { get; set; }

    public long PowderTemplateId { get; set; }
}
