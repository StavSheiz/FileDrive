using FileDriveWebAPI.BL;
using FileDriveWebAPI.Data;
using FileDriveWebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileDriveWebAPI.Controllers
{
    [Route("api/[controller]")]
    public class LoginController : Controller
    {
        private LoginBL loginBL;

        public LoginController(FileDriveContext context) 
        {
            loginBL = new LoginBL(context);
        }

        [HttpGet("signIn")]
        public async Task<ActionResult<Response<User>>> SignInAsync(string name, string password) 
        {
            try
            {
                return new Response<User>((await loginBL.SignInAsync(this.HttpContext, name, password)));
            }
            catch (Exception ex) 
            {
                return new Response<User>(ex);
            }
        }

        [HttpGet("signOut")]
        public async Task<ActionResult<Response<bool>>> SignOutAsync()
        {
            try
            {
                return new Response<bool>((await loginBL.SignOutAsync(this.HttpContext)));
            }
            catch (Exception ex)
            {
                return new Response<bool>(ex);
            }
        }

        [HttpPost]
        public ActionResult<Response<bool>> AddUser([FromBody] AddUserParameters parameters) 
        {
            try
            {
                return new Response<bool>(loginBL.AddUser(parameters.name, parameters.password, parameters.confirmPassword));
            }
            catch (Exception ex) 
            {
                return new Response<bool>(false, ex);
            }
        }

        protected override void Dispose(bool disposing)
        {
            loginBL.Dispose();
            base.Dispose(disposing);
        }

        public class AddUserParameters 
        {
            public string name;
            public string password;
            public string confirmPassword;
        }
    }
}
