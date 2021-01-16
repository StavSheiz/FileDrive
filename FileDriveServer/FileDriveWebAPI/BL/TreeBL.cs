using FileDriveWebAPI.Data;
using FileDriveWebAPI.Models;
using FileDriveWebAPI.Utils.Exceptions;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Linq;

namespace FileDriveWebAPI.BL
{
    public class TreeBL : BaseBL
    {
        public TreeBL(FileDriveContext context) : base(context) { }

        public TreeEntity[] GetTree()
        {
            return this.unitOfWork.TreeRepository.Get(includeProperties: "Children,Owner").Where(x => x.ParentId == null).ToArray();
        }

        public TreeEntity GetTreeEntity(int entityId)
        {
            TreeEntity entity = this.unitOfWork.TreeRepository.Get(entity => entity.Id == entityId, includeProperties: "Owner").FirstOrDefault();

            if (entity == null) 
            {
                throw new ObjectDoesNotExistException();
            }

            return entity;
        }

        public TreeEntity AddFile(IFormFile file, int parentId, int userId)
        {
            if (parentId == 0 || file == null)
            {
                throw new InvalidParametersException();
            }
            byte[] fileBytes;
            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                fileBytes = ms.ToArray();                
            }
            User currentUser = this.unitOfWork.UserRepository.GetByID(userId);

            TreeEntity newTreeEntity = new TreeEntity()
            {
                Name = file.FileName,
                File = fileBytes,
                Owner = currentUser,
                ParentId = parentId,
                Size = (int)file.Length
            };

            this.unitOfWork.TreeRepository.Insert(newTreeEntity);
            this.unitOfWork.Save();
            return newTreeEntity;
        }

        public TreeEntity AddFolder(string folderName, int parentId, int userId)
        {
            if (folderName.Length == 0 || parentId == 0)
            {
                throw new InvalidParametersException();
            }

            User currentUser = this.unitOfWork.UserRepository.GetByID(userId);

            TreeEntity newTreeEntity = new TreeEntity()
            {
                Name = folderName,
                Owner = currentUser,
                ParentId = parentId
            };

            this.unitOfWork.TreeRepository.Insert(newTreeEntity);
            this.unitOfWork.Save();
            return newTreeEntity;
        }
    }
}
