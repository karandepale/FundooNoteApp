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
    public class CollabController : ControllerBase
    {
        private readonly ICollabBusiness collabBusiness;
        public CollabController(ICollabBusiness collabBusiness)
        {
            this.collabBusiness = collabBusiness;
        }



        //CREATE COLLAB:-
        [Authorize]
        [HttpPost]
        [Route("CreateCollab")]
        public IActionResult CreateCollab(CollabCreateModel model , long NoteID)
        {
            var userIdClaim = User.Claims.FirstOrDefault(u => u.Type == "UserID");
            if (userIdClaim != null && long.TryParse(userIdClaim.Value, out long userId))
            {
                var result = collabBusiness.CreateCollab(model, NoteID, userId);

                if (result != null)
                {
                    return Ok(new { success = true, message = "Collab Created Successful", data = result });
                }
                else
                {
                    return NotFound(new { success = false, message = "Collab Creation Failed", data = result });
                }
            }
            else
            {
                return Unauthorized();
            }
        }



        //GET ALL COLLABS FOR A NOTE:-
        [Authorize]
        [HttpGet]
        [Route("GetAllCollabs")]
        public IActionResult GetAllCollab(long NoteID)
        {
            var userIdClaim = User.Claims.FirstOrDefault(u => u.Type == "UserID");
            if (userIdClaim != null && long.TryParse(userIdClaim.Value, out long userId))
            {
                var result = collabBusiness.GetCollabsForANote(NoteID);

                if (result != null)
                {
                    return Ok(new { success = true, message = "Collab getting Successful", data = result });
                }
                else
                {
                    return NotFound(new { success = false, message = "Collab Not Found", data = result });
                }
            }
            else
            {
                return Unauthorized();
            }
        }



        //DELETE COLLAB:-
        [Authorize]
        [HttpDelete]
        [Route("DeleteCollab")]
        public IActionResult DeleteCollab(long CollabID)
        {
            try
            {
                collabBusiness.DeleteACollab(CollabID);
                return Ok(new { success = true, message = "Collab Deleted Successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = "Collab Deletion Failed", error = ex.Message });
            }
        }
    }
}
