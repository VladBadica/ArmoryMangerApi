﻿namespace ArmoryManagerApi.DataTransferObjects.PrimerTemplateDtos;

public class PrimerTemplateDto
{
    public long Id { get; set; }

    public long Size { get; set; }

    public string? Make { get; set; }

    public string? Model { get; set; }
}