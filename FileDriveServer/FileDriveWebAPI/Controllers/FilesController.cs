using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FileDriveWebAPI.BL;
using FileDriveWebAPI.Data;
using FileDriveWebAPI.Enums;
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
        private TreeBL treeBl;
        private ConversionBL conversionBl;
        private IAuthorizationService authorizationService;
        private AuthorizationBL authorizationBl;

        public FilesController(FileDriveContext context, IAuthorizationService authorizationService)
        {
            this.treeBl = new TreeBL(context);
            this.conversionBl = new ConversionBL(context);
            this.authorizationBl = new AuthorizationBL(context);
            this.authorizationService = authorizationService;
        }

        [HttpGet("tree")]
        [Authorize(Policy = "User")]
        public ActionResult<Response<TreeEntity[]>> GetTree()
        {
            TreeEntity[] tree = this.treeBl.GetTree();
            List<TreeEntity> treeWithPermissions = new List<TreeEntity>();

            foreach(TreeEntity subTree in tree)
            {
                TreeEntity shakedTree = TreePermissionsShake(subTree);
                if (shakedTree != null)
                {
                    treeWithPermissions.Add(shakedTree);
                }
            }
            return new Response<TreeEntity[]>(treeWithPermissions.ToArray());
        }

        private TreeEntity TreePermissionsShake(TreeEntity root)
        {
            bool hasPermissions = authorizationBl.HasViewPermissions(User, root);
            List<TreeEntity> childrenWithPermissions = new List<TreeEntity>();   

            foreach (TreeEntity child in root.Children)
            {
                TreeEntity childTree = TreePermissionsShake(child);
                if (childTree != null)
                {
                    hasPermissions = true;
                    childrenWithPermissions.Add(childTree);
                }
            }

            root.Children = childrenWithPermissions;
            return hasPermissions ? root : null;
        }

        [HttpPost("addFile")]
        [Authorize(Policy ="User")]
        public async Task<ActionResult<Response<TreeEntity>>> AddFile([FromForm]IFormFile uploadedFile, [FromForm]int parentId)
        {
            try
            {
                var authorizationResult = await authorizationService.AuthorizeAsync(User, this.treeBl.GetTreeEntity(parentId), new EditRequirement());
                if (authorizationResult.Succeeded)
                {
                    int userId = Convert.ToInt32(this.User.FindFirst(ClaimTypes.SerialNumber).Value);
                    TreeEntity newTreeEntity = this.treeBl.AddFile(uploadedFile, parentId, userId);
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

        [HttpDelete("deleteTreeEntity/{entityId}")]
        [Authorize(Policy = "User")]
        public async Task<ActionResult<Response<bool>>> DeleteTreeEntity(int entityId)
        {
            try
            {
                var authorizationResult = await authorizationService.AuthorizeAsync(User, this.treeBl.GetTreeEntity(entityId), new EditRequirement());
                if (authorizationResult.Succeeded)
                {
                    return new Response<bool>(this.treeBl.Delete(entityId));
                }
                else
                {
                    return new ForbidResult();
                }
            }
            catch (Exception ex)
            {
                return new Response<bool>(false);
            }

        }

        [HttpPost("addFolder")]
        [Authorize(Policy = "User")]
        public async Task<ActionResult<Response<TreeEntity>>> AddFolder([FromBody]AddFolderDTO folderDetails)
        {
            try
            {
                var authorizationResult = await authorizationService.AuthorizeAsync(User, this.treeBl.GetTreeEntity(folderDetails.parentId), new EditRequirement());
                if (authorizationResult.Succeeded)
                {
                    int userId = Convert.ToInt32(this.User.FindFirst(ClaimTypes.SerialNumber).Value);
                    TreeEntity newTreeEntity = this.treeBl.AddFolder(folderDetails.folderName, folderDetails.parentId, userId);
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

        [HttpPost("renameEntity")]
        [Authorize(Policy = "User")]
        public async Task<ActionResult<Response<bool>>> RenameEntity([FromBody] RenameEntityDTO renamedEntityDetails)
        {
            try
            {
                var authorizationResult = await authorizationService.AuthorizeAsync(User, this.treeBl.GetTreeEntity(renamedEntityDetails.entityId), new EditRequirement());
                if (authorizationResult.Succeeded)
                {
                    bool succeeded = this.treeBl.RenameTreeEntity(renamedEntityDetails.entityId, renamedEntityDetails.newName);
                    return new Response<bool>(succeeded);
                }
                else
                {
                    return new ForbidResult();
                }
            }
            catch (Exception ex)
            {
                return new Response<bool>(ex);
            }

        }

        [HttpGet("duplicateFile")]
        [Authorize(Policy = "User")]
        public async Task<ActionResult<Response<TreeEntity>>> DuplicateFile(int entityId)
        {
            try
            {
                TreeEntity entity = this.treeBl.GetTreeEntity(entityId);
                var authorizationResult = await authorizationService.AuthorizeAsync(User, this.treeBl.GetTreeEntity(entity.ParentId ?? -1), new EditRequirement());
                if (authorizationResult.Succeeded)
                {
                    int userId = Convert.ToInt32(this.User.FindFirst(ClaimTypes.SerialNumber).Value);
                    TreeEntity newTreeEntity = this.treeBl.DuplicateFile(entityId, userId);
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

        [HttpGet("convertFile")]
        [Authorize(Policy = "User")]
        public async Task<ActionResult<Response<TreeEntity>>> ConvertFile(int entityId, ENUMConverterType type)
        {
            try
            {
                TreeEntity entity = this.treeBl.GetTreeEntity(entityId);
                var authorizationResult = await authorizationService.AuthorizeAsync(User, entity, new ViewRequirement());
                if (authorizationResult.Succeeded)
                {
                    TreeEntity newTreeEntity = this.conversionBl.ConvertFile(type, entity);
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
