namespace ArmoryManagerApi.ViewModels;

public record ReloadVM
{
    public long Id { get; set; }

    public long CasingCount { get; set; }

    public long PowderCount { get; set; }

    public long PrimerCount { get; set; }

    public virtual CasingVM Casing{ get; set; } = null!;

    public virtual PowderVM Powder { get; set; } = null!;

    public virtual PrimerVM Primer{ get; set; } = null!;

    public string CreatedAt { get; set; } = null!;

    public string UpdatedAt { get; set; } = null!;
}
