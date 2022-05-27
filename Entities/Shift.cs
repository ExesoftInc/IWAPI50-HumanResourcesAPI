using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HumanResourcesAPI.Entities {
    
    
    public partial class Shift {
        
        [Key()]
        [Display(Name = "Shift ID")]
        public byte ShiftID { get; set; }
        
        [Display(Name = "Name")]
        public string Name { get; set; }
        
        [Display(Name = "Start time")]
        public System.TimeSpan StartTime { get; set; }
        
        [Display(Name = "End time")]
        public System.TimeSpan EndTime { get; set; }
        
        [Display(Name = "Modified date")]
        public System.DateTime ModifiedDate { get; set; }
        
        /// Child EmployeeDepartmentHistories where [EmployeeDepartmentHistory].[ShiftID] point to this entity (FK_EmployeeDepartmentHistory_Shift_ShiftID)
        public virtual ICollection<EmployeeDepartmentHistory> EmployeeDepartmentHistories { get; set; } = new HashSet<EmployeeDepartmentHistory>();
    }
}

