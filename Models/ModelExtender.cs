// ----------------------------------------------------------------------------------
// <copyright company="Exesoft Inc.">
//	This code was generated by Instant Web API code automation software (https://www.InstantWebAPI.com)
//	Copyright Exesoft Inc. © 2019.  All rights reserved.
// </copyright>
// ----------------------------------------------------------------------------------

using HumanResourcesAPI.Entities;
using HumanResourcesAPI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HumanResourcesAPI.Entities {
    
    
    public static class ModelExtender {
        
        public static Department ToEntity(this DepartmentModel model, Department entity) {

            entity.Name = model.Name;
            entity.GroupName = model.GroupName;
            entity.ModifiedDate = model.ModifiedDate;

            return entity;
        }
        
        public static Employee ToEntity(this EmployeeModel model, Employee entity) {

            entity.NationalIDNumber = model.NationalIDNumber;
            entity.LoginID = model.LoginID;
            entity.JobTitle = model.JobTitle;
            entity.BirthDate = model.BirthDate;
            entity.MaritalStatus = model.MaritalStatus;
            entity.Gender = model.Gender;
            entity.HireDate = model.HireDate;
            entity.SalariedFlag = model.SalariedFlag;
            entity.VacationHours = model.VacationHours;
            entity.SickLeaveHours = model.SickLeaveHours;
            entity.CurrentFlag = model.CurrentFlag;
            entity.ModifiedDate = model.ModifiedDate;

            return entity;
        }
        
        public static EmployeeDepartmentHistory ToEntity(this EmployeeDepartmentHistoryModel model, EmployeeDepartmentHistory entity) {

            entity.EndDate = model.EndDate;
            entity.ModifiedDate = model.ModifiedDate;

            return entity;
        }
        
        public static EmployeePayHistory ToEntity(this EmployeePayHistoryModel model, EmployeePayHistory entity) {

            entity.Rate = model.Rate;
            entity.PayFrequency = model.PayFrequency;
            entity.ModifiedDate = model.ModifiedDate;

            return entity;
        }
        
        public static JobCandidate ToEntity(this JobCandidateModel model, JobCandidate entity) {

            entity.BusinessEntityID = model.BusinessEntityID;
            entity.Resume = model.Resume;
            entity.ModifiedDate = model.ModifiedDate;

            return entity;
        }
        
        public static Shift ToEntity(this ShiftModel model, Shift entity) {

            entity.Name = model.Name;
            entity.StartTime = model.StartTime;
            entity.EndTime = model.EndTime;
            entity.ModifiedDate = model.ModifiedDate;

            return entity;
        }
    }
}

