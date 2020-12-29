using FileDriveWebAPI.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileDriveWebAPI.Models
{
    public class User
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public ENUMUserType UserType { get; set; }
    }
}
