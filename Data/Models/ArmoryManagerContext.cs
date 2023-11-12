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

    public virtual DbSet<Casing> Casings { get; set; }

    public virtual DbSet<CasingTemplate> CasingTemplates { get; set; }

    public virtual DbSet<Powder> Powders { get; set; }

    public virtual DbSet<PowderTemplate> PowderTemplates { get; set; }

    public virtual DbSet<Primer> Primers { get; set; }

    public virtual DbSet<PrimerTemplate> PrimerTemplates { get; set; }

    public virtual DbSet<Reload> Reloads { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlite("DataSource=armoryManager.db;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Casing>(entity =>
        {
            entity.ToTable("Casing");

            entity.HasIndex(e => e.Id, "IX_Casing_Id").IsUnique();

            entity.HasOne(d => d.User).WithMany(p => p.Casings)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<CasingTemplate>(entity =>
        {
            entity.ToTable("CasingTemplate");

            entity.HasIndex(e => e.Id, "IX_CasingTemplate_Id").IsUnique();

            entity.HasOne(d => d.User).WithMany(p => p.CasingTemplates)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Powder>(entity =>
        {
            entity.ToTable("Powder");

            entity.Property(e => e.Make).HasDefaultValueSql("0");

            entity.HasOne(d => d.User).WithMany(p => p.Powders)
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

        modelBuilder.Entity<Primer>(entity =>
        {
            entity.ToTable("Primer");

            entity.HasOne(d => d.User).WithMany(p => p.Primers)
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

            entity.HasOne(d => d.Casing).WithMany(p => p.Reloads)
                .HasForeignKey(d => d.CasingId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Powder).WithMany(p => p.Reloads)
                .HasForeignKey(d => d.PowderId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Primer).WithMany(p => p.Reloads)
                .HasForeignKey(d => d.PrimerId)
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
