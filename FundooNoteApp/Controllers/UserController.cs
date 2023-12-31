﻿using BusinessLayer.Interfaces;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using RepoLayer.Entity;
using RepoLayer.Services;
using System.Collections.Generic;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FundooNoteApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserBusiness userBusiness;
        private readonly IDistributedCache distributedCache;
        public UserController(IUserBusiness userBusiness , IDistributedCache distributedCache)
        {
            this.userBusiness = userBusiness;
            this.distributedCache = distributedCache;
        }


        // USER REGISTRATION:-
        [HttpPost]
        [Route("Registration")]
        public IActionResult Registration(UserRegistrationModel model)
        {
            var DataResult = userBusiness.UserRegistration(model);
            if(DataResult != null)
            {
                return Ok(new {success=true,message="User Registration Successfull." , data=DataResult});
            }
            else
            {
                return BadRequest(new { success = false, message = "User Registration Failed.", data = DataResult });
            }
        }


        //GET ALL USER:-
        /*   [HttpGet]
           [Route("GetAllUser")]
           public IActionResult GetAllResult()
           {
               var result = userBusiness.GetAllUser();
               if (result != null)
               {
                   return Ok(new { success = true, message = "User List Getting Successful", data = result });
               }
               else
               {
                   return NotFound(new { success = false, message = "User List Getting Failed", data = result });

               }
           }*/


        //RADDIS CACHE FOR GET USER LIST:-
        [HttpGet]
        [Route("UserList")]
        public async Task<IActionResult> GetAllResult()
        {
            var cacheKey = "userList";
            var serializedUserList = await distributedCache.GetStringAsync(cacheKey);

            List<UserEntity> userList;

            if (serializedUserList != null)
            {
                userList = JsonConvert.DeserializeObject<List<UserEntity>>(serializedUserList);
            }
            else
            {
                userList = userBusiness.GetAllUser();

                serializedUserList = JsonConvert.SerializeObject(userList);
                await distributedCache.SetStringAsync(cacheKey, serializedUserList, new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10),
                    SlidingExpiration = TimeSpan.FromMinutes(2)
                });
            }

            if (userList != null)
            {
                return Ok(new { success = true, message = "User List Getting Successful", data = userList });
            }
            else
            {
                return NotFound(new { success = false, message = "User List Getting Failed" });
            }
        }









        // USER LOGIN:-
        [HttpPost]
        [Route("Login")]
        public IActionResult Login(UserLoginModel model)
        {
            var result = userBusiness.UserLogin(model);
            if (result != null)
            {
                return Ok(new { success = true, message = "User Login Successfull.", data = result });
            }
            else
            {
                return NotFound(new { success = false, message = "User Login Failed.", data = result });
            }
        }



        // FORGOT PASSWORD:-
        [HttpPost]
        [Route("ForgotPassword")]
        public IActionResult ForgotPass(ForgotPasswordModel model)
        {
            var result = userBusiness.ForgotPassword(model);
            if (result != null)
            {
                return Ok(new { success = true, message = "Token Send Successfull.", data = result });
            }
            else
            {
                return NotFound(new { success = false, message = "Token not Send.", data = result });
            }
        }


        //RESET PASSWORD:-
        [Authorize]
        [HttpPatch]
        [Route("ResetPassword")]
        public IActionResult ResetPassword(string newPass, string confirmPass)
        {
            var email = User.FindFirst(ClaimTypes.Email).Value;
            var result = userBusiness.ResetPassword(email, newPass, confirmPass);
            if (result != null)
            {
                return Ok(new { success = true, message = "Password Changed Successfully", data = result });
            }
            else
            {
                return NotFound(new { success = false, message = "Password not changed", data = result });
            }
        }



    }
}
