﻿using ArmoryManagerApi.Models;

namespace ArmoryManagerApi.DataTransferObjects.PrimerDtos;

public class PrimerDto
{
    public long Id { get; set; }

    public long Size { get; set; }

    public string? Make { get; set; }

    public string? Model { get; set; }

    public string? DatePurchased { get; set; }

    public long? Price { get; set; }

    public long InitialCount { get; set; }

    public long Remaining { get; set; }

    public string CreatedAt { get; set; } = null!;

    public string UpdatedAt { get; set; } = null!;

}
