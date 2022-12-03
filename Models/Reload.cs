using System;
using System.Collections.Generic;

namespace ArmoryManagerApi.Models;

public partial class Reload
{
    public long Id { get; set; }

    public long BulletPurchaseId { get; set; }

    public long PrimerPurchaseId { get; set; }

    public long PowderPurchaseId { get; set; }

    public long UserId { get; set; }

    public string CreatedAt { get; set; } = null!;

    public string UpdatedAt { get; set; } = null!;

    public virtual BulletPurchase BulletPurchase { get; set; } = null!;

    public virtual PowderPurchase PowderPurchase { get; set; } = null!;

    public virtual PrimerPurchase PrimerPurchase { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
