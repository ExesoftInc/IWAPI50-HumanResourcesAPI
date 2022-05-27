using HumanResourcesAPI.Models;
using HumanResourcesAPI.Services;
using InstantHelper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace HumanResourcesAPI.Controllers {
    
    
    // TODO: Uncomment the following line to use an API Key; change the value of the key in appSetting (X-API-Key)
    // [ApiKey()]
    [Route("JobCandidate")]
    public class JobCandidateController : ControllerBase {
        
        private IJobCandidateBuilder _builder;
        
        public JobCandidateController(IJobCandidateBuilder builder) {
            _builder = builder;
        }
        
        [HttpGet("")]
        public async Task<ActionResult> GetJobCandidates() {

            return Ok(await _builder.GetJobCandidates());
        }
        
        [HttpGet("Display")]
        public async Task<ActionResult> GetDisplayModels() {
            //List all model properties that should be displayed
            //Here only a couple have been added as an example
            var propNames = new List<string>();
            propNames.Add(nameof(JobCandidateModel.JobCandidateID));
            propNames.Add(nameof(JobCandidateModel.BusinessEntityID));

            return Ok(await Task.FromResult(_builder.GetDisplayModels(propNames)));
        }
        
        [HttpGet("Paged")]
        public async Task<ActionResult> Paged(int pageIndex, int pageSize) {

            var models = await _builder.GetJobCandidates();

            return Ok(models.ToPagedList(pageIndex, pageSize, 0, models.Count()));
        }
        
        [HttpGet("{jobCandidateID}")]
        public async Task<ActionResult> GetJobCandidate_ByJobCandidateID(int jobCandidateID) {

             var response = await _builder.GetJobCandidate_ByJobCandidateID(jobCandidateID);
            if (response.ValidationMessage != null) {
                return BadRequest(response.ValidationMessage);
            }

            return Ok(response.Model);
        }
        
        [HttpGet("GetJobCandidate_ByBusinessEntityID/{businessEntityID}")]
        public async Task<IQueryable<JobCandidateModel>> GetJobCandidate_ByBusinessEntityID(int businessEntityID) {

            return await _builder.GetJobCandidate_ByBusinessEntityID(businessEntityID);
        }
        
        [HttpPost("")]
        public async Task<ActionResult> AddJobCandidate([FromBody]JobCandidateModel model) {

            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            var response = await _builder.AddJobCandidate(model);

            if (response.ValidationMessage != null) {
                return BadRequest(response.ValidationMessage);
            }

            return CreatedAtAction("GetJobCandidate_ByJobCandidateID", new {jobCandidateID = ((JobCandidateModel)response.Model).JobCandidateID}, response.Model);
        }
        
        [HttpPut("")]
        public async Task<ActionResult> UpdateJobCandidate([FromBody]JobCandidateModel model) {

            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            var response = await _builder.UpdateJobCandidate(model);

            if (response.ValidationMessage != null) {
                return BadRequest(response.ValidationMessage);
            }

            return AcceptedAtAction("GetJobCandidate_ByJobCandidateID", new {jobCandidateID = model.JobCandidateID}, model);
        }
        
        [HttpDelete("{jobCandidateID}")]
        public async Task<ActionResult> DeleteJobCandidate(int jobCandidateID) {

            var response = await _builder.DeleteJobCandidate(jobCandidateID);

            if (response.ValidationMessage != null) {
                return BadRequest(response.ValidationMessage);
            }

            return StatusCode(response.StatusCode);
        }
    }
}

