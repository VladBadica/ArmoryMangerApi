using Microsoft.EntityFrameworkCore;

namespace ArmoryManagerApi.Models;

public partial class ArmoryManagerContext : DbContext
{
    public ArmoryManagerContext()
    {
    }

    public ArmoryManagerContext(DbContextOptions<ArmoryManagerContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Bullet> Bullets { get; set; }

    public virtual DbSet<Powder> Powders { get; set; }

    public virtual DbSet<Primer> Primers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlite("Data Source=C:\\Users\\vladb\\source\\repos\\ArmoryManagerApi\\armoryManager.db;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Bullet>(entity =>
        {
            entity.ToTable("Bullet");

            entity.HasIndex(e => e.Id, "IX_Bullet_Id").IsUnique();
        });

        modelBuilder.Entity<Powder>(entity =>
        {
            entity.ToTable("Powder");

            entity.HasIndex(e => e.Id, "IX_Powder_Id").IsUnique();
        });

        modelBuilder.Entity<Primer>(entity =>
        {
            entity.ToTable("Primer");

            entity.HasIndex(e => e.Id, "IX_Primer_Id").IsUnique();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
