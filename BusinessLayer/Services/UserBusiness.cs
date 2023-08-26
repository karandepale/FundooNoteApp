using BusinessLayer.Interfaces;
using CommonLayer.Model;
using RepoLayer.Entity;
using RepoLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class UserBusiness : IUserBusiness
    {
        private readonly IUserRepo userRepo;
        public UserBusiness(IUserRepo userRepo)
        {
            this.userRepo = userRepo;
        }

        // USER REGISTRATION:-
        public UserEntity UserRegistration(UserRegistrationModel model)
        {
            try
            {
                return userRepo.UserRegistration(model);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        //GET ALL USERS:-
        public List<UserEntity> GetAllUser()
        {
            try
            {
                return userRepo.GetAllUser();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        //USER LOGIN:-
        public UserLoginResult UserLogin(UserLoginModel model)
        {
            try
            {
                return userRepo.UserLogin(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //FORGOT PASSWORD:-
        public string ForgotPassword(ForgotPasswordModel model)
        {
            try
            {
                return userRepo.ForgotPassword(model);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        //RESET PASSWORD:-
        public bool ResetPassword(string email, string newPass, string confirmPass)
        {
            try
            {
                return userRepo.ResetPassword(email, newPass, confirmPass);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
    }
}
