// ----------------------------------------------------------------------------------
// <copyright company="Exesoft Inc.">
//	This code was generated by Instant Web API code automation software (https://www.InstantWebAPI.com)
//	Copyright Exesoft Inc. © 2019.  All rights reserved.
// </copyright>
// ----------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HumanResourcesAPI.Entities {
    
    
    public partial class Employee {
        
        [Key()]
        [Display(Name = "Business entity ID")]
        public int BusinessEntityID { get; set; }
        
        [Display(Name = "National idn umber")]
        public string NationalIDNumber { get; set; }
        
        [Display(Name = "Login ID")]
        public string LoginID { get; set; }
        
        [Display(Name = "Organization level")]
        public System.Int16? OrganizationLevel { get;private set; }
        
        [Display(Name = "Job title")]
        public string JobTitle { get; set; }
        
        [Display(Name = "Birth date")]
        public System.DateTime BirthDate { get; set; }
        
        [Display(Name = "Marital status")]
        public string MaritalStatus { get; set; }
        
        [Display(Name = "Gender")]
        public string Gender { get; set; }
        
        [Display(Name = "Hire date")]
        public System.DateTime HireDate { get; set; }
        
        [Display(Name = "Salaried flag")]
        public bool SalariedFlag { get; set; }
        
        [Display(Name = "Vacation hours")]
        public short VacationHours { get; set; }
        
        [Display(Name = "Sick leave hours")]
        public short SickLeaveHours { get; set; }
        
        [Display(Name = "Current flag")]
        public bool CurrentFlag { get; set; }
        
        [Display(Name = "Rowguid")]
        public System.Guid Rowguid { get; set; }
        
        [Display(Name = "Modified date")]
        public System.DateTime ModifiedDate { get; set; }
        
        /// Child EmployeeDepartmentHistories where [EmployeeDepartmentHistory].[BusinessEntityID] point to this entity (FK_EmployeeDepartmentHistory_Employee_BusinessEntityID)
        public virtual ICollection<EmployeeDepartmentHistory> EmployeeDepartmentHistories { get; set; } = new HashSet<EmployeeDepartmentHistory>();
        
        /// Child EmployeePayHistories where [EmployeePayHistory].[BusinessEntityID] point to this entity (FK_EmployeePayHistory_Employee_BusinessEntityID)
        public virtual ICollection<EmployeePayHistory> EmployeePayHistories { get; set; } = new HashSet<EmployeePayHistory>();
        
        /// Child JobCandidates where [JobCandidate].[BusinessEntityID] point to this entity (FK_JobCandidate_Employee_BusinessEntityID)
        public virtual ICollection<JobCandidate> JobCandidates { get; set; } = new HashSet<JobCandidate>();
    }
}

