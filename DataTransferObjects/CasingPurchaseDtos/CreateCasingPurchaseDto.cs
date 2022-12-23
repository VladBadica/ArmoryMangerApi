namespace ArmoryManagerApi.DataTransferObjects.CasingPurchaseDtos;

public class CreateCasingPurchaseDto
{
    public long? Make { get; set; }

    public string Calibre { get; set; } = null!;

    public string? Model { get; set; }

    public long Grain { get; set; }

    public string? DatePurchased { get; set; }

    public double? Price { get; set; }

    public long InitialCount { get; set; }
}
