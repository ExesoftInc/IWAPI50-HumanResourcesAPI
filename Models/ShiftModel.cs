using HumanResourcesAPI.Entities;
using HumanResourcesAPI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HumanResourcesAPI.Models {
    
    
    public class ShiftModel {
        
        protected internal byte _shiftID;
        
        protected internal string _name;
        
        protected internal System.TimeSpan _startTime;
        
        protected internal System.TimeSpan _endTime;
        
        protected internal System.DateTime _modifiedDate;
        
        public ShiftModel() {
        }
        
        internal ShiftModel(Shift entity) {
            this._shiftID = entity.ShiftID;
            this._name = entity.Name;
            this._startTime = entity.StartTime;
            this._endTime = entity.EndTime;
            this._modifiedDate = entity.ModifiedDate;
        }
        
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
        [Display(Name = "Start time")]
        public System.TimeSpan StartTime {
            get {
                return this._startTime;
            }
            set {
                this._startTime = value;
            }
        }
        
        [Required()]
        [Display(Name = "End time")]
        public System.TimeSpan EndTime {
            get {
                return this._endTime;
            }
            set {
                this._endTime = value;
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
        
        /// Child EmployeeDepartmentHistories where [EmployeeDepartmentHistory].[ShiftID] point to this entity (FK_EmployeeDepartmentHistory_Shift_ShiftID)
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

            if (obj is ShiftModel) {
                ShiftModel toCompare = (ShiftModel)obj;
              if(toCompare != null)
              {
                  result = Equals(toCompare);
              }
            }

            return result;
        }
        
        public virtual bool Equals(ShiftModel toCompare) {

        bool result = false;

            if (toCompare != null) {
                result = string.Compare(toCompare.Name, Name, true) == 0
;
            }

            return result;
        }
    }
}

