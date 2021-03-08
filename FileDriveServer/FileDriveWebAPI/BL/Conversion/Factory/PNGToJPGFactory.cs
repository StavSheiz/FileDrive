using FileDriveWebAPI.BL.Conversion.Converters;
using FileDriveWebAPI.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileDriveWebAPI.BL.Conversion.Factory
{
    public class PNGToJPGFactory : ConverterFactory
    {
        public override FileConverter GetConverter()
        {
            return new PNGToJPGConverter();
        }
    }
}
