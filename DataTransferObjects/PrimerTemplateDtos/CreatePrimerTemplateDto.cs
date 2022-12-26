namespace ArmoryManagerApi.DataTransferObjects.PrimerTemplateDtos;

public class CreatePrimerTemplateDto
{
    public long Size { get; set; }

    public string? Make { get; set; }

    public string? Model { get; set; }
}