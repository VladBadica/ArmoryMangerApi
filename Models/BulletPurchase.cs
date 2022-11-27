using System;
using System.Collections.Generic;

namespace ArmoryManagerApi.Models;

public partial class BulletPurchase
{
    public long Id { get; set; }

    public string? DatePurchased { get; set; }

    public double? Price { get; set; }

    public long InitialCount { get; set; }

    public long Remaining { get; set; }

    public long? UserId { get; set; }

    public long? BulletTemplateId { get; set; }

    public virtual BulletTemplate? BulletTemplate { get; set; }

    public virtual User? User { get; set; }
}
