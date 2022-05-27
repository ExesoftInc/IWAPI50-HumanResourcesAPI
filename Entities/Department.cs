using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HumanResourcesAPI.Entities {
    
    
    public partial class Department {
        
        [Key()]
        [Display(Name = "Department ID")]
        public short DepartmentID { get; set; }
        
        [Display(Name = "Name")]
        public string Name { get; set; }
        
        [Display(Name = "Group name")]
        public string GroupName { get; set; }
        
        [Display(Name = "Modified date")]
        public System.DateTime ModifiedDate { get; set; }
        
        /// Child EmployeeDepartmentHistories where [EmployeeDepartmentHistory].[DepartmentID] point to this entity (FK_EmployeeDepartmentHistory_Department_DepartmentID)
        public virtual ICollection<EmployeeDepartmentHistory> EmployeeDepartmentHistories { get; set; } = new HashSet<EmployeeDepartmentHistory>();
    }
}

