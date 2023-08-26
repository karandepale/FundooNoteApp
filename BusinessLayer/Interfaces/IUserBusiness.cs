using CommonLayer.Model;
using RepoLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
    public interface IUserBusiness
    {
        public UserEntity UserRegistration(UserRegistrationModel model);
        public UserLoginResult UserLogin(UserLoginModel model);
        public string ForgotPassword(ForgotPasswordModel model);
    }
}
