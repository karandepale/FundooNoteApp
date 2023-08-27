﻿using BusinessLayer.Interfaces;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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



        // UPDATE NOTE:-
        [Authorize]
        [HttpPut]
        [Route("UpdateNote")]
        public IActionResult UpdateNote(NoteUpdateModel model , long NoteID)
        {
            var userid = User.Claims.FirstOrDefault(data => data.Type == "UserID");
            if (userid != null && long.TryParse(userid.Value, out long userId))
            {
                var result = noteBusiness.UpdateNote(model , NoteID, userId);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Note Successfully Updated", data = result });
                }
                else
                {
                    return NotFound(new { success = false, message = "Note Not Update", data = result });
                }
            }
            else
            {
                return null;
            }
        }



        //DELETE NOTE:-
        [Authorize]
        [HttpDelete]
        [Route("DeleteNote")]
        public IActionResult NoteDelete(long NoteID)
        {
            try
            {
                var userid = User.Claims.FirstOrDefault(data => data.Type == "UserID");
                if (userid != null && long.TryParse(userid.Value, out long userId))
                {
                    noteBusiness.DeleteNote(NoteID , userId);
                    return Ok(new { success = true, message = "Note Deleted Successfully" });
                }
                else
                {
                    return null;
                }
            }
            catch (System.Exception)
            {
                return NotFound(new { success = false, message = "Note is not Deleted" });
            }
           
        }



        //SEARCH NOTE BY INPUT QUERY:-
        [Authorize]
        [HttpGet]
        [Route("SearchNote")]
        public IActionResult SearchNote(string query)
        {
            var UserID = User.Claims.FirstOrDefault(data => data.Type == "UserID");
            if (UserID != null && long.TryParse(UserID.Value, out long userId))
            {
                var result = noteBusiness.SearchNoteByQuery(query, userId);
                if(result != null)
                {
                    return Ok(new { success = true, message = "Note Searched Successfully" , data=result });
                }
                else
                {
                    return NotFound(new { success = false, message = "Note is not Deleted" , data=result });
                }
            }
            else
            {
                return null;
            }
        }



        //ARCHIVE NOTE:-
        [Authorize]
        [HttpPut]
        [Route("ArchiveNote")]
        public IActionResult Archive(long NoteID)
        {
            var UserID = User.Claims.FirstOrDefault(data => data.Type == "UserID");
            if (UserID != null && long.TryParse(UserID.Value, out long userId))
            {
                var result = noteBusiness.Archive(NoteID, userId);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Note Archive Successfully", data = result });
                }
                else
                {
                    return NotFound(new { success = false, message = "Note is not Archived", data = result });
                }
            }
            else
            {
                return null;
            }
        }



        //PIN NOTE:-
        [Authorize]
        [HttpPut]
        [Route("PinNote")]
        public IActionResult Pin(long NoteID)
        {
            var UserID = User.Claims.FirstOrDefault(data => data.Type == "UserID");
            if (UserID != null && long.TryParse(UserID.Value, out long userId))
            {
                var result = noteBusiness.Pin(NoteID, userId);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Note Pinned Successfully", data = result });
                }
                else
                {
                    return NotFound(new { success = false, message = "Note is not Pinned", data = result });
                }
            }
            else
            {
                return null;
            }
        }


    }
}
