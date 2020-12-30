﻿using FileDriveWebAPI.BL;
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

        [HttpGet("signIn")]
        public async Task<Response<bool>> SignInAsync(string name, string password) 
        {
            try
            {
                return new Response<bool>((await loginBL.SignInAsync(this.HttpContext, name, password)));
            }
            catch (Exception ex) 
            {
                return new Response<bool>(false, ex);
            }
        }

        [HttpGet("signOut")]
        public async Task<Response<bool>> SignOutAsync()
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
