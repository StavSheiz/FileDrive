using FileDriveWebAPI.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileDriveWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : Controller
    {
        [HttpGet]
        public string GetAsync()
        {
            return "ok";
        }

        [HttpGet("encrypt")]
        public string Encrypt(string salt) 
        {
            return Crypto.Encrypt("password", salt);
        }
    }
}
