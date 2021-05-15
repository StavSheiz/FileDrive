using FileDriveWebAPI.BL.Conversion.Converters;
using FileDriveWebAPI.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileDriveWebAPI.BL.Conversion.Factory
{
    public static class ConverterFactory
    {
        public static FileConverter GetConverter(ENUMConverterType converterType)
        {
            switch(converterType)
            {
                case (ENUMConverterType.JPGToPNG):
                    return new JPGToPNGConverter();
                case (ENUMConverterType.PNGToJPG):
                    return new PNGToJPGConverter();
                default:
                    return null;
            }
        }
    }
}
