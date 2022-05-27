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
    [Route("EmployeePayHistory")]
    public class EmployeePayHistoryController : ControllerBase {
        
        private IEmployeePayHistoryBuilder _builder;
        
        public EmployeePayHistoryController(IEmployeePayHistoryBuilder builder) {
            _builder = builder;
        }
        
        [HttpGet("")]
        public async Task<ActionResult> GetEmployeePayHistories() {

            return Ok(await _builder.GetEmployeePayHistories());
        }
        
        [HttpGet("Display")]
        public async Task<ActionResult> GetDisplayModels() {
            //List all model properties that should be displayed
            //Here only a couple have been added as an example
            var propNames = new List<string>();
            propNames.Add(nameof(EmployeePayHistoryModel.BusinessEntityID));
            propNames.Add(nameof(EmployeePayHistoryModel.RateChangeDate));

            return Ok(await Task.FromResult(_builder.GetDisplayModels(propNames)));
        }
        
        [HttpGet("Paged")]
        public async Task<ActionResult> Paged(int pageIndex, int pageSize) {

            var models = await _builder.GetEmployeePayHistories();

            return Ok(models.ToPagedList(pageIndex, pageSize, 0, models.Count()));
        }
        
        [HttpGet("{businessEntityID}/{rateChangeDate}")]
        public async Task<ActionResult> GetEmployeePayHistory_ByBusinessEntityIDRateChangeDate(int businessEntityID, System.DateTime rateChangeDate) {

             var response = await _builder.GetEmployeePayHistory_ByBusinessEntityIDRateChangeDate(businessEntityID, rateChangeDate);
            if (response.ValidationMessage != null) {
                return BadRequest(response.ValidationMessage);
            }

            return Ok(response.Model);
        }
        
        [HttpGet("GetEmployeePayHistory_ByBusinessEntityID/{businessEntityID}")]
        public async Task<IQueryable<EmployeePayHistoryModel>> GetEmployeePayHistory_ByBusinessEntityID(int businessEntityID) {

            return await _builder.GetEmployeePayHistory_ByBusinessEntityID(businessEntityID);
        }
        
        [HttpPost("")]
        public async Task<ActionResult> AddEmployeePayHistory([FromBody]EmployeePayHistoryModel model) {

            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            var response = await _builder.AddEmployeePayHistory(model);

            if (response.ValidationMessage != null) {
                return BadRequest(response.ValidationMessage);
            }

            return CreatedAtAction("GetEmployeePayHistory_ByBusinessEntityIDRateChangeDate", new {businessEntityID = ((EmployeePayHistoryModel)response.Model).BusinessEntityID, rateChangeDate = ((EmployeePayHistoryModel)response.Model).RateChangeDate}, response.Model);
        }
        
        [HttpPut("")]
        public async Task<ActionResult> UpdateEmployeePayHistory([FromBody]EmployeePayHistoryModel model) {

            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            var response = await _builder.UpdateEmployeePayHistory(model);

            if (response.ValidationMessage != null) {
                return BadRequest(response.ValidationMessage);
            }

            return AcceptedAtAction("GetEmployeePayHistory_ByBusinessEntityIDRateChangeDate", new {businessEntityID = model.BusinessEntityID, rateChangeDate = model.RateChangeDate}, model);
        }
        
        [HttpDelete("{businessEntityID}/{rateChangeDate}")]
        public async Task<ActionResult> DeleteEmployeePayHistory(int businessEntityID, System.DateTime rateChangeDate) {

            var response = await _builder.DeleteEmployeePayHistory(businessEntityID, rateChangeDate);

            if (response.ValidationMessage != null) {
                return BadRequest(response.ValidationMessage);
            }

            return StatusCode(response.StatusCode);
        }
    }
}

