using FileDriveWebAPI.Data;
using FileDriveWebAPI.Enums;
using FileDriveWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FileDriveWebAPI.BL
{
    public class AuthorizationBL : BaseBL
    {
        public AuthorizationBL(FileDriveContext context) : base(context) { }

        public bool HasEditPermissions(ClaimsPrincipal user, TreeEntity resource)
        {
            return this.isOwner(user, resource) || this.isAdmin(user) || this.hasEditPermissionsOnResource(user, resource);
        }

        public bool HasOwnerPermissions(ClaimsPrincipal user, TreeEntity resource) 
        {
            return this.isOwner(user, resource) || this.isAdmin(user);
        }

        private bool isOwner(ClaimsPrincipal user, TreeEntity resource)
        {
            TreeEntity tree = this.unitOfWork.TreeRepository.Get(tree => tree.Id == resource.Id, includeProperties: "Owner").FirstOrDefault();
            return user.FindFirst(ClaimTypes.SerialNumber).Value == tree.Owner.Id.ToString();
        }

        private bool isAdmin(ClaimsPrincipal user) 
        {
            return user.FindFirst(ClaimTypes.Role).Value == ENUMUserType.Admin.ToString();
        }

        private bool hasEditPermissionsOnResource(ClaimsPrincipal user, TreeEntity resource) 
        {
            int userId = Convert.ToInt32(user.FindFirst(ClaimTypes.SerialNumber).Value);

            return this.unitOfWork.PermissionRepository.HasPermissions(resource.Id, userId, ENUMPermissionType.Edit) || 
                this.unitOfWork.TreeRepository.IsOwnerOfAncestor(resource.Id, userId);
        }
    }
}
