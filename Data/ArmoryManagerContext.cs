using ArmoryManagerApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ArmoryManagerApi.Data;

public partial class ArmoryManagerContext : DbContext
{
    public ArmoryManagerContext()
    {
    }

    public ArmoryManagerContext(DbContextOptions<ArmoryManagerContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BulletPurchase> BulletPurchases { get; set; }

    public virtual DbSet<BulletTemplate> BulletTemplates { get; set; }

    public virtual DbSet<PowderTemplate> PowderTemplates { get; set; }

    public virtual DbSet<PrimerTemplate> PrimerTemplates { get; set; }

    public virtual DbSet<Reload> Reloads { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlite("DataSource=armoryManager.db;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BulletPurchase>(entity =>
        {
            entity.ToTable("BulletPurchase");

            entity.HasIndex(e => e.Id, "IX_BulletPurchase_Id").IsUnique();

           // entity.HasOne(d => d.BulletTemplate).WithMany(p => p.BulletPurchases).HasForeignKey(d => d.BulletTemplateId);

            entity.HasOne(d => d.User).WithMany(p => p.BulletPurchases).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<BulletTemplate>(entity =>
        {
            entity.ToTable("BulletTemplate");

            entity.HasIndex(e => e.Id, "IX_BulletTemplate_Id").IsUnique();

            entity.HasOne(d => d.User).WithMany(p => p.BulletTemplates).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<PowderTemplate>(entity =>
        {
            entity.ToTable("PowderTemplate");

            entity.HasIndex(e => e.Id, "IX_PowderTemplate_Id").IsUnique();
        });

        modelBuilder.Entity<PrimerTemplate>(entity =>
        {
            entity.ToTable("PrimerTemplate");

            entity.HasIndex(e => e.Id, "IX_PrimerTemplate_Id").IsUnique();
        });

        modelBuilder.Entity<Reload>(entity =>
        {
            entity.ToTable("Reload");

            entity.HasIndex(e => e.Id, "IX_Reload_Id").IsUnique();

            entity.HasOne(d => d.User).WithMany(p => p.Reloads).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");

            entity.HasIndex(e => e.Id, "IX_User_Id").IsUnique();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
