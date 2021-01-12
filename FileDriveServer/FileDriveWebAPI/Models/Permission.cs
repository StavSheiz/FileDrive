using FileDriveWebAPI.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileDriveWebAPI.Models
{
    public class Permission
    {
        public int Id { get; set; }
        public User User { get; set; }
        public TreeEntity Entity { get; set; } 
        public ENUMPermissionType PermissionType { get; set; }
    }
}
