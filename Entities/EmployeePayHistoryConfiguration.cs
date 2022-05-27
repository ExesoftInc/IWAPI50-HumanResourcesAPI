using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HumanResourcesAPI.Entities {
    
    
    public class EmployeePayHistoryConfiguration : IEntityTypeConfiguration<EmployeePayHistory> {
        
        private string _schema = "HumanResources";
        
        public virtual void Configure(EntityTypeBuilder<EmployeePayHistory> builder) {
            Configure(builder, _schema);
        }
        
        private void Configure(EntityTypeBuilder<EmployeePayHistory> builder, string schema) {
            builder.ToTable("EmployeePayHistory", schema);
            builder.HasKey(x => new { x.BusinessEntityID, x.RateChangeDate });

            builder.Property(x => x.BusinessEntityID).HasColumnName(@"BusinessEntityID").HasColumnType("int").IsRequired();
            builder.Property(x => x.RateChangeDate).HasColumnName(@"RateChangeDate").HasColumnType("datetime").IsRequired();
            builder.Property(x => x.Rate).HasColumnName(@"Rate").HasColumnType("money").IsRequired().HasColumnType("decimal19,4)");
            builder.Property(x => x.PayFrequency).HasColumnName(@"PayFrequency").HasColumnType("tinyint").IsRequired();
            builder.Property(x => x.ModifiedDate).HasColumnName(@"ModifiedDate").HasColumnType("datetime").IsRequired();

            //Foreign keys
            builder.HasOne(a => a.Employee).WithMany(b => b.EmployeePayHistories).HasForeignKey(c => c.BusinessEntityID).OnDelete(DeleteBehavior.Restrict); // FK_EmployeePayHistory_Employee_BusinessEntityID
        }
    }
}

