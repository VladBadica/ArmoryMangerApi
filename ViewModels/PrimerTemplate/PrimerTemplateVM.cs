namespace ArmoryManagerApi.ViewModels;

public record PrimerTemplateVM
{
    public long Id { get; set; }

    public long Size { get; set; }

    public string? Make { get; set; }

    public string? Model { get; set; }
}