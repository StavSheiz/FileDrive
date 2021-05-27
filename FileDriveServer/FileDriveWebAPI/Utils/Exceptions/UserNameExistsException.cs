using FileDriveWebAPI.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace FileDriveWebAPI.Utils.Exceptions
{
    [Serializable]
    public class UserNameExistsException : BaseException
    {
        public UserNameExistsException() : base("User name already exists", ENUMExceptionCodes.UserNameExists) { }
    }
}
