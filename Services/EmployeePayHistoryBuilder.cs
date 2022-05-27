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
    
    
    public class EmployeePayHistoryBuilder : IEmployeePayHistoryBuilder {
        
        private IDbEntities _entities;
        
        private ILoggerManager _logger;
        
        public EmployeePayHistoryBuilder(EntitiesContext context, ILoggerManager logger) {
            _entities = context;
            _logger = logger;
        }
        
        private Expression<Func<EmployeePayHistory, EmployeePayHistoryModel>>  ProjectToModel {
            get {
                return entity => new EmployeePayHistoryModel(entity);
            }
        }
        
        public async Task<IQueryable<EmployeePayHistoryModel>> GetEmployeePayHistories() {
            return await Task.FromResult(_entities.EmployeePayHistories.Select(ProjectToModel));
        }
        
        public IList<ExpandoObject> GetDisplayModels(List<string> propNames) {
            var models = _entities.EmployeePayHistories.Select(ProjectToModel);

            var displayModels = new List<ExpandoObject>();
            foreach (var model in models)
            {
                dynamic displayModel = DynamicHelper.ConvertToExpando(model, propNames);
                displayModels.Add(displayModel);
            }

            return displayModels;
        }
        
        public async Task<BuilderResponse> GetEmployeePayHistory_ByBusinessEntityIDRateChangeDate(int businessEntityID, System.DateTime rateChangeDate) {
            var query = await Search(_entities.EmployeePayHistories, x => x.BusinessEntityID == businessEntityID&& x.RateChangeDate == rateChangeDate).Select(ProjectToModel)?.ToListAsync();
            if (query.Any()) {
                return new BuilderResponse{ Model = query.Single() }; 
            }
            else {
                return new BuilderResponse { ValidationMessage = $"Record Not Found; EmployeePayHistory with businessEntityID, rateChangeDate = '{businessEntityID}', '{rateChangeDate}' doesn't exist." }; 
            }
        }
        
        public async Task<IQueryable<EmployeePayHistoryModel>> GetEmployeePayHistory_ByBusinessEntityID(int businessEntityID) {

            var query = await Task.FromResult(Search(_entities.EmployeePayHistories, x => x.BusinessEntityID == businessEntityID).Select(ProjectToModel));

            return query;
        }
        
        public async Task<BuilderResponse> AddEmployeePayHistory(EmployeePayHistoryModel model) {

            var matchBusinessEntityID = _entities.Employees.Where(x => x.BusinessEntityID.Equals(model.BusinessEntityID));
            if (!matchBusinessEntityID.Any()) {
                return new BuilderResponse { ValidationMessage = $"Foreign Key Violation; " + nameof(model.BusinessEntityID) + " '{model.BusinessEntityID}' doesn't exist in the system."}; 
            }


            var entity = ModelExtender.ToEntity(model);
            _entities.EmployeePayHistories.Add(entity);
            await _entities.SaveChangesAsync();
            _logger.LogInfo(string.Format("EmployeePayHistory added with values: '{0}'", JsonConvert.SerializeObject(model)));

            return new BuilderResponse{ Model = new EmployeePayHistoryModel(entity) }; 
        }
        
        public async Task<BuilderResponse> UpdateEmployeePayHistory(EmployeePayHistoryModel model) {

            var query = Search(_entities.EmployeePayHistories, x =>  x.BusinessEntityID == model.BusinessEntityID && x.RateChangeDate == model.RateChangeDate);
            if (!query.Any()) {
            return new BuilderResponse { ValidationMessage = "Record Not Found; " + string.Format("EmployeePayHistory with _businessEntityID, _rateChangeDate = '{0}', '{1}' doesn't exist.",model.BusinessEntityID, model.RateChangeDate)}; 
            }

            var matchBusinessEntityID = _entities.Employees.Where(x => x.BusinessEntityID.Equals(model.BusinessEntityID));
            if (!matchBusinessEntityID.Any()) {
            return new BuilderResponse { ValidationMessage = "Foreign Key Violation; " + nameof(model.BusinessEntityID) + string.Format("BusinessEntityID = '{0}' doesn't exist in the system.", model.BusinessEntityID)}; 
            }

            EmployeePayHistory entity = query.SingleOrDefault();

            entity = model.ToEntity(entity);
            await _entities.SaveChangesAsync();
            _logger.LogInfo(string.Format("EmployeePayHistory update with values: '{0}'", JsonConvert.SerializeObject(model)));

            return new BuilderResponse{ StatusCode = (int)HttpStatusCode.Accepted }; 
        }
        
        public async Task<BuilderResponse> DeleteEmployeePayHistory(int businessEntityID, System.DateTime rateChangeDate) {

            var query = Search(_entities.EmployeePayHistories, x => x.BusinessEntityID == businessEntityID&& x.RateChangeDate == rateChangeDate);
            if (!query.Any()) {
                return new BuilderResponse { ValidationMessage = "Record Not Found; " + string.Format("EmployeePayHistory with _businessEntityID, _rateChangeDate = '{0}', '{1}' doesn't exist.",businessEntityID, rateChangeDate)}; 
            }

            var entity = query.SingleOrDefault();

            _entities.EmployeePayHistories.Remove(entity);
            await _entities.SaveChangesAsync();
            _logger.LogInfo(string.Format("EmployeePayHistory deleted with values: '{0}'", JsonConvert.SerializeObject(new EmployeePayHistoryModel(entity))));

            return new BuilderResponse{ StatusCode = (int)HttpStatusCode.NoContent }; 
        }
        
        private IQueryable<EmployeePayHistory> Search(IQueryable<EmployeePayHistory> query, Expression<Func<EmployeePayHistory, bool>> filter) {
            return query.Where(filter);
        }
    }
}

