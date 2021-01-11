using FileDriveWebAPI.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileDriveWebAPI.Utils.Exceptions
{
    [Serializable]
    public class InvalidParametersException: BaseException
    {
        public InvalidParametersException() : base("One or more parameters are invalid")
        {
            this.setExceptionCode(ENUMExceptionCodes.InvalidParameters);
        }
    }
}
