using CommonLayer.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepoLayer.Context;
using RepoLayer.Entity;
using RepoLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;

namespace RepoLayer.Services
{
    public class UserRepo : IUserRepo
    {
        private readonly FundooContext fundooContext;
        private readonly IConfiguration configuration;
        public UserRepo(FundooContext fundooContext , IConfiguration configuration)
        {
            this.fundooContext = fundooContext;
            this.configuration = configuration;
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


        //GENERATE JWT:-
        public string GenerateJwtToken(string Email, long UserId)
        {
            //CLAIM IS  PIECE OF INFORMATION ABOUT THE USER FOR WHICH THE TOKEN IS BEING ISSUED.
            var claims = new List<Claim>
            {
                new Claim("UserId", UserId.ToString()),
                new Claim("Email", Email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(configuration["JwtSettings:Issuer"], configuration["JwtSettings:Audience"], claims, DateTime.Now, DateTime.Now.AddHours(1), creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        //USER LOGIN 
        public UserLoginResult UserLogin(UserLoginModel model)
        {
            try
            {
                var userEntity = fundooContext.User.FirstOrDefault(u => u.Email == model.Email && u.Password == model.Password);
                if (userEntity != null)
                {
                    return new UserLoginResult
                    {
                        UserEntity = userEntity,
                        JwtToken = GenerateJwtToken(userEntity.Email, userEntity.UserID)
                    };
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }



        //FORGOT PASSWORD ( USING MSMQ ):-
        public string ForgotPassword(ForgotPasswordModel model)
        {
            try
            {
                var result = fundooContext.User.FirstOrDefault(u => u.Email == model.Email);
                if (result != null)
                {
                    var Token = GenerateJwtToken(result.Email, result.UserID);
                    MSMQ msmq = new MSMQ();
                    msmq.SendData2Queue(Token);

                    return Token;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }






    }
}
