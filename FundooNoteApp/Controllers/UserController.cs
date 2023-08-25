using BusinessLayer.Interfaces;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FundooNoteApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserBusiness userBusiness;
        public UserController(IUserBusiness userBusiness)
        {
            this.userBusiness = userBusiness;
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


    }
}
