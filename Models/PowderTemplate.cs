using System;
using System.Collections.Generic;

namespace ArmoryManagerApi.Models;

public partial class PowderTemplate
{
    public long Id { get; set; }

    public string? Make { get; set; }

    public string? Model { get; set; }

    public long UserId { get; set; }

    public virtual ICollection<PowderPurchase> PowderPurchases { get; } = new List<PowderPurchase>();

    public virtual User User { get; set; } = null!;
}
