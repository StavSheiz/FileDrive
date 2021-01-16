using FileDriveWebAPI.Data;
using FileDriveWebAPI.Enums;
using FileDriveWebAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileDriveWebAPI.DAL
{
    public class TreeRepository : BaseRepository<TreeEntity>
    {
        public TreeRepository(FileDriveContext context) : base(context) { }
    }
}
