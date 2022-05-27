using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HumanResourcesAPI.Entities {
    
    
    public partial class JobCandidate {
        
        [Key()]
        [Display(Name = "Job candidate ID")]
        public int JobCandidateID { get; set; }
        
        [Display(Name = "Business entity ID")]
        public System.Int32? BusinessEntityID { get; set; }
        
        [Display(Name = "Resume")]
        public string Resume { get; set; }
        
        [Display(Name = "Modified date")]
        public System.DateTime ModifiedDate { get; set; }
        
        // Parent Employee pointed by [JobCandidate].([BusinessEntityID]) (FK_JobCandidate_Employee_BusinessEntityID)
        public virtual Employee Employee { get; set; }
    }
}

