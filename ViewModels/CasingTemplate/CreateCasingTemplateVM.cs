namespace ArmoryManagerApi.ViewModels;

public record CreateCasingTemplateVM
{
    public string? Make { get; set; }

    public string? Calibre { get; set; }

    public string? Model { get; set; }

    public long Grain { get; set; }
}