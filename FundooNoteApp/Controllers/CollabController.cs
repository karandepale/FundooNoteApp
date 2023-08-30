using BusinessLayer.Interfaces;
using BusinessLayer.Services;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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


    }
}
