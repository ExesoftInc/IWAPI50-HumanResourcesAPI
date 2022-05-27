using HumanResourcesAPI.Entities;
using HumanResourcesAPI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HumanResourcesAPI.Models {
    
    
    public class EmployeeDepartmentHistoryModel {
        
        protected internal int _businessEntityID;
        
        protected internal short _departmentID;
        
        protected internal byte _shiftID;
        
        protected internal System.DateTime _startDate;
        
        protected internal System.DateTime? _endDate;
        
        protected internal System.DateTime _modifiedDate;
        
        public EmployeeDepartmentHistoryModel() {
        }
        
        internal EmployeeDepartmentHistoryModel(EmployeeDepartmentHistory entity) {
            this._businessEntityID = entity.BusinessEntityID;
            this._departmentID = entity.DepartmentID;
            this._shiftID = entity.ShiftID;
            this._startDate = entity.StartDate;
            this._endDate = entity.EndDate;
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
        [Display(Name = "Shift ID")]
        public byte ShiftID {
            get {
                return this._shiftID;
            }
            set {
                this._shiftID = value;
            }
        }
        
        [Required()]
        [DataType(DataType.Date)]
        [Display(Name = "Start date")]
        public System.DateTime StartDate {
            get {
                return this._startDate;
            }
            set {
                this._startDate = value;
            }
        }
        
        [DataType(DataType.Date)]
        [Display(Name = "End date")]
        public System.DateTime? EndDate {
            get {
                return this._endDate;
            }
            set {
                this._endDate = value;
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
        
        public override int GetHashCode() {
            int hash = 0;
            hash ^=BusinessEntityID.GetHashCode();
            hash ^=DepartmentID.GetHashCode();
            hash ^=ShiftID.GetHashCode();
            hash ^=StartDate.GetHashCode();
            return hash;
        }
        
        public override string ToString() {
            return BusinessEntityID.ToString()
                 + "-" + DepartmentID.ToString()
                 + "-" + ShiftID.ToString()
                 + "-" + StartDate.ToShortDateString()
;
        }
        
        public override bool Equals(object obj) {
        bool result = false;

            if (obj is EmployeeDepartmentHistoryModel) {
                EmployeeDepartmentHistoryModel toCompare = (EmployeeDepartmentHistoryModel)obj;
              if(toCompare != null)
              {
                  result = Equals(toCompare);
              }
            }

            return result;
        }
        
        public virtual bool Equals(EmployeeDepartmentHistoryModel toCompare) {

        bool result = false;

            if (toCompare != null) {
                result = toCompare.BusinessEntityID == BusinessEntityID
             && toCompare.DepartmentID == DepartmentID
             && toCompare.ShiftID == ShiftID
             && toCompare.StartDate.Equals(StartDate)
;
            }

            return result;
        }
    }
}

