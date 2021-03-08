using FileDriveWebAPI.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileDriveWebAPI.Utils.Exceptions
{
    [Serializable]
    public class ConversionFailedException: BaseException
    {
        public ConversionFailedException(string fromFormat, string toFormat) : base($"File conversion failed from ${fromFormat} to ${toFormat}")
        {
            this.setExceptionCode(ENUMExceptionCodes.ConversionFailed);
        }
    }
}
