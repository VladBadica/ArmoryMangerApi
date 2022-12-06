using System.ComponentModel.DataAnnotations;

namespace ArmoryManagerApi.DataTransferObjects.BulletTemplateDtos;

public class BulletTemplateDto
{
    public long Id { get; set; }

    public string? Make { get; set; }

    public string? Calibre { get; set; }

    public string? Model { get; set; }

    public long Grain { get; set; }
}
