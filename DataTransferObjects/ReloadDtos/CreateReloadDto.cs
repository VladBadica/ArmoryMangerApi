﻿namespace ArmoryManagerApi.DataTransferObjects.ReloadDtos;

public class CreateReloadDto
{
    public long CasingId { get; set; }

    public long CasingCount { get; set; }

    public long PowderId { get; set; }

    public long PowderCount { get; set; }

    public long PrimerId { get; set; }

    public long PrimerCount { get; set; }
}
