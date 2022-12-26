using ArmoryManagerApi.DataTransferObjects.CasingDtos;
using ArmoryManagerApi.DataTransferObjects.PowderDtos;
using ArmoryManagerApi.DataTransferObjects.PrimerDtos;

namespace ArmoryManagerApi.DataTransferObjects.ReloadDtos;

public class ReloadDto
{
    public long Id { get; set; }

    public long CasingCount { get; set; }

    public long PowderCount { get; set; }

    public long PrimerCount { get; set; }

    public virtual CasingDto Casing{ get; set; } = null!;

    public virtual PowderDto Powder { get; set; } = null!;

    public virtual PrimerDto Primer{ get; set; } = null!;

    public string CreatedAt { get; set; } = null!;

    public string UpdatedAt { get; set; } = null!;
}
