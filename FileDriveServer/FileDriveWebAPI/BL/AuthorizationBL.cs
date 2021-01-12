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
            return this.IsOwner(user, resource) || this.IsAdmin(user) || this.hasEditPermissionsOnResource(user, resource);
        }

        public bool IsOwner(ClaimsPrincipal user, TreeEntity resource)
        {
            return user.FindFirst(ClaimTypes.Name).Value == resource.Owner.Id.ToString();
        }

        public bool IsAdmin(ClaimsPrincipal user) 
        {
            return user.FindFirst(ClaimTypes.Role).Value == ENUMUserType.Admin.ToString();
        }

        private bool hasEditPermissionsOnResource(ClaimsPrincipal user, TreeEntity resource) 
        {
            return true;
        }
    }
}
