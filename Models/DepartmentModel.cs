using HumanResourcesAPI.Entities;
using HumanResourcesAPI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HumanResourcesAPI.Models {
    
    
    public class DepartmentModel {
        
        protected internal short _departmentID;
        
        protected internal string _name;
        
        protected internal string _groupName;
        
        protected internal System.DateTime _modifiedDate;
        
        public DepartmentModel() {
        }
        
        internal DepartmentModel(Department entity) {
            this._departmentID = entity.DepartmentID;
            this._name = entity.Name;
            this._groupName = entity.GroupName;
            this._modifiedDate = entity.ModifiedDate;
        }
        
        [Display(Name = "Department ID")]
        public short DepartmentID {
            get {
                return this._departmentID;
            }
            set {
                this._departmentID = value;
            }
        }
        
        [Required()]
        [MaxLength(50)]
        [StringLength(50)]
        [Display(Name = "Name")]
        public string Name {
            get {
                return this._name;
            }
            set {
                this._name = value;
            }
        }
        
        [Required()]
        [MaxLength(50)]
        [StringLength(50)]
        [Display(Name = "Group name")]
        public string GroupName {
            get {
                return this._groupName;
            }
            set {
                this._groupName = value;
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
        
        /// Child EmployeeDepartmentHistories where [EmployeeDepartmentHistory].[DepartmentID] point to this entity (FK_EmployeeDepartmentHistory_Department_DepartmentID)
        public virtual ICollection<EmployeeDepartmentHistoryModel> EmployeeDepartmentHistoriesModel { get; set; } = new HashSet<EmployeeDepartmentHistoryModel>();
        
        public override int GetHashCode() {
            int hash = 0;
            hash ^=Name.GetHashCode();
            return hash;
        }
        
        public override string ToString() {
            return Name
;
        }
        
        public override bool Equals(object obj) {
        bool result = false;

            if (obj is DepartmentModel) {
                DepartmentModel toCompare = (DepartmentModel)obj;
              if(toCompare != null)
              {
                  result = Equals(toCompare);
              }
            }

            return result;
        }
        
        public virtual bool Equals(DepartmentModel toCompare) {

        bool result = false;

            if (toCompare != null) {
                result = string.Compare(toCompare.Name, Name, true) == 0
;
            }

            return result;
        }
    }
}

