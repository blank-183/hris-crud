using System;
using System.Collections.Generic;
using HRIS.Models;
using Microsoft.EntityFrameworkCore;

namespace HRIS.Data;

public partial class HrissContext : DbContext
{
    public HrissContext()
    {
    }

    public HrissContext(DbContextOptions<HrissContext> options)
        : base(options)
    {
    }

    public virtual DbSet<EducationalBackground> EducationalBackgrounds { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<GovernmentAccount> GovernmentAccounts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EducationalBackground>(entity =>
        {
            entity.HasKey(e => e.EducationalBackgroundId).HasName("PK__Educatio__55478D97C09CBCC3");

            entity.HasOne(d => d.Employee).WithMany(p => p.EducationalBackgrounds)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Education__Emplo__4D94879B");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("PK__Employee__7AD04F112BB9467A");

            entity.Property(e => e.Sex).IsFixedLength();
        });

        modelBuilder.Entity<GovernmentAccount>(entity =>
        {
            entity.HasKey(e => e.GovernmentAccountId).HasName("PK__Governme__90BD49DC8D88E78B");

            entity.Property(e => e.GsisNo).IsFixedLength();
            entity.Property(e => e.PagibigNo).IsFixedLength();
            entity.Property(e => e.PhilhealthNo).IsFixedLength();
            entity.Property(e => e.SssNo).IsFixedLength();
            entity.Property(e => e.Tin).IsFixedLength();

            entity.HasOne(d => d.Employee).WithOne(p => p.GovernmentAccount)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Governmen__Emplo__4AB81AF0");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
