using FileDriveWebAPI.Data;
using FileDriveWebAPI.Enums;
using FileDriveWebAPI.Models;
using Microsoft.Data.SqlClient;
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

        public int[] GetChildrenIds(int rootId)
        {
            SqlParameter entityParam = new SqlParameter("rootId", rootId);

			int[] entitiesIds = this.context.TreeEntities.FromSqlRaw(@"
					WITH subtree AS
						(SELECT tree.*
						FROM [dbo].[Tree_Entities] tree
						WHERE tree.Id=@rootId

						UNION ALL

						SELECT children.*
						FROM [dbo].[Tree_Entities] children
						INNER JOIN subtree AS TL
						ON children.parentId = TL.Id
						)
			            select * from subtree
					", new SqlParameter[] { entityParam }).AsEnumerable().Select(x => x.Id).ToArray();
            return entitiesIds;
        }
    }
}
