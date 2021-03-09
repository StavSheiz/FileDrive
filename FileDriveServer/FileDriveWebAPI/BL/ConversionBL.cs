using FileDriveWebAPI.BL.Conversion.Converters;
using FileDriveWebAPI.BL.Conversion.Factory;
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
    public class ConversionBL : BaseBL
    {
        public ConversionBL(FileDriveContext context) : base(context) { }

        public TreeEntity ConvertFile(ENUMConverterType type, TreeEntity file) 
        {

            if (file.File == null) 
            {
                throw new InvalidParametersException();
            }

            ConverterFactory factory = null;

            switch (type) 
            {
                case ENUMConverterType.PNGToJPG:
                    {
                        factory = new PNGToJPGFactory();
                        break;
                    }
                case ENUMConverterType.JPGToPNG: 
                    {
                        factory = new JPGToPNGFactory();
                        break;
                    }
                default: 
                    {
                        throw new InvalidParametersException();
                    }
            }

            FileConverter converter = factory.GetConverter();

            file.File = converter.Convert(file);
            file.Name = converter.getNewFileName(file.Name);

            this.unitOfWork.TreeRepository.Update(file);
            this.unitOfWork.Save();

            return file;
        }
    }
}
