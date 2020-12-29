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

        public User GetUser(string name, string password) 
        {
            return loginBL.GetUser(name, password);
        }

        protected override void Dispose(bool disposing)
        {
            loginBL.Dispose();
            base.Dispose(disposing);
        }
    }
}
