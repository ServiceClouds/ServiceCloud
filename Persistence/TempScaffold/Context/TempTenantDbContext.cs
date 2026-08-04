using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Persistence.TempScaffold.Models;

namespace Persistence.TempScaffold.Context;

public partial class TempTenantDbContext : DbContext
{
    public TempTenantDbContext(DbContextOptions<TempTenantDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Service> Services { get; set; }

    public virtual DbSet<ServiceCategory> ServiceCategories { get; set; }

    public virtual DbSet<ServiceCategoryBranch> ServiceCategoryBranches { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Service>(entity =>
        {
            entity.HasOne(d => d.ServiceCategory).WithMany(p => p.Services)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Service_ServiceCategory");
        });

        modelBuilder.Entity<ServiceCategoryBranch>(entity =>
        {
            entity.HasOne(d => d.ServiceCategory).WithMany(p => p.ServiceCategoryBranches)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ServiceCategoryBranch_ServiceCategory");
        });
        
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
