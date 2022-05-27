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
    [Route("Employee")]
    public class EmployeeController : ControllerBase {
        
        private IEmployeeBuilder _builder;
        
        public EmployeeController(IEmployeeBuilder builder) {
            _builder = builder;
        }
        
        [HttpGet("")]
        public async Task<ActionResult> GetEmployees() {

            return Ok(await _builder.GetEmployees());
        }
        
        [HttpGet("Display")]
        public async Task<ActionResult> GetDisplayModels() {
            //List all model properties that should be displayed
            //Here only a couple have been added as an example
            var propNames = new List<string>();
            propNames.Add(nameof(EmployeeModel.BusinessEntityID));
            propNames.Add(nameof(EmployeeModel.NationalIDNumber));

            return Ok(await Task.FromResult(_builder.GetDisplayModels(propNames)));
        }
        
        [HttpGet("Paged")]
        public async Task<ActionResult> Paged(int pageIndex, int pageSize) {

            var models = await _builder.GetEmployees();

            return Ok(models.ToPagedList(pageIndex, pageSize, 0, models.Count()));
        }
        
        [HttpGet("{businessEntityID}")]
        public async Task<ActionResult> GetEmployee_ByBusinessEntityID(int businessEntityID) {

             var response = await _builder.GetEmployee_ByBusinessEntityID(businessEntityID);
            if (response.ValidationMessage != null) {
                return BadRequest(response.ValidationMessage);
            }

            return Ok(response.Model);
        }
        
        [HttpPost("")]
        public async Task<ActionResult> AddEmployee([FromBody]EmployeeModel model) {

            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            var response = await _builder.AddEmployee(model);

            if (response.ValidationMessage != null) {
                return BadRequest(response.ValidationMessage);
            }

            return CreatedAtAction("GetEmployee_ByBusinessEntityID", new {businessEntityID = ((EmployeeModel)response.Model).BusinessEntityID}, response.Model);
        }
        
        [HttpPut("")]
        public async Task<ActionResult> UpdateEmployee([FromBody]EmployeeModel model) {

            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            var response = await _builder.UpdateEmployee(model);

            if (response.ValidationMessage != null) {
                return BadRequest(response.ValidationMessage);
            }

            return AcceptedAtAction("GetEmployee_ByBusinessEntityID", new {businessEntityID = model.BusinessEntityID}, model);
        }
        
        [HttpDelete("{businessEntityID}")]
        public async Task<ActionResult> DeleteEmployee(int businessEntityID) {

            var response = await _builder.DeleteEmployee(businessEntityID);

            if (response.ValidationMessage != null) {
                return BadRequest(response.ValidationMessage);
            }

            return StatusCode(response.StatusCode);
        }
    }
}

