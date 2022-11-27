using System;
using System.Collections.Generic;

namespace ArmoryManagerApi.Models;

public partial class Reload
{
    public long Id { get; set; }

    public long BulletPurchaseId { get; set; }

    public long? UserId { get; set; }

    public virtual User? User { get; set; }
}
