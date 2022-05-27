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
    
    
    public class JobCandidateBuilder : IJobCandidateBuilder {
        
        private IDbEntities _entities;
        
        private ILoggerManager _logger;
        
        public JobCandidateBuilder(EntitiesContext context, ILoggerManager logger) {
            _entities = context;
            _logger = logger;
        }
        
        private Expression<Func<JobCandidate, JobCandidateModel>>  ProjectToModel {
            get {
                return entity => new JobCandidateModel(entity);
            }
        }
        
        public async Task<IQueryable<JobCandidateModel>> GetJobCandidates() {
            return await Task.FromResult(_entities.JobCandidates.Select(ProjectToModel));
        }
        
        public IList<ExpandoObject> GetDisplayModels(List<string> propNames) {
            var models = _entities.JobCandidates.Select(ProjectToModel);

            var displayModels = new List<ExpandoObject>();
            foreach (var model in models)
            {
                dynamic displayModel = DynamicHelper.ConvertToExpando(model, propNames);
                displayModels.Add(displayModel);
            }

            return displayModels;
        }
        
        public async Task<BuilderResponse> GetJobCandidate_ByJobCandidateID(int jobCandidateID) {
            var query = await Search(_entities.JobCandidates, x => x.JobCandidateID == jobCandidateID).Select(ProjectToModel)?.ToListAsync();
            if (query.Any()) {
                return new BuilderResponse{ Model = query.Single() }; 
            }
            else {
                return new BuilderResponse { ValidationMessage = $"Record Not Found; JobCandidate with jobCandidateID = '{jobCandidateID}' doesn't exist." }; 
            }
        }
        
        public async Task<IQueryable<JobCandidateModel>> GetJobCandidate_ByBusinessEntityID(int businessEntityID) {

            var query = await Task.FromResult(Search(_entities.JobCandidates, x => x.BusinessEntityID == businessEntityID).Select(ProjectToModel));

            return query;
        }
        
        public async Task<BuilderResponse> AddJobCandidate(JobCandidateModel model) {

            if (model.BusinessEntityID != null) {
                var matchBusinessEntityID = _entities.Employees.Where(x => x.BusinessEntityID.Equals(model.BusinessEntityID));
                if (!matchBusinessEntityID.Any()) {
                return new BuilderResponse { ValidationMessage = $"Foreign Key Violation; " + nameof(model.BusinessEntityID) + " '{model.BusinessEntityID}' doesn't exist in the system."}; 
                }
            }


            var entity = ModelExtender.ToEntity(model);
            _entities.JobCandidates.Add(entity);
            await _entities.SaveChangesAsync();
            _logger.LogInfo(string.Format("JobCandidate added with values: '{0}'", JsonConvert.SerializeObject(model)));

            return new BuilderResponse{ Model = new JobCandidateModel(entity) }; 
        }
        
        public async Task<BuilderResponse> UpdateJobCandidate(JobCandidateModel model) {

            var query = Search(_entities.JobCandidates, x =>  x.JobCandidateID == model.JobCandidateID);
            if (!query.Any()) {
                return new BuilderResponse { ValidationMessage = "Record Not Found; " + string.Format("JobCandidate with _jobCandidateID = '{0}' doesn't exist.",model.JobCandidateID)}; 
            }

            if (model.BusinessEntityID != null) {
                var matchBusinessEntityID = _entities.Employees.Where(x => x.BusinessEntityID.Equals(model.BusinessEntityID));
                if (!matchBusinessEntityID.Any()) {
                return new BuilderResponse{ ValidationMessage = "Foreign Key Violation; " + nameof(model.BusinessEntityID) + string.Format("JobCandidate with BusinessEntityID = '{0}' doesn't exist.", model.BusinessEntityID)}; 
                }
            }

            JobCandidate entity = query.SingleOrDefault();
            entity = model.ToEntity(entity);
            await _entities.SaveChangesAsync();
            _logger.LogInfo(string.Format("JobCandidate update with values: '{0}'", JsonConvert.SerializeObject(model)));

            return new BuilderResponse{ StatusCode = (int)HttpStatusCode.Accepted }; 
        }
        
        public async Task<BuilderResponse> DeleteJobCandidate(int jobCandidateID) {

            var query = Search(_entities.JobCandidates, x => x.JobCandidateID == jobCandidateID);
            if (!query.Any()) {
                return new BuilderResponse { ValidationMessage = "Record Not Found; " + string.Format("JobCandidate with _jobCandidateID = '{0}' doesn't exist.",jobCandidateID)}; 
            }

            var entity = query.SingleOrDefault();

            _entities.JobCandidates.Remove(entity);
            await _entities.SaveChangesAsync();
            _logger.LogInfo(string.Format("JobCandidate deleted with values: '{0}'", JsonConvert.SerializeObject(new JobCandidateModel(entity))));

            return new BuilderResponse{ StatusCode = (int)HttpStatusCode.NoContent }; 
        }
        
        private IQueryable<JobCandidate> Search(IQueryable<JobCandidate> query, Expression<Func<JobCandidate, bool>> filter) {
            return query.Where(filter);
        }
    }
}

