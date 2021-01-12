using FileDriveWebAPI.BL;
using FileDriveWebAPI.Data;
using FileDriveWebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FileDriveWebAPI.Utils.Authorization
{
    public class TreeEntityEditHandler : AuthorizationHandler<EditRequirement, TreeEntity>
    {
        private AuthorizationBL authorizationBL;

        public TreeEntityEditHandler(FileDriveContext context) 
        {
            this.authorizationBL = new AuthorizationBL(context);
        }

        protected override Task HandleRequirementAsync(
           AuthorizationHandlerContext context,
           EditRequirement requirement,
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
