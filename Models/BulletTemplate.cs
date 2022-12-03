
namespace ArmoryManagerApi.Models;

public partial class BulletTemplate
{
    public long Id { get; set; }

    public string? Make { get; set; }

    public string Calibre { get; set; } = null!;

    public string? Model { get; set; }

    public long Grain { get; set; }

    public long UserId { get; set; }

    public virtual ICollection<BulletPurchase> BulletPurchases { get; } = new List<BulletPurchase>();

    public virtual User User { get; set; } = null!;
}
