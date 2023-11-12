namespace ArmoryManagerApi.ViewModels;

public record CreateCasingVM
{
    public string? Make { get; set; }

    public string Calibre { get; set; } = null!;

    public string? Model { get; set; }

    public long Grain { get; set; }

    public string? DatePurchased { get; set; }

    public double? Price { get; set; }

    public long InitialCount { get; set; }
}
