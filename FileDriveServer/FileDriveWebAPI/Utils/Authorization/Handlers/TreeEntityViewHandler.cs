using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileDriveWebAPI.BL;
using FileDriveWebAPI.Data;
using FileDriveWebAPI.Models;
using FileDriveWebAPI.Utils.Authorization.Requirements;
using Microsoft.AspNetCore.Authorization;

namespace FileDriveWebAPI.Utils.Authorization.Handlers
{
    public class TreeEntityViewHandler : BaseHandler<ViewRequirement, TreeEntity>
    {
        public TreeEntityViewHandler(FileDriveContext context) : base(context) { }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ViewRequirement requirement, TreeEntity resource)
        {
            if (this.authorizationBL.HasViewPermissions(context.User, resource))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
