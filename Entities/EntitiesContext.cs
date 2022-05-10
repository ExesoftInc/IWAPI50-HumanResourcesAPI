// ----------------------------------------------------------------------------------
// <copyright company="Exesoft Inc.">
//	This code was generated by Instant Web API code automation software (https://www.InstantWebAPI.com)
//	Copyright Exesoft Inc. © 2019.  All rights reserved.
// </copyright>
// ----------------------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace HumanResourcesAPI.Entities {
    
    
    public class EntitiesContext : DbContext, IDbEntities {
        
        public EntitiesContext() {
            //empty constructor
        }
        
        public EntitiesContext(DbContextOptions<EntitiesContext> options) : 
                base(options) {
        }
        
        public virtual DbSet<Department> Departments { get; set; }
        
        public virtual DbSet<Employee> Employees { get; set; }
        
        public virtual DbSet<EmployeeDepartmentHistory> EmployeeDepartmentHistories { get; set; }
        
        public virtual DbSet<EmployeePayHistory> EmployeePayHistories { get; set; }
        
        public virtual DbSet<JobCandidate> JobCandidates { get; set; }
        
        public virtual DbSet<Shift> Shifts { get; set; }
        
        public virtual async Task<int> SaveChangesAsync() {
           return await SaveChangesAsync(new CancellationToken());
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder) {

            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new DepartmentConfiguration());
            modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
            modelBuilder.ApplyConfiguration(new EmployeeDepartmentHistoryConfiguration());
            modelBuilder.ApplyConfiguration(new EmployeePayHistoryConfiguration());
            modelBuilder.ApplyConfiguration(new JobCandidateConfiguration());
            modelBuilder.ApplyConfiguration(new ShiftConfiguration());
        }
    }
}

