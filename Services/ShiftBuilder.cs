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
    
    
    public class ShiftBuilder : IShiftBuilder {
        
        private IDbEntities _entities;
        
        private ILoggerManager _logger;
        
        public ShiftBuilder(EntitiesContext context, ILoggerManager logger) {
            _entities = context;
            _logger = logger;
        }
        
        private Expression<Func<Shift, ShiftModel>>  ProjectToModel {
            get {
                return entity => new ShiftModel(entity);
            }
        }
        
        public async Task<IQueryable<ShiftModel>> GetShifts() {
            return await Task.FromResult(_entities.Shifts.Select(ProjectToModel));
        }
        
        public IList<ExpandoObject> GetDisplayModels(List<string> propNames) {
            var models = _entities.Shifts.Select(ProjectToModel);

            var displayModels = new List<ExpandoObject>();
            foreach (var model in models)
            {
                dynamic displayModel = DynamicHelper.ConvertToExpando(model, propNames);
                displayModels.Add(displayModel);
            }

            return displayModels;
        }
        
        public async Task<BuilderResponse> GetShift_ByShiftID(byte shiftID) {
            var query = await Search(_entities.Shifts, x => x.ShiftID == shiftID).Select(ProjectToModel)?.ToListAsync();
            if (query.Any()) {
                return new BuilderResponse{ Model = query.Single() }; 
            }
            else {
                return new BuilderResponse { ValidationMessage = $"Record Not Found; Shift with shiftID = '{shiftID}' doesn't exist." }; 
            }
        }
        
        public async Task<BuilderResponse> AddShift(ShiftModel model) {


            var entity = ModelExtender.ToEntity(model);
            _entities.Shifts.Add(entity);
            await _entities.SaveChangesAsync();
            _logger.LogInfo(string.Format("Shift added with values: '{0}'", JsonConvert.SerializeObject(model)));

            return new BuilderResponse{ Model = new ShiftModel(entity) }; 
        }
        
        public async Task<BuilderResponse> UpdateShift(ShiftModel model) {

            var query = Search(_entities.Shifts, x =>  x.ShiftID == model.ShiftID);
            if (!query.Any()) {
                return new BuilderResponse { ValidationMessage = "Record Not Found; " + string.Format("Shift with _shiftID = '{0}' doesn't exist.",model.ShiftID)}; 
            }

            Shift entity = query.SingleOrDefault();
            entity = model.ToEntity(entity);
            await _entities.SaveChangesAsync();
            _logger.LogInfo(string.Format("Shift update with values: '{0}'", JsonConvert.SerializeObject(model)));

            return new BuilderResponse{ StatusCode = (int)HttpStatusCode.Accepted }; 
        }
        
        public async Task<BuilderResponse> DeleteShift(byte shiftID) {

            var query = Search(_entities.Shifts, x => x.ShiftID == shiftID);
            if (!query.Any()) {
                return new BuilderResponse { ValidationMessage = "Record Not Found; " + string.Format("Shift with _shiftID = '{0}' doesn't exist.",shiftID)}; 
            }

            var entity = query.SingleOrDefault();

            _entities.Shifts.Remove(entity);
            await _entities.SaveChangesAsync();
            _logger.LogInfo(string.Format("Shift deleted with values: '{0}'", JsonConvert.SerializeObject(new ShiftModel(entity))));

            return new BuilderResponse{ StatusCode = (int)HttpStatusCode.NoContent }; 
        }
        
        private IQueryable<Shift> Search(IQueryable<Shift> query, Expression<Func<Shift, bool>> filter) {
            return query.Where(filter);
        }
    }
}

