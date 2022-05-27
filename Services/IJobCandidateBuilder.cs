using HumanResourcesAPI.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HumanResourcesAPI.Services {
    
    
    public interface IJobCandidateBuilder {
        
        Task<IQueryable<JobCandidateModel>> GetJobCandidates();
        
        IList<ExpandoObject> GetDisplayModels(List<string> propNames);
        
        Task<BuilderResponse> GetJobCandidate_ByJobCandidateID(int jobCandidateID);
        
        Task<IQueryable<JobCandidateModel>> GetJobCandidate_ByBusinessEntityID(int businessEntityID);
        
        Task<BuilderResponse> AddJobCandidate(JobCandidateModel model);
        
        Task<BuilderResponse> UpdateJobCandidate(JobCandidateModel model);
        
        Task<BuilderResponse> DeleteJobCandidate(int jobCandidateID);
    }
}

