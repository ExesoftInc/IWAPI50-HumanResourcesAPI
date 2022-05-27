using HumanResourcesAPI.Entities;
using HumanResourcesAPI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HumanResourcesAPI.Models {
    
    
    public class EmployeeModel {
        
        protected internal int _businessEntityID;
        
        protected internal string _nationalIDNumber;
        
        protected internal string _loginID;
        
        protected internal short? _organizationLevel;
        
        protected internal string _jobTitle;
        
        protected internal System.DateTime _birthDate;
        
        protected internal string _maritalStatus;
        
        protected internal string _gender;
        
        protected internal System.DateTime _hireDate;
        
        protected internal bool _salariedFlag;
        
        protected internal short _vacationHours;
        
        protected internal short _sickLeaveHours;
        
        protected internal bool _currentFlag;
        
        protected internal System.Guid _rowguid;
        
        protected internal System.DateTime _modifiedDate;
        
        public EmployeeModel() {
        }
        
        internal EmployeeModel(Employee entity) {
            this._businessEntityID = entity.BusinessEntityID;
            this._nationalIDNumber = entity.NationalIDNumber;
            this._loginID = entity.LoginID;
            _organizationLevel = entity.OrganizationLevel;
            this._jobTitle = entity.JobTitle;
            this._birthDate = entity.BirthDate;
            this._maritalStatus = entity.MaritalStatus;
            this._gender = entity.Gender;
            this._hireDate = entity.HireDate;
            this._salariedFlag = entity.SalariedFlag;
            this._vacationHours = entity.VacationHours;
            this._sickLeaveHours = entity.SickLeaveHours;
            this._currentFlag = entity.CurrentFlag;
            _rowguid = entity.Rowguid;
            this._modifiedDate = entity.ModifiedDate;
        }
        
        [Required()]
        [Display(Name = "Business entity ID")]
        public int BusinessEntityID {
            get {
                return this._businessEntityID;
            }
            set {
                this._businessEntityID = value;
            }
        }
        
        [Required()]
        [MaxLength(15)]
        [StringLength(15)]
        [Display(Name = "National idn umber")]
        public string NationalIDNumber {
            get {
                return this._nationalIDNumber;
            }
            set {
                this._nationalIDNumber = value;
            }
        }
        
        [Required()]
        [MaxLength(256)]
        [StringLength(256)]
        [Display(Name = "Login ID")]
        public string LoginID {
            get {
                return this._loginID;
            }
            set {
                this._loginID = value;
            }
        }
        
        [Display(Name = "Organization level")]
        public short? OrganizationLevel {
            get {
                return this._organizationLevel;
            }
        }
        
        [Required()]
        [MaxLength(50)]
        [StringLength(50)]
        [Display(Name = "Job title")]
        public string JobTitle {
            get {
                return this._jobTitle;
            }
            set {
                this._jobTitle = value;
            }
        }
        
        [Required()]
        [DataType(DataType.Date)]
        [Display(Name = "Birth date")]
        public System.DateTime BirthDate {
            get {
                return this._birthDate;
            }
            set {
                this._birthDate = value;
            }
        }
        
        [Required()]
        [MaxLength(1)]
        [StringLength(1)]
        [Display(Name = "Marital status")]
        public string MaritalStatus {
            get {
                return this._maritalStatus;
            }
            set {
                this._maritalStatus = value;
            }
        }
        
        [Required()]
        [MaxLength(1)]
        [StringLength(1)]
        [Display(Name = "Gender")]
        public string Gender {
            get {
                return this._gender;
            }
            set {
                this._gender = value;
            }
        }
        
        [Required()]
        [DataType(DataType.Date)]
        [Display(Name = "Hire date")]
        public System.DateTime HireDate {
            get {
                return this._hireDate;
            }
            set {
                this._hireDate = value;
            }
        }
        
        [Required()]
        [Display(Name = "Salaried flag")]
        public bool SalariedFlag {
            get {
                return this._salariedFlag;
            }
            set {
                this._salariedFlag = value;
            }
        }
        
        [Required()]
        [Display(Name = "Vacation hours")]
        public short VacationHours {
            get {
                return this._vacationHours;
            }
            set {
                this._vacationHours = value;
            }
        }
        
        [Required()]
        [Display(Name = "Sick leave hours")]
        public short SickLeaveHours {
            get {
                return this._sickLeaveHours;
            }
            set {
                this._sickLeaveHours = value;
            }
        }
        
        [Required()]
        [Display(Name = "Current flag")]
        public bool CurrentFlag {
            get {
                return this._currentFlag;
            }
            set {
                this._currentFlag = value;
            }
        }
        
        [Display(Name = "Rowguid")]
        public System.Guid Rowguid {
            get {
                return this._rowguid;
            }
        }
        
        [Required()]
        [DataType(DataType.DateTime)]
        [Display(Name = "Modified date")]
        public System.DateTime ModifiedDate {
            get {
                return this._modifiedDate;
            }
            set {
                this._modifiedDate = value;
            }
        }
        
        /// Child EmployeeDepartmentHistories where [EmployeeDepartmentHistory].[BusinessEntityID] point to this entity (FK_EmployeeDepartmentHistory_Employee_BusinessEntityID)
        public virtual ICollection<EmployeeDepartmentHistoryModel> EmployeeDepartmentHistoriesModel { get; set; } = new HashSet<EmployeeDepartmentHistoryModel>();
        
        /// Child EmployeePayHistories where [EmployeePayHistory].[BusinessEntityID] point to this entity (FK_EmployeePayHistory_Employee_BusinessEntityID)
        public virtual ICollection<EmployeePayHistoryModel> EmployeePayHistoriesModel { get; set; } = new HashSet<EmployeePayHistoryModel>();
        
        /// Child JobCandidates where [JobCandidate].[BusinessEntityID] point to this entity (FK_JobCandidate_Employee_BusinessEntityID)
        public virtual ICollection<JobCandidateModel> JobCandidatesModel { get; set; } = new HashSet<JobCandidateModel>();
        
        public override int GetHashCode() {
            int hash = 0;
            hash ^=BusinessEntityID.GetHashCode();
            return hash;
        }
        
        public override string ToString() {
            return BusinessEntityID.ToString()
;
        }
        
        public override bool Equals(object obj) {
        bool result = false;

            if (obj is EmployeeModel) {
                EmployeeModel toCompare = (EmployeeModel)obj;
              if(toCompare != null)
              {
                  result = Equals(toCompare);
              }
            }

            return result;
        }
        
        public virtual bool Equals(EmployeeModel toCompare) {

        bool result = false;

            if (toCompare != null) {
                result = toCompare.BusinessEntityID == BusinessEntityID
;
            }

            return result;
        }
    }
}

