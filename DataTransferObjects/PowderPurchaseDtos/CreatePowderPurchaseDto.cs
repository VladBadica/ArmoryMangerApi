namespace ArmoryManagerApi.DataTransferObjects.PowderPurchaseDtos;

public class CreatePowderPurchaseDto
{
    public string Make { get; set; } = null!;

    public string? Model { get; set; }

    public string? DatePurchased { get; set; }

    public long? Price { get; set; }

    public long InitialCount { get; set; }
}
