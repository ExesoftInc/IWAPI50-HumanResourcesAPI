using HumanResourcesAPI.Entities;
using HumanResourcesAPI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HumanResourcesAPI.Models {
    
    
    public class EmployeePayHistoryModel {
        
        protected internal int _businessEntityID;
        
        protected internal System.DateTime _rateChangeDate;
        
        protected internal decimal _rate;
        
        protected internal byte _payFrequency;
        
        protected internal System.DateTime _modifiedDate;
        
        public EmployeePayHistoryModel() {
        }
        
        internal EmployeePayHistoryModel(EmployeePayHistory entity) {
            this._businessEntityID = entity.BusinessEntityID;
            this._rateChangeDate = entity.RateChangeDate;
            this._rate = entity.Rate;
            this._payFrequency = entity.PayFrequency;
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
        [DataType(DataType.DateTime)]
        [Display(Name = "Rate change date")]
        public System.DateTime RateChangeDate {
            get {
                return this._rateChangeDate;
            }
            set {
                this._rateChangeDate = value;
            }
        }
        
        [Required()]
        [DataType(DataType.Currency)]
        [Display(Name = "Rate")]
        public decimal Rate {
            get {
                return this._rate;
            }
            set {
                this._rate = value;
            }
        }
        
        [Required()]
        [Display(Name = "Pay frequency")]
        public byte PayFrequency {
            get {
                return this._payFrequency;
            }
            set {
                this._payFrequency = value;
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
            hash ^=RateChangeDate.GetHashCode();
            return hash;
        }
        
        public override string ToString() {
            return BusinessEntityID.ToString()
                 + "-" + RateChangeDate.ToShortDateString()
;
        }
        
        public override bool Equals(object obj) {
        bool result = false;

            if (obj is EmployeePayHistoryModel) {
                EmployeePayHistoryModel toCompare = (EmployeePayHistoryModel)obj;
              if(toCompare != null)
              {
                  result = Equals(toCompare);
              }
            }

            return result;
        }
        
        public virtual bool Equals(EmployeePayHistoryModel toCompare) {

        bool result = false;

            if (toCompare != null) {
                result = toCompare.BusinessEntityID == BusinessEntityID
             && toCompare.RateChangeDate.Equals(RateChangeDate)
;
            }

            return result;
        }
    }
}

