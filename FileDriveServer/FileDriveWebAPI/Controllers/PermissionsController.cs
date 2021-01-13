using FileDriveWebAPI.BL;
using FileDriveWebAPI.Data;
using FileDriveWebAPI.Enums;
using FileDriveWebAPI.Models;
using FileDriveWebAPI.Utils.Authorization.Handlers;
using FileDriveWebAPI.Utils.Authorization.Requirements;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileDriveWebAPI.Controllers
{
    public class PermissionsController : Controller
    {
        private PermissionsBL permissionsBL;
        private TreeBL treeBL;
        private IAuthorizationService authorizationService;


        public PermissionsController(FileDriveContext context, IAuthorizationService authorizationService)
        {
            this.permissionsBL = new PermissionsBL(context);
            this.treeBL = new TreeBL(context);
            this.authorizationService = authorizationService;
        }

        [HttpGet("getByEntity")]
        [Authorize(Policy = "User")]
        public async Task<ActionResult<Response<List<Permission>>>> GetPermissionsAsync(int entityId)
        {
            var authorizationResult = await authorizationService.AuthorizeAsync(User, treeBL.GetTreeEntity(entityId), new OwnerRequirement());

            if (authorizationResult.Succeeded)
            {
                try
                {
                    return new Response<List<Permission>>(permissionsBL.GetAllPermissionsForEntity(entityId));
                }
                catch (Exception ex)
                {
                    return new Response<List<Permission>>(ex);
                }
            }
            else 
            {
                return new ForbidResult();
            }

        }

        [HttpPost("add")]
        [Authorize(Policy = "User")]
        public async Task<ActionResult<Response<bool>>> AddPermissionAsync(AddPermissionParameters parameters)
        {
            var authorizationResult = await authorizationService.AuthorizeAsync(User, treeBL.GetTreeEntity(parameters.entityId), new OwnerRequirement());

            if (authorizationResult.Succeeded)
            {
                try
                {
                    return new Response<bool>(permissionsBL.AddPermission(parameters.userId, parameters.entityId, parameters.permissionType));
                }
                catch (Exception ex)
                {
                    return new Response<bool>(false, ex);
                }
            }
            else
            {
                return new ForbidResult();
            }
        }

        [HttpGet("update")]
        [Authorize(Policy = "User")]
        public async Task<ActionResult<Response<bool>>> UpdatePermissionAsync(int permissionId, ENUMPermissionType permissionType)
        {
            var authorizationResult = await authorizationService.AuthorizeAsync(User, permissionsBL.GetPermission(permissionId).Entity, new OwnerRequirement());

            if (authorizationResult.Succeeded)
            {
                try
                {
                    return new Response<bool>(permissionsBL.UpdatePermission(permissionId, permissionType));
                }
                catch (Exception ex)
                {
                    return new Response<bool>(false, ex);
                }
            }
            else
            {
                return new ForbidResult();
            }
        }

        [HttpGet("delete")]
        [Authorize(Policy = "User")]
        public async Task<ActionResult<Response<bool>>> DeletePermissionAsync(int permissionId)
        {
            var authorizationResult = await authorizationService.AuthorizeAsync(User, permissionsBL.GetPermission(permissionId).Entity, new OwnerRequirement());

            if (authorizationResult.Succeeded)
            {
                try
                {
                    return new Response<bool>(permissionsBL.DeletePermission(permissionId));
                }
                catch (Exception ex)
                {
                    return new Response<bool>(false, ex);
                }
            }
            else
            {
                return new ForbidResult();
            }
        }

        public class AddPermissionParameters
        {
            public int entityId { get; set; }
            public int userId { get; set; }
            public ENUMPermissionType permissionType { get; set; }
        }

        protected override void Dispose(bool disposing)
        {
            permissionsBL.Dispose();
            treeBL.Dispose();
            base.Dispose(disposing);
        }
    }
}
