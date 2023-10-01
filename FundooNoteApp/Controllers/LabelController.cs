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
    public class LabelController : ControllerBase
    {
        private readonly ILabelBusiness labelBusiness;
        private readonly IDistributedCache distributedCache;
        public LabelController(ILabelBusiness labelBusiness , IDistributedCache distributedCache)
        {
            this.labelBusiness = labelBusiness;
            this.distributedCache = distributedCache;
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
                var result = labelBusiness.CreateLabel(model, NoteID , userId);

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
        /*  [Authorize]
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
          }*/


        // GET ALL LABELS USING RADDIS CACHE:-
        [Authorize]
        [HttpGet]
        [Route("GetAllLabels")]
        public async Task<IActionResult> GetLabels(long NoteID)
        {
            var myKey = "labelList";
            var serializeLabelList = await distributedCache.GetStringAsync(myKey);
            List<LabelEntity> result;

            if (serializeLabelList != null)
            {
                result = JsonConvert.DeserializeObject<List<LabelEntity>>(serializeLabelList);
            }
            else
            {
                var userIdClaim = User.Claims.FirstOrDefault(data => data.Type == "UserID");
                if (userIdClaim != null && long.TryParse(userIdClaim.Value, out long userId))
                {
                    result = labelBusiness.GetAllLabels(NoteID);
                    serializeLabelList = JsonConvert.SerializeObject(result);

                    await distributedCache.SetStringAsync(myKey, serializeLabelList, new DistributedCacheEntryOptions
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
                return Ok(new { success = true, message = "List Of Labels getting Successful.", data = result });
            }
            else
            {
                return NotFound(new { success = false, message = "List Of Labels Not getting.", data = result });
            }

        }



        // UPDATE LABEL:-
        [Authorize]
        [HttpPut]
        [Route("UpdateLabel")]
        public IActionResult UpdateLabel(LabelUpdateModel model , long LabelID)
        {
            var userIdClaim = User.Claims.FirstOrDefault(u => u.Type == "UserID");
            if (userIdClaim != null && long.TryParse(userIdClaim.Value, out long userId))
            {
                var result = labelBusiness.UpdateLabel(model , LabelID);

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
