using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HumanResourcesAPI.Entities {
    
    
    public partial class EmployeePayHistory {
        
        [Key()]
        [Column(Order=1)]
        [Display(Name = "Business entity ID")]
        public int BusinessEntityID { get; set; }
        
        [Key()]
        [Column(Order=2)]
        [Display(Name = "Rate change date")]
        public System.DateTime RateChangeDate { get; set; }
        
        [Display(Name = "Rate")]
        public decimal Rate { get; set; }
        
        [Display(Name = "Pay frequency")]
        public byte PayFrequency { get; set; }
        
        [Display(Name = "Modified date")]
        public System.DateTime ModifiedDate { get; set; }
        
        // Parent Employee pointed by [EmployeePayHistory].([BusinessEntityID]) (FK_EmployeePayHistory_Employee_BusinessEntityID)
        public virtual Employee Employee { get; set; }
    }
}

