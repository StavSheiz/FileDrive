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
    [ApiController]
    public class LoginController : Controller
    {
        private LoginBL loginBL;

        public LoginController(FileDriveContext context) 
        {
            loginBL = new LoginBL(context);
        }

        public Response<User> GetUser(string name, string password) 
        {
            return new Response<User>(loginBL.GetUser(name, password));
        }

        public Response<bool> AddUser(string name, string password) 
        {
            return new Response<bool>(loginBL.AddUser(name, password));
        }

        protected override void Dispose(bool disposing)
        {
            loginBL.Dispose();
            base.Dispose(disposing);
        }
    }
}
