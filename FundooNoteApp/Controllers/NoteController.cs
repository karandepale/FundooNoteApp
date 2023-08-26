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


    }
}
