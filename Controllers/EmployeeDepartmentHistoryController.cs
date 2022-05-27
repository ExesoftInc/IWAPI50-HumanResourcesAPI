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
    [Route("EmployeeDepartmentHistory")]
    public class EmployeeDepartmentHistoryController : ControllerBase {
        
        private IEmployeeDepartmentHistoryBuilder _builder;
        
        public EmployeeDepartmentHistoryController(IEmployeeDepartmentHistoryBuilder builder) {
            _builder = builder;
        }
        
        [HttpGet("")]
        public async Task<ActionResult> GetEmployeeDepartmentHistories() {

            return Ok(await _builder.GetEmployeeDepartmentHistories());
        }
        
        [HttpGet("Display")]
        public async Task<ActionResult> GetDisplayModels() {
            //List all model properties that should be displayed
            //Here only a couple have been added as an example
            var propNames = new List<string>();
            propNames.Add(nameof(EmployeeDepartmentHistoryModel.BusinessEntityID));
            propNames.Add(nameof(EmployeeDepartmentHistoryModel.DepartmentID));

            return Ok(await Task.FromResult(_builder.GetDisplayModels(propNames)));
        }
        
        [HttpGet("Paged")]
        public async Task<ActionResult> Paged(int pageIndex, int pageSize) {

            var models = await _builder.GetEmployeeDepartmentHistories();

            return Ok(models.ToPagedList(pageIndex, pageSize, 0, models.Count()));
        }
        
        [HttpGet("{businessEntityID}/{startDate}/{departmentID}/{shiftID}")]
        public async Task<ActionResult> GetEmployeeDepartmentHistory_ByBusinessEntityIDStartDateDepartmentIDShiftID(int businessEntityID, System.DateTime startDate, short departmentID, byte shiftID) {

             var response = await _builder.GetEmployeeDepartmentHistory_ByBusinessEntityIDStartDateDepartmentIDShiftID(businessEntityID, startDate, departmentID, shiftID);
            if (response.ValidationMessage != null) {
                return BadRequest(response.ValidationMessage);
            }

            return Ok(response.Model);
        }
        
        [HttpGet("GetEmployeeDepartmentHistory_ByBusinessEntityID/{businessEntityID}")]
        public async Task<IQueryable<EmployeeDepartmentHistoryModel>> GetEmployeeDepartmentHistory_ByBusinessEntityID(int businessEntityID) {

            return await _builder.GetEmployeeDepartmentHistory_ByBusinessEntityID(businessEntityID);
        }
        
        [HttpGet("GetEmployeeDepartmentHistory_ByDepartmentID/{departmentID}")]
        public async Task<IQueryable<EmployeeDepartmentHistoryModel>> GetEmployeeDepartmentHistory_ByDepartmentID(short departmentID) {

            return await _builder.GetEmployeeDepartmentHistory_ByDepartmentID(departmentID);
        }
        
        [HttpGet("GetEmployeeDepartmentHistory_ByShiftID/{shiftID}")]
        public async Task<IQueryable<EmployeeDepartmentHistoryModel>> GetEmployeeDepartmentHistory_ByShiftID(byte shiftID) {

            return await _builder.GetEmployeeDepartmentHistory_ByShiftID(shiftID);
        }
        
        [HttpPost("")]
        public async Task<ActionResult> AddEmployeeDepartmentHistory([FromBody]EmployeeDepartmentHistoryModel model) {

            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            var response = await _builder.AddEmployeeDepartmentHistory(model);

            if (response.ValidationMessage != null) {
                return BadRequest(response.ValidationMessage);
            }

            return CreatedAtAction("GetEmployeeDepartmentHistory_ByBusinessEntityIDStartDateDepartmentIDShiftID", new {businessEntityID = ((EmployeeDepartmentHistoryModel)response.Model).BusinessEntityID, startDate = ((EmployeeDepartmentHistoryModel)response.Model).StartDate, departmentID = ((EmployeeDepartmentHistoryModel)response.Model).DepartmentID, shiftID = ((EmployeeDepartmentHistoryModel)response.Model).ShiftID}, response.Model);
        }
        
        [HttpPut("")]
        public async Task<ActionResult> UpdateEmployeeDepartmentHistory([FromBody]EmployeeDepartmentHistoryModel model) {

            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            var response = await _builder.UpdateEmployeeDepartmentHistory(model);

            if (response.ValidationMessage != null) {
                return BadRequest(response.ValidationMessage);
            }

            return AcceptedAtAction("GetEmployeeDepartmentHistory_ByBusinessEntityIDStartDateDepartmentIDShiftID", new {businessEntityID = model.BusinessEntityID, startDate = model.StartDate, departmentID = model.DepartmentID, shiftID = model.ShiftID}, model);
        }
        
        [HttpDelete("{businessEntityID}/{startDate}/{departmentID}/{shiftID}")]
        public async Task<ActionResult> DeleteEmployeeDepartmentHistory(int businessEntityID, System.DateTime startDate, short departmentID, byte shiftID) {

            var response = await _builder.DeleteEmployeeDepartmentHistory(businessEntityID, startDate, departmentID, shiftID);

            if (response.ValidationMessage != null) {
                return BadRequest(response.ValidationMessage);
            }

            return StatusCode(response.StatusCode);
        }
    }
}

