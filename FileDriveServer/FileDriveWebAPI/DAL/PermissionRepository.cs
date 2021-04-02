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
    public class PermissionRepository : BaseRepository<Permission>
    {
        public PermissionRepository(FileDriveContext context) : base(context) { }

        public bool HasPermissions(int entityId, int userId, ENUMPermissionType permissionType) 
        {
			SqlParameter userParam = new SqlParameter("userId", userId);
			SqlParameter entityParam = new SqlParameter("entityId", entityId);
			SqlParameter permissionParam = new SqlParameter("permissionType", permissionType);

			bool hasPermissions = this.context.Permissions.FromSqlRaw(@"
					WITH treeList AS
						(SELECT tree.Id, tree.ParentId, 1 AS treeLevel
						FROM [filedriveadmin].[Tree_Entities] tree
						WHERE tree.Id=@entityId

						UNION ALL

						SELECT parents.Id, Parents.ParentId, TL.treeLevel + 1 AS treeLevel
						FROM [filedriveadmin].[Tree_Entities] parents
						INNER JOIN treeList AS TL
						ON parents.Id = TL.ParentId
						)
					SELECT treeList.Id as EntityId, perms.Id as Id, perms.UserId as UserId, perms.PermissionType as PermissionType, treeList.treeLevel FROM treeList, [filedriveadmin].[Permissions] perms
					WHERE treeList.Id=perms.EntityId and perms.UserId=@userId and PermissionType=@permissionType
					", new SqlParameter[] { userParam, entityParam, permissionParam }).AsEnumerable().Any();

			return hasPermissions;
        }
    }
}
