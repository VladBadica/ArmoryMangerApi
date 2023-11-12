namespace ArmoryManagerApi.ViewModels;

public record CreatePrimerVM
{ 
    public long Size { get; set; }

    public string? Make { get; set; }

    public string? Model { get; set; }

    public string? DatePurchased { get; set; }

    public long? Price { get; set; }

    public long InitialCount { get; set; }
}
