using System.ComponentModel.DataAnnotations;

namespace ArmoryManagerApi.DataTransferObjects.CasingTemplateDtos;

public class CreateCasingTemplateDto
{
    public string? Make { get; set; }

    public string? Calibre { get; set; }

    public string? Model { get; set; }

    public long Grain { get; set; }
}