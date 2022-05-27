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
    [Route("Department")]
    public class DepartmentController : ControllerBase {
        
        private IDepartmentBuilder _builder;
        
        public DepartmentController(IDepartmentBuilder builder) {
            _builder = builder;
        }
        
        [HttpGet("")]
        public async Task<ActionResult> GetDepartments() {

            return Ok(await _builder.GetDepartments());
        }
        
        [HttpGet("Display")]
        public async Task<ActionResult> GetDisplayModels() {
            //List all model properties that should be displayed
            //Here only a couple have been added as an example
            var propNames = new List<string>();
            propNames.Add(nameof(DepartmentModel.DepartmentID));
            propNames.Add(nameof(DepartmentModel.Name));

            return Ok(await Task.FromResult(_builder.GetDisplayModels(propNames)));
        }
        
        [HttpGet("Paged")]
        public async Task<ActionResult> Paged(int pageIndex, int pageSize) {

            var models = await _builder.GetDepartments();

            return Ok(models.ToPagedList(pageIndex, pageSize, 0, models.Count()));
        }
        
        [HttpGet("{departmentID}")]
        public async Task<ActionResult> GetDepartment_ByDepartmentID(short departmentID) {

             var response = await _builder.GetDepartment_ByDepartmentID(departmentID);
            if (response.ValidationMessage != null) {
                return BadRequest(response.ValidationMessage);
            }

            return Ok(response.Model);
        }
        
        [HttpPost("")]
        public async Task<ActionResult> AddDepartment([FromBody]DepartmentModel model) {

            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            var response = await _builder.AddDepartment(model);

            if (response.ValidationMessage != null) {
                return BadRequest(response.ValidationMessage);
            }

            return CreatedAtAction("GetDepartment_ByDepartmentID", new {departmentID = ((DepartmentModel)response.Model).DepartmentID}, response.Model);
        }
        
        [HttpPut("")]
        public async Task<ActionResult> UpdateDepartment([FromBody]DepartmentModel model) {

            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            var response = await _builder.UpdateDepartment(model);

            if (response.ValidationMessage != null) {
                return BadRequest(response.ValidationMessage);
            }

            return AcceptedAtAction("GetDepartment_ByDepartmentID", new {departmentID = model.DepartmentID}, model);
        }
        
        [HttpDelete("{departmentID}")]
        public async Task<ActionResult> DeleteDepartment(short departmentID) {

            var response = await _builder.DeleteDepartment(departmentID);

            if (response.ValidationMessage != null) {
                return BadRequest(response.ValidationMessage);
            }

            return StatusCode(response.StatusCode);
        }
    }
}

