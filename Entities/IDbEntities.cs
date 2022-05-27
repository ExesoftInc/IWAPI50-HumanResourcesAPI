using Microsoft.EntityFrameworkCore;
using System;

namespace HumanResourcesAPI.Entities {
    
    
    public interface IDbEntities : IDbEntityBase {
        
        DbSet<Department> Departments {
            get;
            set;
        }
        
        DbSet<Employee> Employees {
            get;
            set;
        }
        
        DbSet<EmployeeDepartmentHistory> EmployeeDepartmentHistories {
            get;
            set;
        }
        
        DbSet<EmployeePayHistory> EmployeePayHistories {
            get;
            set;
        }
        
        DbSet<JobCandidate> JobCandidates {
            get;
            set;
        }
        
        DbSet<Shift> Shifts {
            get;
            set;
        }
    }
}

