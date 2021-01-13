using FileDriveWebAPI.BL;
using FileDriveWebAPI.Data;
using FileDriveWebAPI.Models;
using FileDriveWebAPI.Utils.Authorization.Requirements;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileDriveWebAPI.Utils.Authorization.Handlers
{
    public class PermissionsEditHandler : AuthorizationHandler<OwnerRequirement, TreeEntity>
    {
        private AuthorizationBL authorizationBL;

        public PermissionsEditHandler(FileDriveContext context)
        {
            this.authorizationBL = new AuthorizationBL(context);
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OwnerRequirement requirement, TreeEntity resource)
        {
            if (this.authorizationBL.HasOwnerPermissions(context.User, resource))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
