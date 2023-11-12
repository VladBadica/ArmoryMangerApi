namespace ArmoryManagerApi.ViewModels;

public record PowderTemplateVM
{
    public long Id { get; set; }

    public string? Make { get; set; }

    public string? Model { get; set; }
}