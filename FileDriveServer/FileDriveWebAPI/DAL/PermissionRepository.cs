using FileDriveWebAPI.Data;
using FileDriveWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileDriveWebAPI.DAL
{
    public class PermissionRepository : BaseRepository<Permission>
    {
        public PermissionRepository(FileDriveContext context) : base(context) { }
    }
}
