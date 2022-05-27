using HumanResourcesAPI.Entities;
using HumanResourcesAPI.Models;
using HumanResourcesAPI.Services;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;

namespace HumanResourcesAPI.Services {
    
    
    public class EmployeeDepartmentHistoryBuilder : IEmployeeDepartmentHistoryBuilder {
        
        private IDbEntities _entities;
        
        private ILoggerManager _logger;
        
        public EmployeeDepartmentHistoryBuilder(EntitiesContext context, ILoggerManager logger) {
            _entities = context;
            _logger = logger;
        }
        
        private Expression<Func<EmployeeDepartmentHistory, EmployeeDepartmentHistoryModel>>  ProjectToModel {
            get {
                return entity => new EmployeeDepartmentHistoryModel(entity);
            }
        }
        
        public async Task<IQueryable<EmployeeDepartmentHistoryModel>> GetEmployeeDepartmentHistories() {
            return await Task.FromResult(_entities.EmployeeDepartmentHistories.Select(ProjectToModel));
        }
        
        public IList<ExpandoObject> GetDisplayModels(List<string> propNames) {
            var models = _entities.EmployeeDepartmentHistories.Select(ProjectToModel);

            var displayModels = new List<ExpandoObject>();
            foreach (var model in models)
            {
                dynamic displayModel = DynamicHelper.ConvertToExpando(model, propNames);
                displayModels.Add(displayModel);
            }

            return displayModels;
        }
        
        public async Task<BuilderResponse> GetEmployeeDepartmentHistory_ByBusinessEntityIDStartDateDepartmentIDShiftID(int businessEntityID, System.DateTime startDate, short departmentID, byte shiftID) {
            var query = await Search(_entities.EmployeeDepartmentHistories, x => x.BusinessEntityID == businessEntityID&& x.StartDate == startDate&& x.DepartmentID == departmentID&& x.ShiftID == shiftID).Select(ProjectToModel)?.ToListAsync();
            if (query.Any()) {
                return new BuilderResponse{ Model = query.Single() }; 
            }
            else {
                return new BuilderResponse { ValidationMessage = $"Record Not Found; EmployeeDepartmentHistory with businessEntityID, startDate, departmentID, shiftID = '{businessEntityID}', '{startDate}', '{departmentID}', '{shiftID}' doesn't exist." }; 
            }
        }
        
        public async Task<IQueryable<EmployeeDepartmentHistoryModel>> GetEmployeeDepartmentHistory_ByBusinessEntityID(int businessEntityID) {

            var query = await Task.FromResult(Search(_entities.EmployeeDepartmentHistories, x => x.BusinessEntityID == businessEntityID).Select(ProjectToModel));

            return query;
        }
        
        public async Task<IQueryable<EmployeeDepartmentHistoryModel>> GetEmployeeDepartmentHistory_ByDepartmentID(short departmentID) {

            var query = await Task.FromResult(Search(_entities.EmployeeDepartmentHistories, x => x.DepartmentID == departmentID).Select(ProjectToModel));

            return query;
        }
        
        public async Task<IQueryable<EmployeeDepartmentHistoryModel>> GetEmployeeDepartmentHistory_ByShiftID(byte shiftID) {

            var query = await Task.FromResult(Search(_entities.EmployeeDepartmentHistories, x => x.ShiftID == shiftID).Select(ProjectToModel));

            return query;
        }
        
        public async Task<BuilderResponse> AddEmployeeDepartmentHistory(EmployeeDepartmentHistoryModel model) {

            var matchBusinessEntityID = _entities.Employees.Where(x => x.BusinessEntityID.Equals(model.BusinessEntityID));
            if (!matchBusinessEntityID.Any()) {
                return new BuilderResponse { ValidationMessage = $"Foreign Key Violation; " + nameof(model.BusinessEntityID) + " '{model.BusinessEntityID}' doesn't exist in the system."}; 
            }

            var matchDepartmentID = _entities.Departments.Where(x => x.DepartmentID.Equals(model.DepartmentID));
            if (!matchDepartmentID.Any()) {
                return new BuilderResponse { ValidationMessage = $"Foreign Key Violation; " + nameof(model.DepartmentID) + " '{model.DepartmentID}' doesn't exist in the system."}; 
            }

            var matchShiftID = _entities.Shifts.Where(x => x.ShiftID.Equals(model.ShiftID));
            if (!matchShiftID.Any()) {
                return new BuilderResponse { ValidationMessage = $"Foreign Key Violation; " + nameof(model.ShiftID) + " '{model.ShiftID}' doesn't exist in the system."}; 
            }


            var entity = ModelExtender.ToEntity(model);
            _entities.EmployeeDepartmentHistories.Add(entity);
            await _entities.SaveChangesAsync();
            _logger.LogInfo(string.Format("EmployeeDepartmentHistory added with values: '{0}'", JsonConvert.SerializeObject(model)));

            return new BuilderResponse{ Model = new EmployeeDepartmentHistoryModel(entity) }; 
        }
        
