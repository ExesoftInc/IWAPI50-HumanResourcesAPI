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
    [Route("Shift")]
    public class ShiftController : ControllerBase {
        
        private IShiftBuilder _builder;
        
        public ShiftController(IShiftBuilder builder) {
            _builder = builder;
        }
        
        [HttpGet("")]
        public async Task<ActionResult> GetShifts() {

            return Ok(await _builder.GetShifts());
        }
        
        [HttpGet("Display")]
        public async Task<ActionResult> GetDisplayModels() {
            //List all model properties that should be displayed
            //Here only a couple have been added as an example
            var propNames = new List<string>();
            propNames.Add(nameof(ShiftModel.ShiftID));
            propNames.Add(nameof(ShiftModel.Name));

            return Ok(await Task.FromResult(_builder.GetDisplayModels(propNames)));
        }
        
        [HttpGet("Paged")]
        public async Task<ActionResult> Paged(int pageIndex, int pageSize) {

            var models = await _builder.GetShifts();

            return Ok(models.ToPagedList(pageIndex, pageSize, 0, models.Count()));
        }
        
        [HttpGet("{shiftID}")]
        public async Task<ActionResult> GetShift_ByShiftID(byte shiftID) {

             var response = await _builder.GetShift_ByShiftID(shiftID);
            if (response.ValidationMessage != null) {
                return BadRequest(response.ValidationMessage);
            }

            return Ok(response.Model);
        }
        
        [HttpPost("")]
        public async Task<ActionResult> AddShift([FromBody]ShiftModel model) {

            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            var response = await _builder.AddShift(model);

            if (response.ValidationMessage != null) {
                return BadRequest(response.ValidationMessage);
            }

            return CreatedAtAction("GetShift_ByShiftID", new {shiftID = ((ShiftModel)response.Model).ShiftID}, response.Model);
        }
        
        [HttpPut("")]
        public async Task<ActionResult> UpdateShift([FromBody]ShiftModel model) {

            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            var response = await _builder.UpdateShift(model);

            if (response.ValidationMessage != null) {
                return BadRequest(response.ValidationMessage);
            }

            return AcceptedAtAction("GetShift_ByShiftID", new {shiftID = model.ShiftID}, model);
        }
        
        [HttpDelete("{shiftID}")]
        public async Task<ActionResult> DeleteShift(byte shiftID) {

            var response = await _builder.DeleteShift(shiftID);

            if (response.ValidationMessage != null) {
                return BadRequest(response.ValidationMessage);
            }

            return StatusCode(response.StatusCode);
        }
    }
}

