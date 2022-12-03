namespace ArmoryManagerApi.DataTransferObjects.PrimerPurchaseDtos;

public class CreatePrimerPurchaseDto
{
    public string? DatePurchased { get; set; }

    public long? Price { get; set; }

    public long InitialCount { get; set; }

    public long PrimerTemplateId { get; set; }
}
