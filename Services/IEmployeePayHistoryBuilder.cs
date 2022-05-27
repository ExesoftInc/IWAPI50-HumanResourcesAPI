using HumanResourcesAPI.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HumanResourcesAPI.Services {
    
    
    public interface IEmployeePayHistoryBuilder {
        
        Task<IQueryable<EmployeePayHistoryModel>> GetEmployeePayHistories();
        
        IList<ExpandoObject> GetDisplayModels(List<string> propNames);
        
        Task<BuilderResponse> GetEmployeePayHistory_ByBusinessEntityIDRateChangeDate(int businessEntityID, System.DateTime rateChangeDate);
        
        Task<IQueryable<EmployeePayHistoryModel>> GetEmployeePayHistory_ByBusinessEntityID(int businessEntityID);
        
        Task<BuilderResponse> AddEmployeePayHistory(EmployeePayHistoryModel model);
        
        Task<BuilderResponse> UpdateEmployeePayHistory(EmployeePayHistoryModel model);
        
        Task<BuilderResponse> DeleteEmployeePayHistory(int businessEntityID, System.DateTime rateChangeDate);
    }
}

