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
    
    
    public class DepartmentBuilder : IDepartmentBuilder {
        
        private IDbEntities _entities;
        
        private ILoggerManager _logger;
        
        public DepartmentBuilder(EntitiesContext context, ILoggerManager logger) {
            _entities = context;
            _logger = logger;
        }
        
        private Expression<Func<Department, DepartmentModel>>  ProjectToModel {
            get {
                return entity => new DepartmentModel(entity);
            }
        }
        
        public async Task<IQueryable<DepartmentModel>> GetDepartments() {
            return await Task.FromResult(_entities.Departments.Select(ProjectToModel));
        }
        
        public IList<ExpandoObject> GetDisplayModels(List<string> propNames) {
            var models = _entities.Departments.Select(ProjectToModel);

            var displayModels = new List<ExpandoObject>();
            foreach (var model in models)
            {
                dynamic displayModel = DynamicHelper.ConvertToExpando(model, propNames);
                displayModels.Add(displayModel);
            }

            return displayModels;
        }
        
        public async Task<BuilderResponse> GetDepartment_ByDepartmentID(short departmentID) {
            var query = await Search(_entities.Departments, x => x.DepartmentID == departmentID).Select(ProjectToModel)?.ToListAsync();
            if (query.Any()) {
                return new BuilderResponse{ Model = query.Single() }; 
            }
            else {
                return new BuilderResponse { ValidationMessage = $"Record Not Found; Department with departmentID = '{departmentID}' doesn't exist." }; 
            }
        }
        
        public async Task<BuilderResponse> AddDepartment(DepartmentModel model) {


            var entity = ModelExtender.ToEntity(model);
            _entities.Departments.Add(entity);
            await _entities.SaveChangesAsync();
            _logger.LogInfo(string.Format("Department added with values: '{0}'", JsonConvert.SerializeObject(model)));

            return new BuilderResponse{ Model = new DepartmentModel(entity) }; 
        }
        
        public async Task<BuilderResponse> UpdateDepartment(DepartmentModel model) {

            var query = Search(_entities.Departments, x =>  x.DepartmentID == model.DepartmentID);
            if (!query.Any()) {
                return new BuilderResponse { ValidationMessage = "Record Not Found; " + string.Format("Department with _departmentID = '{0}' doesn't exist.",model.DepartmentID)}; 
            }

            Department entity = query.SingleOrDefault();
            entity = model.ToEntity(entity);
            await _entities.SaveChangesAsync();
            _logger.LogInfo(string.Format("Department update with values: '{0}'", JsonConvert.SerializeObject(model)));

            return new BuilderResponse{ StatusCode = (int)HttpStatusCode.Accepted }; 
        }
        
        public async Task<BuilderResponse> DeleteDepartment(short departmentID) {

            var query = Search(_entities.Departments, x => x.DepartmentID == departmentID);
            if (!query.Any()) {
                return new BuilderResponse { ValidationMessage = "Record Not Found; " + string.Format("Department with _departmentID = '{0}' doesn't exist.",departmentID)}; 
            }

            var entity = query.SingleOrDefault();

            _entities.Departments.Remove(entity);
            await _entities.SaveChangesAsync();
            _logger.LogInfo(string.Format("Department deleted with values: '{0}'", JsonConvert.SerializeObject(new DepartmentModel(entity))));

            return new BuilderResponse{ StatusCode = (int)HttpStatusCode.NoContent }; 
        }
        
        private IQueryable<Department> Search(IQueryable<Department> query, Expression<Func<Department, bool>> filter) {
            return query.Where(filter);
        }
    }
}

