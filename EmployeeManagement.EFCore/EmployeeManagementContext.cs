using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using EmployeeManagement.Entities;

namespace EmployeeManagement.EFCore.Models
{
    public partial class EmployeeManagementContext : DbContext
    {
        public EmployeeManagementContext()
        {
        }

        public EmployeeManagementContext(DbContextOptions<EmployeeManagementContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Department> Departments { get; set; } = null!;
        public virtual DbSet<Employee> Employees { get; set; } = null!;
        public virtual DbSet<GradeMaster> GradeMasters { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data source=.;Initial catalog=EmployeeMgmtSystem;Integrated Security=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>(entity =>
            {
                entity.HasKey(e => e.DeptId);

                entity.ToTable("Department");

                entity.Property(e => e.DeptId)
                    .ValueGeneratedNever()
                    .HasColumnName("Dept_ID");

                entity.Property(e => e.DeptName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Dept_Name");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.EmpId);

                entity.ToTable("Employee");

                entity.Property(e => e.EmpId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Emp_ID");

                entity.Property(e => e.EmpBasicSalary).HasColumnName("Emp_Basic_Salary");

                entity.Property(e => e.EmpContactNum)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("Emp_Contact_Num");

                entity.Property(e => e.EmpDateOfBirth)
                    .HasColumnType("date")
                    .HasColumnName("Emp_Date_Of_Birth");

                entity.Property(e => e.EmpDateOfJoining)
                    .HasColumnType("date")
                    .HasColumnName("Emp_Date_Of_joining");

                entity.Property(e => e.EmpDeptId).HasColumnName("Emp_Dept_ID");

                entity.Property(e => e.EmpDesignation)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Emp_Designation");

                entity.Property(e => e.EmpFirstName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Emp_First_Name");

                entity.Property(e => e.EmpGender)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("Emp_Gender");

                entity.Property(e => e.EmpGradeCode)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("Emp_Grade_Code");

                entity.Property(e => e.EmpHomeAddress)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Emp_Home_Address");

                entity.Property(e => e.EmpLastName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Emp_Last_Name");

                entity.Property(e => e.EmpMartialStatus)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("Emp_Martial_status");

                entity.HasOne(d => d.EmpDept)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.EmpDeptId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Employee_Department");

                entity.HasOne(d => d.EmpGradeCodeNavigation)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.EmpGradeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Employee_Grade_Master");
            });

            modelBuilder.Entity<GradeMaster>(entity =>
            {
                entity.HasKey(e => e.GradeCode);

                entity.ToTable("Grade_Master");

                entity.Property(e => e.GradeCode)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("Grade_Code");

                entity.Property(e => e.Designation)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.MaxSalary).HasColumnName("Max_Salary");

                entity.Property(e => e.MinSalary).HasColumnName("Min_Salary");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
