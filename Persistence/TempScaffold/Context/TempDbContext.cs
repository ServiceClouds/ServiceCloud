using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace Persistence.TempScaffold.Context;

public partial class TempDbContext : DbContext
{
    public TempDbContext()
    {
    }

    public TempDbContext(DbContextOptions<TempDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<StaffToken> StaffTokens { get; set; }

//    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//    => optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=ServiceCloud;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<StaffToken>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("StaffToken");

            entity.Property(e => e.AccessTokenExpiry).HasColumnType("datetime");
            entity.Property(e => e.AppSourceTypeId).HasColumnName("AppSourceTypeID");
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.StaffLoginId).HasColumnName("StaffLoginID");
            entity.Property(e => e.StaffTokenId)
                .ValueGeneratedOnAdd()
                .HasColumnName("StaffTokenID");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
