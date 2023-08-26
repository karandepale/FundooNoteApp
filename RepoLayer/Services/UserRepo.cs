using CommonLayer.Model;
using RepoLayer.Context;
using RepoLayer.Entity;
using RepoLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace RepoLayer.Services
{
    public class UserRepo : IUserRepo
    {
        private readonly FundooContext fundooContext;
        public UserRepo(FundooContext fundooContext)
        {
            this.fundooContext = fundooContext;
        }

        //USER REGISTRATION:-
        public UserEntity UserRegistration(UserRegistrationModel model)
        {
            try
            {
                // Validate email using regular expression:-
                string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
                if (!Regex.IsMatch(model.Email, emailPattern))
                {
                    return null;
                }

                // Validate password using regular expression:-
                string passwordPattern = @"^(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$";
                if (!Regex.IsMatch(model.Password, passwordPattern))
                {
                    return null;
                }

                UserEntity userEntity = new UserEntity();
                userEntity.FirstName = model.FirstName;
                userEntity.LastName = model.LastName;
                userEntity.DateOfBirth = model.DateOfBirth;
                userEntity.Email = model.Email;
                userEntity.Password = model.Password;

                fundooContext.User.Add(userEntity);
                fundooContext.SaveChanges();

                return userEntity;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




    }
}
