using HumanResourcesAPI.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HumanResourcesAPI.Services {
    
    
    public interface IEmployeeDepartmentHistoryBuilder {
        
        Task<IQueryable<EmployeeDepartmentHistoryModel>> GetEmployeeDepartmentHistories();
        
        IList<ExpandoObject> GetDisplayModels(List<string> propNames);
        
        Task<BuilderResponse> GetEmployeeDepartmentHistory_ByBusinessEntityIDStartDateDepartmentIDShiftID(int businessEntityID, System.DateTime startDate, short departmentID, byte shiftID);
        
        Task<IQueryable<EmployeeDepartmentHistoryModel>> GetEmployeeDepartmentHistory_ByBusinessEntityID(int businessEntityID);
        
        Task<IQueryable<EmployeeDepartmentHistoryModel>> GetEmployeeDepartmentHistory_ByDepartmentID(short departmentID);
        
        Task<IQueryable<EmployeeDepartmentHistoryModel>> GetEmployeeDepartmentHistory_ByShiftID(byte shiftID);
        
        Task<BuilderResponse> AddEmployeeDepartmentHistory(EmployeeDepartmentHistoryModel model);
        
        Task<BuilderResponse> UpdateEmployeeDepartmentHistory(EmployeeDepartmentHistoryModel model);
        
        Task<BuilderResponse> DeleteEmployeeDepartmentHistory(int businessEntityID, System.DateTime startDate, short departmentID, byte shiftID);
    }
}

