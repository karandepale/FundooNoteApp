using BusinessLayer.Interfaces;
using BusinessLayer.Services;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

    }
}
