using FileDriveWebAPI.BL.Conversion.Converters;
using FileDriveWebAPI.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileDriveWebAPI.BL.Conversion.Factory
{
    public abstract class ConverterFactory
    {
        public abstract FileConverter GetConverter();
    }
}
