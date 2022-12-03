using System.ComponentModel.DataAnnotations;

namespace ArmoryManagerApi.DataTransferObjects.BulletTemplateDtos;

public class CreateBulletTemplateDto
{
    public string? Make { get; set; }

    [Required]
    public string Calibre { get; set; } = null!;

    public string? Model { get; set; }

    [Required]
    public long Grain { get; set; }
}