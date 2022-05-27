using HumanResourcesAPI.Entities;
using HumanResourcesAPI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HumanResourcesAPI.Models {
    
    
    public class JobCandidateModel {
        
        protected internal int _jobCandidateID;
        
        protected internal int? _businessEntityID;
        
        protected internal string _resume;
        
        protected internal System.DateTime _modifiedDate;
        
        public JobCandidateModel() {
        }
        
        internal JobCandidateModel(JobCandidate entity) {
            this._jobCandidateID = entity.JobCandidateID;
            this._businessEntityID = entity.BusinessEntityID;
            this._resume = entity.Resume;
            this._modifiedDate = entity.ModifiedDate;
        }
        
        [Display(Name = "Job candidate ID")]
        public int JobCandidateID {
            get {
                return this._jobCandidateID;
            }
            set {
                this._jobCandidateID = value;
            }
        }
        
        [Display(Name = "Business entity ID")]
        public int? BusinessEntityID {
            get {
                return this._businessEntityID;
            }
            set {
                this._businessEntityID = value;
            }
        }
        
        [Display(Name = "Resume")]
        public string Resume {
            get {
                return this._resume;
            }
            set {
                this._resume = value;
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
            hash ^=Resume.GetHashCode();
            return hash;
        }
        
        public override string ToString() {
            return GetHashCode().ToString();
        }
        
        public override bool Equals(object obj) {
        bool result = false;

            if (obj is JobCandidateModel) {
                JobCandidateModel toCompare = (JobCandidateModel)obj;
              if(toCompare != null)
              {
                  result = Equals(toCompare);
              }
            }

            return result;
        }
        
        public virtual bool Equals(JobCandidateModel toCompare) {

        bool result = false;

            if (toCompare != null) {
                result = toCompare.BusinessEntityID.Equals(BusinessEntityID)
             && toCompare.Resume.Equals(Resume)
;
            }

            return result;
        }
    }
}

