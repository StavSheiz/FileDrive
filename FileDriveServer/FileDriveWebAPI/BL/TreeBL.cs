using FileDriveWebAPI.Data;
using FileDriveWebAPI.Models;
using FileDriveWebAPI.Utils;
using FileDriveWebAPI.Utils.Exceptions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FileDriveWebAPI.BL
{
    public class TreeBL : BaseBL
    {
        public TreeBL(FileDriveContext context) : base(context) { }

        public TreeEntity[] GetTree()
        {
            return this.unitOfWork.TreeRepository.Get(x => x.ParentId == null, includeProperties: "Children,Owner").ToArray();
        }

        public TreeEntity GetTreeEntity(int entityId)
        {
            TreeEntity entity = this.unitOfWork.TreeRepository.GetByID(entityId);

            if (entity == null) 
            {
                throw new ObjectDoesNotExistException();
            }

            return entity;
        }
    }
}
