using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HumanResourcesAPI.Entities {
    
    
    public class JobCandidateConfiguration : IEntityTypeConfiguration<JobCandidate> {
        
        private string _schema = "HumanResources";
        
        public virtual void Configure(EntityTypeBuilder<JobCandidate> builder) {
            Configure(builder, _schema);
        }
        
        private void Configure(EntityTypeBuilder<JobCandidate> builder, string schema) {
            builder.ToTable("JobCandidate", schema);
            builder.HasKey(x => x.JobCandidateID);

            builder.Property(x => x.JobCandidateID).HasColumnName(@"JobCandidateID").HasColumnType("int").IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.BusinessEntityID).HasColumnName(@"BusinessEntityID").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.Resume).HasColumnName(@"Resume").HasColumnType("xml").IsRequired(false);
            builder.Property(x => x.ModifiedDate).HasColumnName(@"ModifiedDate").HasColumnType("datetime").IsRequired();

            //Foreign keys
            builder.HasOne(a => a.Employee).WithMany(b => b.JobCandidates).HasForeignKey(c => c.BusinessEntityID).OnDelete(DeleteBehavior.Restrict); // FK_JobCandidate_Employee_BusinessEntityID
        }
    }
}

