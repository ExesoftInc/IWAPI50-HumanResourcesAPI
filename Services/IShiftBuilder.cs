using HumanResourcesAPI.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HumanResourcesAPI.Services {
    
    
    public interface IShiftBuilder {
        
        Task<IQueryable<ShiftModel>> GetShifts();
        
        IList<ExpandoObject> GetDisplayModels(List<string> propNames);
        
        Task<BuilderResponse> GetShift_ByShiftID(byte shiftID);
        
        Task<BuilderResponse> AddShift(ShiftModel model);
        
        Task<BuilderResponse> UpdateShift(ShiftModel model);
        
        Task<BuilderResponse> DeleteShift(byte shiftID);
    }
}

