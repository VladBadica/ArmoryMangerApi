using System;
using System.Collections.Generic;

namespace ArmoryManagerApi.Models;

public partial class Primer
{
    public long Id { get; set; }

    public string? Size { get; set; }

    public string? Make { get; set; }

    public string? Model { get; set; }
}
