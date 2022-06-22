// ----------------------------------------------------------------------------------
// <copyright company="Exesoft Inc.">
//	This code was generated by Instant Web API code automation software (https://www.InstantWebAPI.com)
//	Copyright Exesoft Inc. © 2019.  All rights reserved.
// </copyright>
// ----------------------------------------------------------------------------------

using HumanResourcesAPI.Entities;
using HumanResourcesAPI.Models;
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
    
    
    public class EmployeeBuilder : IEmployeeBuilder {
        
        private IDbEntities _entities;
        
        private ILoggerManager _logger;
        
        public EmployeeBuilder(EntitiesContext context, ILoggerManager logger) {
            _entities = context;
            _logger = logger;
        }
        
        private Expression<Func<Employee, EmployeeModel>>  ProjectToModel {
            get {
                return entity => new EmployeeModel(entity);
            }
        }
        
        public async Task<IQueryable<EmployeeModel>> GetEmployees() {
            return await Task.FromResult(_entities.Employees.Select(ProjectToModel));
        }
        
        public IList<ExpandoObject> GetDisplayModels(List<string> propNames) {
            var models = _entities.Employees.Select(ProjectToModel);

            var displayModels = new List<ExpandoObject>();
            foreach (var model in models)
            {
                dynamic displayModel = DynamicHelper.ConvertToExpando(model, propNames);
                displayModels.Add(displayModel);
            }

            return displayModels;
        }
        
        public async Task<BuilderResponse> GetEmployee_ByBusinessEntityID(int businessEntityID) {
            var query = Search(_entities.Employees, x => x.BusinessEntityID == businessEntityID).Select(ProjectToModel);
            if (query.Any()) {
                return await Task.FromResult(new BuilderResponse{ Model = query.Single() }); 
            }
            else {
                return await Task.FromResult(new BuilderResponse { ValidationMessage = $"Record Not Found; Employee with businessEntityID = '{businessEntityID}' doesn't exist." }); 
            }
        }
        
        public async Task<BuilderResponse> AddEmployee(EmployeeModel model) {

            System.Int32 maxCount = 0;
            if(_entities.Employees.Count() > 0)
            maxCount = _entities.Employees.Max(x => x.BusinessEntityID);
            model.BusinessEntityID= ++maxCount;

            var entity = ModelExtender.ToEntity(model);
            _entities.Employees.Add(entity);
            await _entities.SaveChangesAsync();
            _logger.LogInfo(string.Format("Employee added with values: '{0}'", JsonConvert.SerializeObject(model)));

            return new BuilderResponse{ Model = new EmployeeModel(entity) }; 
        }
        
        public async Task<BuilderResponse> UpdateEmployee(EmployeeModel model) {

            var query = Search(_entities.Employees, x =>  x.BusinessEntityID == model.BusinessEntityID);
            if (!query.Any()) {
            return new BuilderResponse { ValidationMessage = "Record Not Found; " + string.Format("Employee with _businessEntityID = '{0}' doesn't exist.",model.BusinessEntityID)}; 
            }

            Employee entity = query.SingleOrDefault();

            entity = model.ToEntity(entity);
            await _entities.SaveChangesAsync();
            _logger.LogInfo(string.Format("Employee update with values: '{0}'", JsonConvert.SerializeObject(model)));

            return new BuilderResponse{ StatusCode = (int)HttpStatusCode.Accepted }; 
        }
        
        public async Task<BuilderResponse> DeleteEmployee(int businessEntityID) {

            var query = Search(_entities.Employees, x => x.BusinessEntityID == businessEntityID);
            if (!query.Any()) {
                return new BuilderResponse { ValidationMessage = "Record Not Found; " + string.Format("Employee with _businessEntityID = '{0}' doesn't exist.",businessEntityID)}; 
            }

            var entity = query.SingleOrDefault();

            _entities.Employees.Remove(entity);
            await _entities.SaveChangesAsync();
            _logger.LogInfo(string.Format("Employee deleted with values: '{0}'", JsonConvert.SerializeObject(new EmployeeModel(entity))));

            return new BuilderResponse{ StatusCode = (int)HttpStatusCode.NoContent }; 
        }
        
        private IQueryable<Employee> Search(IQueryable<Employee> query, Expression<Func<Employee, bool>> filter) {
            return query.Where(filter);
        }
    }
}

