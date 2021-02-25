﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FileDriveWebAPI.BL;
using FileDriveWebAPI.Data;
using FileDriveWebAPI.Models;
using FileDriveWebAPI.Utils.Authorization.Requirements;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FileDriveWebAPI.Controllers
{
    [Route("api/[controller]")]
    public class FilesController : Controller
    {
        private TreeBL bl;
        private IAuthorizationService authorizationService;

        public FilesController(FileDriveContext context, IAuthorizationService authorizationService)
        {
            this.bl = new TreeBL(context);
            this.authorizationService = authorizationService;
        }

        [HttpGet("tree")]
        [Authorize(Policy = "User")]
        public ActionResult<Response<TreeEntity[]>> GetTree()
        {
            return new Response<TreeEntity[]>(this.bl.GetTree());
        }

        [HttpPost("addFile")]
        [Authorize(Policy ="User")]
        public async Task<ActionResult<Response<TreeEntity>>> AddFile([FromForm]IFormFile uploadedFile, [FromForm]int parentId)
        {
            try
            {
                var authorizationResult = await authorizationService.AuthorizeAsync(User, this.bl.GetTreeEntity(parentId), new EditRequirement());
                if (authorizationResult.Succeeded)
                {
                    int userId = Convert.ToInt32(this.User.FindFirst(ClaimTypes.SerialNumber).Value);
                    TreeEntity newTreeEntity = this.bl.AddFile(uploadedFile, parentId, userId);
                    return new Response<TreeEntity>(newTreeEntity);
                }
                else
                {
                    return new ForbidResult();
                }
            } catch (Exception ex)
            {
                return new Response<TreeEntity>(ex);
            }
            
        }

        [HttpPost("addFolder")]
        [Authorize(Policy = "User")]
        public async Task<ActionResult<Response<TreeEntity>>> AddFolder([FromBody]AddFolderDTO folderDetails)
        {
            try
            {
                var authorizationResult = await authorizationService.AuthorizeAsync(User, this.bl.GetTreeEntity(folderDetails.parentId), new EditRequirement());
                if (authorizationResult.Succeeded)
                {
                    int userId = Convert.ToInt32(this.User.FindFirst(ClaimTypes.SerialNumber).Value);
                    TreeEntity newTreeEntity = this.bl.AddFolder(folderDetails.folderName, folderDetails.parentId, userId);
                    return new Response<TreeEntity>(newTreeEntity);
                }
                else
                {
                    return new ForbidResult();
                }
            }
            catch (Exception ex)
            {
                return new Response<TreeEntity>(ex);
            }

        }

        [HttpGet("duplicateFile")]
        [Authorize(Policy = "User")]
        public async Task<ActionResult<Response<TreeEntity>>> DuplicateFile(int entityId)
        {
            try
            {
                TreeEntity entity = this.bl.GetTreeEntity(entityId);
                var authorizationResult = await authorizationService.AuthorizeAsync(User, this.bl.GetTreeEntity(entity.ParentId ?? -1), new EditRequirement());
                if (authorizationResult.Succeeded)
                {
                    int userId = Convert.ToInt32(this.User.FindFirst(ClaimTypes.SerialNumber).Value);
                    TreeEntity newTreeEntity = this.bl.DuplicateFile(entityId, userId);
                    return new Response<TreeEntity>(newTreeEntity);
                }
                else
                {
                    return new ForbidResult();
                }
            }
            catch (Exception ex)
            {
                return new Response<TreeEntity>(ex);
            }

        }
    }

}
