using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HumanResourcesAPI.Entities {
    
    
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee> {
        
        private string _schema = "HumanResources";
        
        public virtual void Configure(EntityTypeBuilder<Employee> builder) {
            Configure(builder, _schema);
        }
        
        private void Configure(EntityTypeBuilder<Employee> builder, string schema) {
            builder.ToTable("Employee", schema);
            builder.HasKey(x => x.BusinessEntityID);

            builder.Property(x => x.BusinessEntityID).HasColumnName(@"BusinessEntityID").HasColumnType("int").IsRequired();
            builder.Property(x => x.NationalIDNumber).HasColumnName(@"NationalIDNumber").HasColumnType("nvarchar").IsRequired().HasMaxLength(15);
            builder.Property(x => x.LoginID).HasColumnName(@"LoginID").HasColumnType("nvarchar").IsRequired().HasMaxLength(256);
            builder.Property(x => x.OrganizationLevel).HasColumnName(@"OrganizationLevel").HasColumnType("smallint").IsRequired(false).ValueGeneratedOnAddOrUpdate();
            builder.Property(x => x.JobTitle).HasColumnName(@"JobTitle").HasColumnType("nvarchar").IsRequired().HasMaxLength(50);
            builder.Property(x => x.BirthDate).HasColumnName(@"BirthDate").HasColumnType("date").IsRequired();
            builder.Property(x => x.MaritalStatus).HasColumnName(@"MaritalStatus").HasColumnType("nchar").IsRequired().IsFixedLength().HasMaxLength(1);
            builder.Property(x => x.Gender).HasColumnName(@"Gender").HasColumnType("nchar").IsRequired().IsFixedLength().HasMaxLength(1);
            builder.Property(x => x.HireDate).HasColumnName(@"HireDate").HasColumnType("date").IsRequired();
            builder.Property(x => x.SalariedFlag).HasColumnName(@"SalariedFlag").HasColumnType("bit").IsRequired();
            builder.Property(x => x.VacationHours).HasColumnName(@"VacationHours").HasColumnType("smallint").IsRequired();
            builder.Property(x => x.SickLeaveHours).HasColumnName(@"SickLeaveHours").HasColumnType("smallint").IsRequired();
            builder.Property(x => x.CurrentFlag).HasColumnName(@"CurrentFlag").HasColumnType("bit").IsRequired();
            builder.Property(x => x.Rowguid).HasColumnName(@"rowguid").HasColumnType("uniqueidentifier").IsRequired().ValueGeneratedOnAddOrUpdate();
            builder.Property(x => x.ModifiedDate).HasColumnName(@"ModifiedDate").HasColumnType("datetime").IsRequired();

            //Foreign keys
        }
    }
}

