using System;
using System.Collections.Generic;

namespace ArmoryManagerApi.Models;

public partial class User
{
    public long Id { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public virtual ICollection<BulletPurchase> BulletPurchases { get; } = new List<BulletPurchase>();

    public virtual ICollection<BulletTemplate> BulletTemplates { get; } = new List<BulletTemplate>();

    public virtual ICollection<Reload> Reloads { get; } = new List<Reload>();
}
