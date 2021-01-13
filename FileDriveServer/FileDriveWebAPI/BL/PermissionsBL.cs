using FileDriveWebAPI.Data;
using FileDriveWebAPI.Enums;
using FileDriveWebAPI.Models;
using FileDriveWebAPI.Utils.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileDriveWebAPI.BL
{
    public class PermissionsBL : BaseBL
    {
        public PermissionsBL(FileDriveContext context) : base(context) { }

        public List<Permission> GetAllPermissionsForEntity(int entityId) 
        {
            return this.unitOfWork.PermissionRepository.Get(permission => permission.Entity.Id == entityId).ToList();
        }

        public Permission GetPermission(int permissionId) 
        {
            return this.unitOfWork.PermissionRepository.GetByID(permissionId);
        }

        public bool AddPermission(int userId, int entityId, ENUMPermissionType permissionType) 
        {
            User user = this.unitOfWork.UserRepository.GetByID(userId);
            TreeEntity entity = this.unitOfWork.TreeRepository.GetByID(entityId);

            if (user == null || entity == null) 
            {
                throw new InvalidParametersException();
            }

            Permission permission = this.unitOfWork.PermissionRepository.Get(permission => permission.Entity.Id == entityId && permission.User.Id == userId).First();

            if (permission == null)
            {
                this.unitOfWork.PermissionRepository.Insert(new Permission { User = user, Entity = entity, PermissionType = permissionType });
            }
            else 
            {
                throw new ObjectAlreadyExistsException();
            }

            return true;
        }

        public bool UpdatePermission(int permissionId, ENUMPermissionType permissionType) 
        {
            Permission permission = this.unitOfWork.PermissionRepository.GetByID(permissionId);
            permission.PermissionType = permissionType;

            this.unitOfWork.PermissionRepository.Update(permission);

            return true;
        }

        public bool DeletePermission(int permissionId) 
        {
            this.unitOfWork.PermissionRepository.Delete(permissionId);

            return true;
        }
    }
}
