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
        public bool IsOwnerOfAncestor(int entityId, int userId) 
        {
			SqlParameter userParam = new SqlParameter("userId", userId);
			SqlParameter entityParam = new SqlParameter("entityId", entityId);

			bool hasPermissions = this.context.TreeEntities.FromSqlRaw(@"
					WITH treeList AS
						(SELECT tree.Id, tree.ParentId, tree.OwnerId, tree.Name, tree.[File], tree.Size, 1 AS treeLevel
						FROM [filedriveadmin].[Tree_Entities] tree
						WHERE tree.Id=@entityId

						UNION ALL

						SELECT parents.Id, parents.ParentId, parents.OwnerId, parents.Name, parents.[File], parents.Size, TL.treeLevel + 1 AS treeLevel
						FROM [filedriveadmin].[Tree_Entities] parents
						INNER JOIN treeList AS TL
						ON parents.Id = TL.ParentId
						)
					SELECT treeList.Id as Id, treeList.ParentId as ParentId, treeList.Name as Name, treeList.OwnerId as OwnerId, treeList.[File] as [File], treeList.Size as Size FROM treeList
					WHERE treeList.OwnerId=@userId
					", new SqlParameter[] { userParam, entityParam }).AsEnumerable().Any();

			return hasPermissions;
		}
    }
}
