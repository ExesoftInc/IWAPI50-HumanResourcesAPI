using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HumanResourcesAPI.Entities {
    
    
    public partial class EmployeeDepartmentHistory {
        
        [Key()]
        [Column(Order=1)]
        [Display(Name = "Business entity ID")]
        public int BusinessEntityID { get; set; }
        
        [Key()]
        [Column(Order=3)]
        [Display(Name = "Department ID")]
        public short DepartmentID { get; set; }
        
        [Key()]
        [Column(Order=4)]
        [Display(Name = "Shift ID")]
        public byte ShiftID { get; set; }
        
        [Key()]
        [Column(Order=2)]
        [Display(Name = "Start date")]
        public System.DateTime StartDate { get; set; }
        
        [Display(Name = "End date")]
        public System.DateTime? EndDate { get; set; }
        
        [Display(Name = "Modified date")]
        public System.DateTime ModifiedDate { get; set; }
        
        // Parent Employee pointed by [EmployeeDepartmentHistory].([BusinessEntityID]) (FK_EmployeeDepartmentHistory_Employee_BusinessEntityID)
        public virtual Employee Employee { get; set; }
        
        // Parent Department pointed by [EmployeeDepartmentHistory].([DepartmentID]) (FK_EmployeeDepartmentHistory_Department_DepartmentID)
        public virtual Department Department { get; set; }
        
        // Parent Shift pointed by [EmployeeDepartmentHistory].([ShiftID]) (FK_EmployeeDepartmentHistory_Shift_ShiftID)
        public virtual Shift Shift { get; set; }
    }
}

