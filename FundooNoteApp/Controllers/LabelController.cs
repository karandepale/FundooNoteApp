using BusinessLayer.Interfaces;
using BusinessLayer.Services;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace FundooNoteApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LabelController : ControllerBase
    {
        private readonly ILabelBusiness labelBusiness;
        public LabelController(ILabelBusiness labelBusiness)
        {
            this.labelBusiness = labelBusiness;
        }


        // CREATE LABEL:-
        [Authorize]
        [HttpPost]
        [Route("CreateLabel")]
        public IActionResult CreateLabel(LabelCreateModel model, long NoteID)
        {
            var userIdClaim = User.Claims.FirstOrDefault(u => u.Type == "UserID");
            if (userIdClaim != null && long.TryParse(userIdClaim.Value, out long userId))
            {
                var result = labelBusiness.CreateLabel(model, NoteID);

                if (result != null)
                {
                    return Ok(new { success = true, message = "Label Created Successful", data = result });
                }
                else
                {
                    return NotFound(new { success = false, message = "Label Creation Failed", data = result });
                }
            }
            else
            {
                return Unauthorized();
            }
        }



        // GET ALL LABELS:-
        [Authorize]
        [HttpGet]
        [Route("GetAllLabels")]
        public IActionResult GetAllLabel(long NoteID)
        {
            var userIdClaim = User.Claims.FirstOrDefault(u => u.Type == "UserID");
            if (userIdClaim != null && long.TryParse(userIdClaim.Value, out long userId))
            {
                var result = labelBusiness.GetAllLabels(NoteID);

                if (result != null)
                {
                    return Ok(new { success = true, message = "Label Getting Successful", data = result });
                }
                else
                {
                    return NotFound(new { success = false, message = "Unable to get Labels", data = result });
                }
            }
            else
            {
                return Unauthorized();
            }
        }



        // UPDATE LABEL:-
        [Authorize]
        [HttpPut]
        [Route("UpdateLabel")]
        public IActionResult UpdateLabel(LabelUpdateModel model , long NoteID)
        {
            var userIdClaim = User.Claims.FirstOrDefault(u => u.Type == "UserID");
            if (userIdClaim != null && long.TryParse(userIdClaim.Value, out long userId))
            {
                var result = labelBusiness.UpdateLabel(model , NoteID);

                if (result != null)
                {
                    return Ok(new { success = true, message = "Label Successfully updated", data = result });
                }
                else
                {
                    return NotFound(new { success = false, message = "Unable to update Label", data = result });
                }
            }
            else
            {
                return Unauthorized();
            }
        }



        // DELETE LABEL:-
        [HttpDelete]
        [Route("DeleteLabel")]
        public IActionResult DeleteLabel(long LabelID)
        {
            try
            {
                labelBusiness.DeleteLabel(LabelID);
                return Ok(new { success = true, message = "Label Deleted Successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = "Label Deletion Failed", error = ex.Message });
            }
        }
    }
}
