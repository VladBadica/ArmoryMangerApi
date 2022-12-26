using System;
using System.Collections.Generic;

namespace ArmoryManagerApi.Models;

public partial class PrimerTemplate
{
    public long Id { get; set; }

    public long? Size { get; set; }

    public string? Make { get; set; }

    public string? Model { get; set; }

    public long UserId { get; set; }

    public virtual User User { get; set; } = null!;
}
