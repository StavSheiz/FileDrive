using FileDriveWebAPI.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileDriveWebAPI.Utils.Exceptions
{
    [Serializable]
    public class InvalidEntityTypeException : BaseException
    {
        public InvalidEntityTypeException() : base("Entity is of the wrong type", ENUMExceptionCodes.InvalidEntityType) { }
    }
}
