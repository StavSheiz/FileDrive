using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileDriveWebAPI.Utils.Authorization.Requirements
{
    public class OwnerRequirement: IAuthorizationRequirement
    {
    }
}
