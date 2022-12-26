using System;
using System.Collections.Generic;

namespace ArmoryManagerApi.Models;

public partial class Reload
{
    public long Id { get; set; }

    public long CasingId { get; set; }

    public long PrimerId { get; set; }

    public long PowderId { get; set; }

    public long UserId { get; set; }

    public string CreatedAt { get; set; } = null!;

    public string UpdatedAt { get; set; } = null!;

    public long CasingCount { get; set; }

    public long PowderCount { get; set; }

    public long PrimerCount { get; set; }

    public virtual Casing Casing { get; set; } = null!;

    public virtual Powder Powder { get; set; } = null!;

    public virtual Primer Primer { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
