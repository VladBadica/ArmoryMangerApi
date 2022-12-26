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

    public virtual ICollection<CasingTemplate> CasingTemplates { get; } = new List<CasingTemplate>();

    public virtual ICollection<Casing> Casings { get; } = new List<Casing>();

    public virtual ICollection<PowderTemplate> PowderTemplates { get; } = new List<PowderTemplate>();

    public virtual ICollection<Powder> Powders { get; } = new List<Powder>();

    public virtual ICollection<PrimerTemplate> PrimerTemplates { get; } = new List<PrimerTemplate>();

    public virtual ICollection<Primer> Primers { get; } = new List<Primer>();

    public virtual ICollection<Reload> Reloads { get; } = new List<Reload>();
}
