using CommonLayer.Model;
using RepoLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepoLayer.Interfaces
{
    public interface IUserRepo
    {
        public UserEntity UserRegistration(UserRegistrationModel model);
        public List<UserEntity> GetAllUser();
        public UserLoginResult UserLogin(UserLoginModel model);
        public string ForgotPassword(ForgotPasswordModel model);
        public bool ResetPassword(string email, string newPass, string confirmPass);
    }
}
