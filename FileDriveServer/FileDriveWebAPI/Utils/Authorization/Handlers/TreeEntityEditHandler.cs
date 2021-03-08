using FileDriveWebAPI.BL;
using FileDriveWebAPI.Data;
using FileDriveWebAPI.Models;
using FileDriveWebAPI.Utils.Authorization.Requirements;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FileDriveWebAPI.Utils.Authorization.Handlers
{
    public class TreeEntityEditHandler : BaseHandler<EditRequirement, TreeEntity>
    {

        public TreeEntityEditHandler(FileDriveContext context) : base(context) { }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, EditRequirement requirement,
           TreeEntity resource)
        {
            if(this.authorizationBL.HasEditPermissions(context.User, resource)) 
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
