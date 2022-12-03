using System;
using System.Collections.Generic;
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

    public virtual DbSet<BulletPurchase> BulletPurchases { get; set; }

    public virtual DbSet<BulletTemplate> BulletTemplates { get; set; }

    public virtual DbSet<PowderPurchase> PowderPurchases { get; set; }

    public virtual DbSet<PowderTemplate> PowderTemplates { get; set; }

    public virtual DbSet<PrimerPurchase> PrimerPurchases { get; set; }

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

            entity.HasOne(d => d.BulletTemplate).WithMany(p => p.BulletPurchases)
                .HasForeignKey(d => d.BulletTemplateId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.User).WithMany(p => p.BulletPurchases)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<BulletTemplate>(entity =>
        {
            entity.ToTable("BulletTemplate");

            entity.HasIndex(e => e.Id, "IX_BulletTemplate_Id").IsUnique();

            entity.HasOne(d => d.User).WithMany(p => p.BulletTemplates)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<PowderPurchase>(entity =>
        {
            entity.ToTable("PowderPurchase");

            entity.HasOne(d => d.PowderTemplate).WithMany(p => p.PowderPurchases)
                .HasForeignKey(d => d.PowderTemplateId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.User).WithMany(p => p.PowderPurchases)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<PowderTemplate>(entity =>
        {
            entity.ToTable("PowderTemplate");

            entity.HasIndex(e => e.Id, "IX_PowderTemplate_Id").IsUnique();

            entity.HasOne(d => d.User).WithMany(p => p.PowderTemplates)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<PrimerPurchase>(entity =>
        {
            entity.ToTable("PrimerPurchase");

            entity.HasOne(d => d.PrimerTemplate).WithMany(p => p.PrimerPurchases)
                .HasForeignKey(d => d.PrimerTemplateId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.User).WithMany(p => p.PrimerPurchases)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<PrimerTemplate>(entity =>
        {
            entity.ToTable("PrimerTemplate");

            entity.HasIndex(e => e.Id, "IX_PrimerTemplate_Id").IsUnique();

            entity.HasOne(d => d.User).WithMany(p => p.PrimerTemplates)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Reload>(entity =>
        {
            entity.ToTable("Reload");

            entity.HasIndex(e => e.Id, "IX_Reload_Id").IsUnique();

            entity.HasOne(d => d.BulletPurchase).WithMany(p => p.Reloads)
                .HasForeignKey(d => d.BulletPurchaseId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.PowderPurchase).WithMany(p => p.Reloads)
                .HasForeignKey(d => d.PowderPurchaseId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.PrimerPurchase).WithMany(p => p.Reloads)
                .HasForeignKey(d => d.PrimerPurchaseId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.User).WithMany(p => p.Reloads)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull);
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
