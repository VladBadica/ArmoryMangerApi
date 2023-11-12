namespace ArmoryManagerApi.ViewModels;

public record CreatePowderVM
{
    public string Make { get; set; } = null!;

    public string? Model { get; set; }

    public string? DatePurchased { get; set; }

    public long? Price { get; set; }

    public long InitialCount { get; set; }
}
