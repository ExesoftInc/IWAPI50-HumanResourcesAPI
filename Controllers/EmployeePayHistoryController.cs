// ----------------------------------------------------------------------------------
// <copyright company="Exesoft Inc.">
//	This code was generated by Instant Web API code automation software (https://www.InstantWebAPI.com)
//	Copyright Exesoft Inc. © 2019.  All rights reserved.
// </copyright>
// ----------------------------------------------------------------------------------

using HumanResourcesAPI.Models;
using HumanResourcesAPI.Services;
using InstantHelper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Threading.Tasks;

namespace HumanResourcesAPI.Controllers {
    
    
    // Uncomment the following line to use an API Key; change the value of the key in appSetting (X-API-Key)
    // [ApiKey()]
    [Route("EmployeePayHistory")]
    public class EmployeePayHistoryController : ControllerBase {
        
        private IEmployeePayHistoryBuilder _builder;
        
        public EmployeePayHistoryController(IEmployeePayHistoryBuilder builder) {
            _builder = builder;
        }
        
        [HttpGet("")]
        public async Task<IList<EmployeePayHistoryModel>> GetEmployeePayHistories() {

            return await _builder.GetEmployeePayHistories()?.ToListAsync();
        }
        
        [HttpGet("Display")]
        public IList<ExpandoObject> GetDisplayModels() {
            //List all model properties that should be displayed
            //Here only a couple have been added as an example
            var propNames = new List<string>();
            propNames.Add(nameof(EmployeePayHistoryModel.BusinessEntityID));
            propNames.Add(nameof(EmployeePayHistoryModel.RateChangeDate));

            return _builder.GetDisplayModels(propNames);
        }
        
        [HttpGet("Paged")]
        public async Task<IPagedList<EmployeePayHistoryModel>> Paged(int pageIndex, int pageSize) {

            var models = await _builder.GetEmployeePayHistories()?.ToListAsync();

            return models.ToPagedList(pageIndex, pageSize, 0, models.Count);
        }
        
        [HttpGet("{businessEntityID}/{rateChangeDate}")]
        public async Task<ActionResult> GetEmployeePayHistory_ByBusinessEntityIDRateChangeDate(int businessEntityID, System.DateTime rateChangeDate) {

             var response = await _builder.GetEmployeePayHistory_ByBusinessEntityIDRateChangeDate(businessEntityID, rateChangeDate);
            if (response.RequestMessage != null) {
              return BadRequest(response.RequestMessage);
            }

            return Ok(response.Model);
        }
        
        [HttpGet("GetEmployeePayHistory_ByBusinessEntityID/{businessEntityID}")]
        public async Task<IList<EmployeePayHistoryModel>> GetEmployeePayHistory_ByBusinessEntityID(int businessEntityID) {

            return await _builder.GetEmployeePayHistory_ByBusinessEntityID(businessEntityID)?.ToListAsync();
        }
        
        [HttpPost("")]
        public async Task<ActionResult> AddEmployeePayHistory([FromBody]EmployeePayHistoryModel model) {

            if (!ModelState.IsValid) {
              return BadRequest(ModelState);
            }

            var response = await _builder.AddEmployeePayHistory(model);
            if (response.ErrorMessage != null) {
              return BadRequest($"Error for {nameof(AddEmployeePayHistory)}; {response.ErrorMessage}");
            }

            if (response.RequestMessage != null) {
              return BadRequest(response.RequestMessage);
            }

            return CreatedAtAction("GetEmployeePayHistory_ByBusinessEntityIDRateChangeDate", new {businessEntityID = ((EmployeePayHistoryModel)response.Model).BusinessEntityID, rateChangeDate = ((EmployeePayHistoryModel)response.Model).RateChangeDate}, response.Model);
        }
        
        [HttpPut("")]
        public async Task<ActionResult> UpdateEmployeePayHistory([FromBody]EmployeePayHistoryModel model) {

            if (!ModelState.IsValid) {
              return BadRequest(ModelState);
            }
            var response = await _builder.UpdateEmployeePayHistory(model);
            if (response.ErrorMessage != null) {
              return BadRequest($"Error for {nameof(UpdateEmployeePayHistory)}; {response.ErrorMessage}");
            }

            if (response.RequestMessage != null) {
              return BadRequest(response.RequestMessage);
            }

            return StatusCode(response.StatusCode);
        }
        
        [HttpDelete("{businessEntityID}/{rateChangeDate}")]
        public async Task<ActionResult> DeleteEmployeePayHistory(int businessEntityID, System.DateTime rateChangeDate) {

            var response = await _builder.DeleteEmployeePayHistory(businessEntityID, rateChangeDate);
            if (response.ErrorMessage != null) {
              return BadRequest($"Error for {nameof(DeleteEmployeePayHistory)}; {response.ErrorMessage}");
            }

            if (response.RequestMessage != null) {
              return BadRequest(response.RequestMessage);
            }

            return StatusCode(response.StatusCode);
        }
    }
}

