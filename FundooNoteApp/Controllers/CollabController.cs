using BusinessLayer.Interfaces;
using BusinessLayer.Services;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using RepoLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundooNoteApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollabController : ControllerBase
    {
        private readonly ICollabBusiness collabBusiness;
        private readonly IDistributedCache distributedCache;
        public CollabController(ICollabBusiness collabBusiness , IDistributedCache distributedCache)
        {
            this.collabBusiness = collabBusiness;
            this.distributedCache = distributedCache;
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
        /* [Authorize]
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
         }*/



        // COLLAB LIST USING RADDIS CACHE:-
        [Authorize]
        [HttpGet]
        [Route("GetAllCollabs")]
        public async Task<IActionResult> GetCollabs(long NoteID)
        {
            var key = "collabs";
            var cacheData = await distributedCache.GetStringAsync(key);
            List<CollabEntity> result;

            if (cacheData != null)
            {
                result = JsonConvert.DeserializeObject<List<CollabEntity>>(cacheData);
            }
            else
            {
                var userIdClaim = User.Claims.FirstOrDefault(data => data.Type == "UserID");
                if (userIdClaim != null && long.TryParse(userIdClaim.Value, out long userId))
                {
                    result = collabBusiness.GetCollabsForANote(NoteID);
                    cacheData = JsonConvert.SerializeObject(result);

                    await distributedCache.SetStringAsync(key, cacheData, new DistributedCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10),
                        SlidingExpiration = TimeSpan.FromMinutes(2)
                    });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Invalid user ID claim" });
                }
            }
            if (result != null)
            {
                return Ok(new { success = true, message = "Collabs Getting Successfully", data = result });
            }
            else
            {
                return NotFound(new { success = false, message = "Collabs  Not Found", data = result });
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
