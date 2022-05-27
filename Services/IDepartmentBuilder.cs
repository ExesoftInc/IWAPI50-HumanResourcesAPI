using HumanResourcesAPI.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HumanResourcesAPI.Services {
    
    
    public interface IDepartmentBuilder {
        
        Task<IQueryable<DepartmentModel>> GetDepartments();
        
        IList<ExpandoObject> GetDisplayModels(List<string> propNames);
        
        Task<BuilderResponse> GetDepartment_ByDepartmentID(short departmentID);
        
        Task<BuilderResponse> AddDepartment(DepartmentModel model);
        
        Task<BuilderResponse> UpdateDepartment(DepartmentModel model);
        
        Task<BuilderResponse> DeleteDepartment(short departmentID);
    }
}

