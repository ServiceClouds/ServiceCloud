using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Persistence.Entities;

namespace Persistence.Data;

public partial class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Branch> Branches { get; set; }

    public virtual DbSet<BranchIntegration> BranchIntegrations { get; set; }

    public virtual DbSet<Company> Companies { get; set; }

    public virtual DbSet<Staff> Staff { get; set; }

    public virtual DbSet<StaffBranch> StaffBranches { get; set; }

    public virtual DbSet<StaffLoggedInBranch> StaffLoggedInBranches { get; set; }

    public virtual DbSet<StaffLogin> StaffLogins { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {




        //Automatically implement all IConfiguration<T> Classes
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        base.OnModelCreating(modelBuilder);

        
    

    OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
