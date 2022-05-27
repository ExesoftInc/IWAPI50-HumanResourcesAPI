using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HumanResourcesAPI.Entities {
    
    
    public class EmployeeDepartmentHistoryConfiguration : IEntityTypeConfiguration<EmployeeDepartmentHistory> {
        
        private string _schema = "HumanResources";
        
        public virtual void Configure(EntityTypeBuilder<EmployeeDepartmentHistory> builder) {
            Configure(builder, _schema);
        }
        
        private void Configure(EntityTypeBuilder<EmployeeDepartmentHistory> builder, string schema) {
            builder.ToTable("EmployeeDepartmentHistory", schema);
            builder.HasKey(x => new { x.BusinessEntityID, x.StartDate, x.DepartmentID, x.ShiftID });

            builder.Property(x => x.BusinessEntityID).HasColumnName(@"BusinessEntityID").HasColumnType("int").IsRequired();
            builder.Property(x => x.DepartmentID).HasColumnName(@"DepartmentID").HasColumnType("smallint").IsRequired();
            builder.Property(x => x.ShiftID).HasColumnName(@"ShiftID").HasColumnType("tinyint").IsRequired();
            builder.Property(x => x.StartDate).HasColumnName(@"StartDate").HasColumnType("date").IsRequired();
            builder.Property(x => x.EndDate).HasColumnName(@"EndDate").HasColumnType("date").IsRequired(false);
            builder.Property(x => x.ModifiedDate).HasColumnName(@"ModifiedDate").HasColumnType("datetime").IsRequired();

            //Foreign keys
            builder.HasOne(a => a.Employee).WithMany(b => b.EmployeeDepartmentHistories).HasForeignKey(c => c.BusinessEntityID).OnDelete(DeleteBehavior.Restrict); // FK_EmployeeDepartmentHistory_Employee_BusinessEntityID
            builder.HasOne(a => a.Department).WithMany(b => b.EmployeeDepartmentHistories).HasForeignKey(c => c.DepartmentID).OnDelete(DeleteBehavior.Restrict); // FK_EmployeeDepartmentHistory_Department_DepartmentID
            builder.HasOne(a => a.Shift).WithMany(b => b.EmployeeDepartmentHistories).HasForeignKey(c => c.ShiftID).OnDelete(DeleteBehavior.Restrict); // FK_EmployeeDepartmentHistory_Shift_ShiftID
        }
    }
}

