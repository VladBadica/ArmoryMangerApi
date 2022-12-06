using System;
using System.Collections.Generic;

namespace ArmoryManagerApi.Models;

public partial class User
{
    public long Id { get; set; }

    public string UserName { get; set; } = null!;

    public byte[] Password { get; set; } = null!;

    public byte[] Salt { get; set; } = null!;

    public string CreatedAt { get; set; } = null!;

    public string UpdatedAt { get; set; } = null!;

    public virtual ICollection<BulletPurchase> BulletPurchases { get; } = new List<BulletPurchase>();

    public virtual ICollection<BulletTemplate> BulletTemplates { get; } = new List<BulletTemplate>();

    public virtual ICollection<PowderPurchase> PowderPurchases { get; } = new List<PowderPurchase>();

    public virtual ICollection<PowderTemplate> PowderTemplates { get; } = new List<PowderTemplate>();

    public virtual ICollection<PrimerPurchase> PrimerPurchases { get; } = new List<PrimerPurchase>();

    public virtual ICollection<PrimerTemplate> PrimerTemplates { get; } = new List<PrimerTemplate>();

    public virtual ICollection<Reload> Reloads { get; } = new List<Reload>();
}
