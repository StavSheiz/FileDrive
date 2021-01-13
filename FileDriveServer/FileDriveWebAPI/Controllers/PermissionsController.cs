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
            try
            {
                var authorizationResult = await authorizationService.AuthorizeAsync(User, treeBL.GetTreeEntity(entityId), new OwnerRequirement());

                if (authorizationResult.Succeeded)
                {
                    return new Response<List<Permission>>(permissionsBL.GetAllPermissionsForEntity(entityId));
                }
                else
                {
                    return new ForbidResult();
                }
            }
            catch (Exception ex)
            {
                return new Response<List<Permission>>(ex);
            }
        }

        [HttpPost("add")]
        [Authorize(Policy = "User")]
        public async Task<ActionResult<Response<bool>>> AddPermissionAsync(AddPermissionParameters parameters)
        {
            try
            {
                var authorizationResult = await authorizationService.AuthorizeAsync(User, treeBL.GetTreeEntity(parameters.entityId), new OwnerRequirement());

                if (authorizationResult.Succeeded)
                {
                    return new Response<bool>(permissionsBL.AddPermission(parameters.userId, parameters.entityId, parameters.permissionType));
                }
                else
                {
                    return new ForbidResult();
                }
            }
            catch (Exception ex)
            {
                return new Response<bool>(false, ex);
            }
        }

        [HttpGet("update")]
        [Authorize(Policy = "User")]
        public async Task<ActionResult<Response<bool>>> UpdatePermissionAsync(int permissionId, ENUMPermissionType permissionType)
        {
            try
            {
                var authorizationResult = await authorizationService.AuthorizeAsync(User, permissionsBL.GetPermission(permissionId).Entity, new OwnerRequirement());

                if (authorizationResult.Succeeded)
                {
                    return new Response<bool>(permissionsBL.UpdatePermission(permissionId, permissionType));
                }
                else
                {
                    return new ForbidResult();
                }
            }
            catch (Exception ex)
            {
                return new Response<bool>(false, ex);
            }
        }

        [HttpGet("delete")]
        [Authorize(Policy = "User")]
        public async Task<ActionResult<Response<bool>>> DeletePermissionAsync(int permissionId)
        {
            try
            {
                var authorizationResult = await authorizationService.AuthorizeAsync(User, permissionsBL.GetPermission(permissionId).Entity, new OwnerRequirement());

                if (authorizationResult.Succeeded)
                {
                    return new Response<bool>(permissionsBL.DeletePermission(permissionId));
                }
                else
                {
                    return new ForbidResult();
                }
            }
            catch (Exception ex)
            {
                return new Response<bool>(false, ex);
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
