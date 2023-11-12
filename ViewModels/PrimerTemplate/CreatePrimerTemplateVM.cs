namespace ArmoryManagerApi.ViewModels;

public record CreatePrimerTemplateVM
{
    public long Size { get; set; }

    public string? Make { get; set; }

    public string? Model { get; set; }
}