using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HumanResourcesAPI.Entities {
    
    
    public class ShiftConfiguration : IEntityTypeConfiguration<Shift> {
        
        private string _schema = "HumanResources";
        
        public virtual void Configure(EntityTypeBuilder<Shift> builder) {
            Configure(builder, _schema);
        }
        
        private void Configure(EntityTypeBuilder<Shift> builder, string schema) {
            builder.ToTable("Shift", schema);
            builder.HasKey(x => x.ShiftID);

            builder.Property(x => x.ShiftID).HasColumnName(@"ShiftID").HasColumnType("tinyint").IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.Name).HasColumnName(@"Name").HasColumnType("nvarchar").IsRequired().HasMaxLength(50);
            builder.Property(x => x.StartTime).HasColumnName(@"StartTime").HasColumnType("time").IsRequired();
            builder.Property(x => x.EndTime).HasColumnName(@"EndTime").HasColumnType("time").IsRequired();
            builder.Property(x => x.ModifiedDate).HasColumnName(@"ModifiedDate").HasColumnType("datetime").IsRequired();

            //Foreign keys
        }
    }
}

