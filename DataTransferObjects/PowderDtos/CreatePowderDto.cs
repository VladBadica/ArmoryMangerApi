namespace ArmoryManagerApi.DataTransferObjects.PowderDtos;

public class CreatePowderDto
{
    public string Make { get; set; } = null!;

    public string? Model { get; set; }

    public string? DatePurchased { get; set; }

    public long? Price { get; set; }

    public long InitialCount { get; set; }
}
