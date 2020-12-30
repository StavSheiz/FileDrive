using FileDriveWebAPI.BL;
using FileDriveWebAPI.Data;
using FileDriveWebAPI.Models;
using Microsoft.AspNetCore.Http;
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

        public Response<User> GetUser(string name, string password) 
        {
            try
            {
                return new Response<User>(loginBL.GetUser(name, password));
            }
            catch (Exception ex) 
            {
                return new Response<User>(ex);
            }
            
        }

        [HttpPost]
        public Response<bool> AddUser([FromBody]string name, [FromBody]string password) 
        {
            try
            {
                return new Response<bool>(loginBL.AddUser(name, password));
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
    }
}
