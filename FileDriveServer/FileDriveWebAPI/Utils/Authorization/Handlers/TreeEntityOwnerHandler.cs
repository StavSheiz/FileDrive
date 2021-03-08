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
    public class TreeEntityOwnerHandler : BaseHandler<OwnerRequirement, TreeEntity>
    {

        public TreeEntityOwnerHandler(FileDriveContext context): base(context) { }

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
