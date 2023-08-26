using BusinessLayer.Interfaces;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;

namespace FundooNoteApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private readonly INoteBusiness noteBusiness;

        public NoteController(INoteBusiness noteBusiness )
        {
            this.noteBusiness = noteBusiness;
        }

        //CREATE NOTE:-
        [Authorize]
        [HttpPost]
        [Route("Create_Note")]
        public IActionResult CreateNote(NoteCreateModel model)
        {
            var userIdClaim = User.Claims.FirstOrDefault(u => u.Type == "UserID"); 
            if (userIdClaim != null && long.TryParse(userIdClaim.Value, out long userId))
            {
                var result = noteBusiness.CreateNote(model, userId);

                if (result != null)
                {
                    return Ok(new { success = true, message = "Note Created Successful", data = result });
                }
                else
                {
                    return NotFound(new { success = false, message = "Note Creation Failed", data = result });
                }
            }
            else
            {
                return Unauthorized(); 
            }
        }



        // GET ALL NOTES:-
        [Authorize]
        [HttpGet]
        [Route("GetAllNotes")]
        public IActionResult GetAllNotes()
        {
            var userID = User.Claims.FirstOrDefault(data => data.Type == "UserID");
            if (userID != null && long.TryParse(userID.Value, out long userId))
            {
                var result = noteBusiness.GetAllNotes(userId);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Getting Notes Successfull", data = result });
                }
                else
                {
                    return NotFound(new { success = false, message = "Notes Note Found", data = result });
                }
            }
            else
            {
                return null;
            }
        }



        // GET NOTE BY ID:-
        [Authorize]
        [HttpGet]
        [Route("GetNoteByID")]
        public IActionResult GetNoteByID(long NoteID)
        {
            var UserID = User.Claims.FirstOrDefault(data => data.Type == "UserID");
            if (UserID != null && long.TryParse(UserID.Value, out long userId))
            {
                var result = noteBusiness.GetNoteByID(NoteID, userId);
                if(result != null)
                {
                    return Ok(new { success = true, message = "Note By ID Getting Successful", data = result });
                }
                else
                {
                    return NotFound(new { success = false, message = "Note Not Found", data = result });
                }
            }
            else
            {
                return null;
            }
        }




    }
}
