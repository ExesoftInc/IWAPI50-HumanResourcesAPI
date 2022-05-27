using HumanResourcesAPI.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HumanResourcesAPI.Services {
    
    
    public interface IEmployeeBuilder {
        
        Task<IQueryable<EmployeeModel>> GetEmployees();
        
        IList<ExpandoObject> GetDisplayModels(List<string> propNames);
        
        Task<BuilderResponse> GetEmployee_ByBusinessEntityID(int businessEntityID);
        
        Task<BuilderResponse> AddEmployee(EmployeeModel model);
        
        Task<BuilderResponse> UpdateEmployee(EmployeeModel model);
        
        Task<BuilderResponse> DeleteEmployee(int businessEntityID);
    }
}

