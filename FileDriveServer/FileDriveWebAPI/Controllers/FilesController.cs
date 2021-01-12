using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileDriveWebAPI.BL;
using FileDriveWebAPI.Data;
using FileDriveWebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FileDriveWebAPI.Controllers
{
    [Route("api/[controller]")]
    public class FilesController : Controller
    {
        private TreeBL bl;

        public FilesController(FileDriveContext context)
        {
            this.bl = new TreeBL(context);
        }

        [HttpGet("tree")]
        [Authorize(Policy = "User")]
        public Response<TreeEntity[]> GetTree()
        {
            return new Response<TreeEntity[]>(this.bl.GetTree());
        }
    }
}
