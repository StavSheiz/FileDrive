using FileDriveWebAPI.Data;
using FileDriveWebAPI.Models;
using FileDriveWebAPI.Utils;
using FileDriveWebAPI.Utils.Exceptions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FileDriveWebAPI.BL
{
    public class LoginBL: BaseBL
    {
        public LoginBL(FileDriveContext context) : base(context) { }

        public async Task<bool> SignInAsync(HttpContext httpContext, string name, string password) 
        {
            User user = this.GetUser(name, password);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.SerialNumber, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.UserType.ToString()),
            };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                ExpiresUtc = DateTimeOffset.UtcNow.AddHours(5),
                IsPersistent = true,
                IssuedUtc = DateTimeOffset.UtcNow
            };

            await httpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

            return true;
        }

        public async Task<bool> SignOutAsync(HttpContext httpContext) 
        {
            await httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return true;
        }

        public bool AddUser(string name, string password) 
        {
            bool isUniqueName = this.unitOfWork.UserRepository.GetUser(name) == null;
            bool isValidPassword = validatePassword(password);

            if (!isUniqueName) 
            {
                throw new UserNameExistsException();
            }

            if (!isValidPassword) 
            {
                throw new InvalidPasswordException();
            }

            this.unitOfWork.UserRepository.AddUser(name, password);
            this.unitOfWork.Save();

            return true;
        }

        private bool validatePassword(string password) 
        {
            return password.Length >= 8;
        }

        private User GetUser(string name, string password)
        {
            if (name == null || password == null) 
            {
                throw new UserDoesNotExistException();
            }

            User user = this.unitOfWork.UserRepository.GetUser(name, Crypto.Encrypt(password, name));

            if (user == null)
            {
                throw new UserDoesNotExistException();
            }

            return user;
        }
    }
}
