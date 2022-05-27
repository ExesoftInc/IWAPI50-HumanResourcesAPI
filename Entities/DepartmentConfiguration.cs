using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HumanResourcesAPI.Entities {
    
    
    public class DepartmentConfiguration : IEntityTypeConfiguration<Department> {
        
        private string _schema = "HumanResources";
        
        public virtual void Configure(EntityTypeBuilder<Department> builder) {
            Configure(builder, _schema);
        }
        
        private void Configure(EntityTypeBuilder<Department> builder, string schema) {
            builder.ToTable("Department", schema);
            builder.HasKey(x => x.DepartmentID);

            builder.Property(x => x.DepartmentID).HasColumnName(@"DepartmentID").HasColumnType("smallint").IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.Name).HasColumnName(@"Name").HasColumnType("nvarchar").IsRequired().HasMaxLength(50);
            builder.Property(x => x.GroupName).HasColumnName(@"GroupName").HasColumnType("nvarchar").IsRequired().HasMaxLength(50);
            builder.Property(x => x.ModifiedDate).HasColumnName(@"ModifiedDate").HasColumnType("datetime").IsRequired();

            //Foreign keys
        }
    }
}

