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
        public abstract byte[] Convert(TreeEntity file);

        public string getNewFileName(string fileName) 
        {
            string[] seperatedName = fileName.Split('.');
            seperatedName[seperatedName.Length - 1] = this.extension;
            return String.Join('.', seperatedName);
        }

    }
}
