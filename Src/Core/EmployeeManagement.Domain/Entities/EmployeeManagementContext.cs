using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Domain.Entities;

public partial class EmployeeManagementContext : DbContext
{
    public EmployeeManagementContext()
    {
    }

    public EmployeeManagementContext(DbContextOptions<EmployeeManagementContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<EmployeeType> EmployeeTypes { get; set; }

    public virtual DbSet<MonthlyAttendanceSummary> MonthlyAttendanceSummaries { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    public virtual DbSet<WorkLog> WorkLogs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=LAPTOP-4V0QV0VF\\SQLEXPRESS;Initial Catalog=EmployeeManagement;Integrated Security=True;MultipleActiveResultSets=True;Encrypt=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmpId).HasName("PK__Employee__AF2DBB9917677E5B");

            entity.ToTable("Employee");

            entity.HasIndex(e => e.EmailId, "UQ__Employee__A9D10534CB726610").IsUnique();

            entity.Property(e => e.Client).HasMaxLength(50);
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.EmailId).HasMaxLength(256);
            entity.Property(e => e.EmployeeType)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.Gender)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.LastName).HasMaxLength(150);
            entity.Property(e => e.LastUpdatedBy).HasMaxLength(50);
            entity.Property(e => e.Password).HasMaxLength(32);
            entity.Property(e => e.Reference).HasMaxLength(50);
            entity.Property(e => e.Status).HasDefaultValue(true, "DF__Employee__IsActi__5070F446");
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<EmployeeType>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("EmployeeType");

            entity.Property(e => e.EmployeeType1)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("EmployeeType");
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<MonthlyAttendanceSummary>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("MonthlyAttendanceSummary");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CC4C9C5CE200");

            entity.HasIndex(e => e.Email, "UQ__Users__A9D105347343AF65").IsUnique();

            entity.HasIndex(e => e.EmpId, "UQ__Users__AF2DBB9835701CE2").IsUnique();

            entity.Property(e => e.DisplayName).HasMaxLength(150);
            entity.Property(e => e.Email)
                .HasMaxLength(256)
                .HasDefaultValue("unique", "DF_Users_Email");
            entity.Property(e => e.IsActive).HasDefaultValue(true, "DF__Users__IsActive__367C1819");

            entity.HasOne(d => d.UserType).WithMany(p => p.Users)
                .HasForeignKey(d => d.UserTypeId)
                .HasConstraintName("FK_userType");
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.ToTable("UserRole");

            entity.Property(e => e.UserType)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<WorkLog>(entity =>
        {
            entity.HasKey(e => e.WorkLogId).HasName("PK__WorkLogs__FE542C22127C3B69");

            entity.HasIndex(e => new { e.UserId, e.WorkDate }, "UQ_WorkLogs_UserDate").IsUnique();

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysutcdatetime())", "DF__WorkLogs__Create__7E02B4CC");
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.HoursWorked).HasColumnType("decimal(5, 2)");

            entity.HasOne(d => d.User).WithMany(p => p.WorkLogs)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_WorkLogs_Users");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
