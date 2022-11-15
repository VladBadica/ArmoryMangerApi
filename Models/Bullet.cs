using System;
using System.Collections.Generic;

namespace ArmoryManagerApi.Models;

public partial class Bullet
{
    public long Id { get; set; }

    public string? Make { get; set; }

    public string Calibre { get; set; } = null!;

    public string? Model { get; set; }

    public long Grain { get; set; }
}
