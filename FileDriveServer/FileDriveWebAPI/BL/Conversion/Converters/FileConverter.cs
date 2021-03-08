using FileDriveWebAPI.Enums;
using FileDriveWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileDriveWebAPI.BL.Conversion.Converters
{
    public abstract class FileConverter
    {
        private ENUMConverterType type;
        private string extension;

        public FileConverter(ENUMConverterType type, string extension) 
        {
            this.type = type;
            this.extension = extension;
        }
        public ENUMConverterType Type { get { return type; } }
        public abstract TreeEntity Convert(TreeEntity file);

        protected TreeEntity GetNewFile(TreeEntity file, byte[] newFile) 
        {
            return new TreeEntity { 
                File = newFile, 
                Name = getNewFileName(file.Name), 
                Id = file.Id, Owner = file.Owner, 
                Children = file.Children, 
                Parent = file.Parent, 
                ParentId = file.ParentId, 
                Size = newFile.Length 
            };
        }

        private string getNewFileName(string fileName) 
        {
            string[] seperatedName = fileName.Split('.');
            seperatedName[seperatedName.Length - 1] = this.extension;
            return String.Join('.', seperatedName);
        }

    }
}
