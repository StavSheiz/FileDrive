using FileDriveWebAPI.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileDriveWebAPI.Utils.Exceptions
{
    [Serializable]
    public class ObjectAlreadyExistsException: BaseException
    {
        public ObjectAlreadyExistsException() : base("Cannot insert - object already exists")
        {
            this.setExceptionCode(ENUMExceptionCodes.ObjectAlreadyExists);
        }
    }
}
