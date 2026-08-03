//using System;
//using System.Collections.Generic;
//using Microsoft.EntityFrameworkCore;
//using Persistence.TempScaffold.Entities;

//namespace Persistence.TempScaffold.Context;

//public partial class AppDbContext : DbContext
//{
//    public AppDbContext()
//    {
//    }

//    public AppDbContext(DbContextOptions<AppDbContext> options)
//        : base(options)
//    {
//    }

//    public virtual DbSet<Branch> Branches { get; set; }

//    public virtual DbSet<Company> Companies { get; set; }

//    public virtual DbSet<Country> Countries { get; set; }

//    public virtual DbSet<Currency> Currencies { get; set; }

//    public virtual DbSet<Role> Roles { get; set; }

//    public virtual DbSet<Staff> Staff { get; set; }

//    public virtual DbSet<StaffBranch> StaffBranches { get; set; }

//    public virtual DbSet<StaffPosition> StaffPositions { get; set; }

//    public virtual DbSet<StateCountry> StateCountries { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=ServiceCloudTenant;Trusted_Connection=True;TrustServerCertificate=True;");

//    protected override void OnModelCreating(ModelBuilder modelBuilder)
//    {
//        modelBuilder.Entity<Branch>(entity =>
//        {
//            entity.HasOne(d => d.Company).WithMany(p => p.Branches)
//                .OnDelete(DeleteBehavior.ClientSetNull)
//                .HasConstraintName("FK_Branch_Company");

//            entity.HasOne(d => d.Country).WithMany(p => p.Branches)
//                .OnDelete(DeleteBehavior.ClientSetNull)
//                .HasConstraintName("FK_Branch_Country");
//        });

//        modelBuilder.Entity<Company>(entity =>
//        {
//            entity.HasOne(d => d.Country).WithMany(p => p.Companies)
//                .OnDelete(DeleteBehavior.ClientSetNull)
//                .HasConstraintName("FK_Company_Country");

//            entity.HasOne(d => d.Currency).WithMany(p => p.Companies)
//                .OnDelete(DeleteBehavior.ClientSetNull)
//                .HasConstraintName("FK_Company_Currency");
//        });

//        modelBuilder.Entity<Staff>(entity =>
//        {
//            entity.Property(e => e.FullName).HasComputedColumnSql("(Trim(([FirstName]+' ')+[LastName]))", true);

//            entity.HasOne(d => d.Company).WithMany(p => p.Staff)
//                .OnDelete(DeleteBehavior.ClientSetNull)
//                .HasConstraintName("FK_Staff_Company");

//            entity.HasOne(d => d.Country).WithMany(p => p.Staff).HasConstraintName("FK_Staff_Country");

//            entity.HasOne(d => d.StaffPosition).WithMany(p => p.Staff)
//                .OnDelete(DeleteBehavior.ClientSetNull)
//                .HasConstraintName("FK_Staff_StaffPosition");

//            entity.HasOne(d => d.StateCountry).WithMany(p => p.Staff).HasConstraintName("FK_Staff_StateCountry");
//        });

//        modelBuilder.Entity<StaffBranch>(entity =>
//        {
//            entity.HasOne(d => d.Branch).WithMany(p => p.StaffBranches)
//                .OnDelete(DeleteBehavior.ClientSetNull)
//                .HasConstraintName("FK_StaffBranch_Branch");

//            entity.HasOne(d => d.Role).WithMany(p => p.StaffBranches).HasConstraintName("FK_StaffBranch_Role");

//            entity.HasOne(d => d.Staff).WithMany(p => p.StaffBranches)
//                .OnDelete(DeleteBehavior.ClientSetNull)
//                .HasConstraintName("FK_StaffBranch_Staff");
//        });

//        modelBuilder.Entity<StateCountry>(entity =>
//        {
//            entity.HasOne(d => d.Country).WithMany(p => p.StateCountries)
//                .OnDelete(DeleteBehavior.ClientSetNull)
//                .HasConstraintName("FK_StateCountry_Country");
//        });

//        OnModelCreatingPartial(modelBuilder);
//    }

//    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
//}
