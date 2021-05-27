using FileDriveWebAPI.Data;
using FileDriveWebAPI.Models;
using FileDriveWebAPI.Utils.Exceptions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
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

        public TreeEntity DuplicateFile(int entityId, int userId) 
        {
            TreeEntity fileToDuplicate = GetTreeEntity(entityId);
            User currentUser = this.unitOfWork.UserRepository.GetByID(userId);

            if (fileToDuplicate.File == null) 
            {
                throw new InvalidEntityTypeException();
            }

            TreeEntity newFile = new TreeEntity
            {
                Name = getNextFilename(fileToDuplicate.Name, fileToDuplicate.ParentId),
                Owner = currentUser,
                ParentId = fileToDuplicate.ParentId,
                File = fileToDuplicate.File,
                Size = fileToDuplicate.Size
            };

            this.unitOfWork.TreeRepository.Insert(newFile);
            this.unitOfWork.Save();
            return newFile;
        }



        private string getNextFilename(string filename, int? parentId)
        {
            int i = 1;

            string[] splitName = filename.Split('.');
            string file = String.Join("",splitName.Take(splitName.Length - 1)) + "{0}";
            string extension = splitName[splitName.Length - 1];

            int entityId = parentId ?? default(int);
            List<TreeEntity> siblings = getTreeEntityChildren(entityId);


            while (siblings.Exists(file => file.Name == filename))
                filename = string.Format(file, "(" + i++ + ").") + extension;

            return filename;
        }

        private List<TreeEntity> getTreeEntityChildren(int parentId) 
        {
            TreeEntity parent = this.unitOfWork.TreeRepository.Get(entity => entity.Id == parentId, includeProperties: "Children").FirstOrDefault();

            if (parent == null)
            {
                throw new ObjectDoesNotExistException();
            }

            return parent.Children;
        }
    }
}
