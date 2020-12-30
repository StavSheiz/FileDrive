using FileDriveWebAPI.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace FileDriveWebAPI.Utils.Exceptions
{
    [Serializable]
    public class InvalidPasswordException: BaseException
    {
        public InvalidPasswordException() : base("Invalid password. Password must contain 8 or more characters")
        {
            this.setExceptionCode(ENUMExceptionCodes.InvalidPassword);
        }
    }
}
