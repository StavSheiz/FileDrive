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
            return this.unitOfWork.PermissionRepository.Get(permission => permission.Entity.Id == entityId, includeProperties: "User,Entity").ToList();
        }

        public Permission GetPermission(int permissionId) 
        {
            Permission permission =  this.unitOfWork.PermissionRepository.Get(permission => permission.Id == permissionId, includeProperties: "User,Entity").FirstOrDefault();

            if (permission == null) 
            {
                throw new ObjectDoesNotExistException();
            }

            return permission;
        }

        public List<User> GetUsers() 
        {
            return this.unitOfWork.UserRepository.Get().ToList();
        }

        public bool AddPermission(int userId, int entityId, ENUMPermissionType permissionType) 
        {
            User user = this.unitOfWork.UserRepository.GetByID(userId);
            TreeEntity entity = this.unitOfWork.TreeRepository.GetByID(entityId);

            if (user == null || entity == null) 
            {
                throw new InvalidParametersException();
            }

            Permission permission = this.unitOfWork.PermissionRepository.Get(permission => permission.Entity.Id == entityId && permission.User.Id == userId).FirstOrDefault();

            if (permission == null)
            {
                this.unitOfWork.PermissionRepository.Insert(new Permission { User = user, Entity = entity, PermissionType = permissionType });
                this.unitOfWork.Save();
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

            if (permission == null) 
            {
                throw new ObjectDoesNotExistException();
            }
            permission.PermissionType = permissionType;

            this.unitOfWork.PermissionRepository.Update(permission);
            this.unitOfWork.Save();

            return true;
        }

        public bool DeletePermission(int permissionId) 
        {
            this.unitOfWork.PermissionRepository.Delete(permissionId);
            this.unitOfWork.Save();

            return true;
        }
    }
}