        public async Task<BuilderResponse> UpdateEmployeeDepartmentHistory(EmployeeDepartmentHistoryModel model) {

            var query = Search(_entities.EmployeeDepartmentHistories, x =>  x.BusinessEntityID == model.BusinessEntityID && x.StartDate == model.StartDate && x.DepartmentID == model.DepartmentID && x.ShiftID == model.ShiftID);
            if (!query.Any()) {
            return new BuilderResponse { ValidationMessage = "Record Not Found; " + string.Format("EmployeeDepartmentHistory with _businessEntityID, _startDate, _departmentID, _shiftID = '{0}', '{1}', '{2}', '{3}' doesn't exist.",model.BusinessEntityID, model.StartDate, model.DepartmentID, model.ShiftID)}; 
            }

            var matchBusinessEntityID = _entities.Employees.Where(x => x.BusinessEntityID.Equals(model.BusinessEntityID));
            if (!matchBusinessEntityID.Any()) {
            return new BuilderResponse { ValidationMessage = "Foreign Key Violation; " + nameof(model.BusinessEntityID) + string.Format("BusinessEntityID = '{0}' doesn't exist in the system.", model.BusinessEntityID)}; 
            }

            var matchDepartmentID = _entities.Departments.Where(x => x.DepartmentID.Equals(model.DepartmentID));
            if (!matchDepartmentID.Any()) {
            return new BuilderResponse { ValidationMessage = "Foreign Key Violation; " + nameof(model.DepartmentID) + string.Format("DepartmentID = '{0}' doesn't exist in the system.", model.DepartmentID)}; 
            }

            var matchShiftID = _entities.Shifts.Where(x => x.ShiftID.Equals(model.ShiftID));
            if (!matchShiftID.Any()) {
            return new BuilderResponse { ValidationMessage = "Foreign Key Violation; " + nameof(model.ShiftID) + string.Format("ShiftID = '{0}' doesn't exist in the system.", model.ShiftID)}; 
            }

            EmployeeDepartmentHistory entity = query.SingleOrDefault();

            entity = model.ToEntity(entity);
            await _entities.SaveChangesAsync();
            _logger.LogInfo(string.Format("EmployeeDepartmentHistory update with values: '{0}'", JsonConvert.SerializeObject(model)));

            return new BuilderResponse{ StatusCode = (int)HttpStatusCode.Accepted }; 
        }
        
        public async Task<BuilderResponse> DeleteEmployeeDepartmentHistory(int businessEntityID, System.DateTime startDate, short departmentID, byte shiftID) {

            var query = Search(_entities.EmployeeDepartmentHistories, x => x.BusinessEntityID == businessEntityID&& x.StartDate == startDate&& x.DepartmentID == departmentID&& x.ShiftID == shiftID);
            if (!query.Any()) {
                return new BuilderResponse { ValidationMessage = "Record Not Found; " + string.Format("EmployeeDepartmentHistory with _businessEntityID, _startDate, _departmentID, _shiftID = '{0}', '{1}', '{2}', '{3}' doesn't exist.",businessEntityID, startDate, departmentID, shiftID)}; 
            }

            var entity = query.SingleOrDefault();

            _entities.EmployeeDepartmentHistories.Remove(entity);
            await _entities.SaveChangesAsync();
            _logger.LogInfo(string.Format("EmployeeDepartmentHistory deleted with values: '{0}'", JsonConvert.SerializeObject(new EmployeeDepartmentHistoryModel(entity))));

            return new BuilderResponse{ StatusCode = (int)HttpStatusCode.NoContent }; 
        }
        
        private IQueryable<EmployeeDepartmentHistory> Search(IQueryable<EmployeeDepartmentHistory> query, Expression<Func<EmployeeDepartmentHistory, bool>> filter) {
            return query.Where(filter);
        }
    }
}

