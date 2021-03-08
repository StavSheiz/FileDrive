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
    public abstract class BaseHandler<S,T> : AuthorizationHandler<S, T> where S:IAuthorizationRequirement
    {
        protected AuthorizationBL authorizationBL;

        public BaseHandler(FileDriveContext context)
        {
            this.authorizationBL = new AuthorizationBL(context);
        }
    }
}
