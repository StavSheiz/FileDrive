using FileDriveWebAPI.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace FileDriveWebAPI.Utils.Exceptions
{
    [Serializable]
    public class UserDoesNotExistException: BaseException
    {
        public UserDoesNotExistException(): base("No user with the given parameters exists") 
        {
            this.setExceptionCode(ENUMExceptionCodes.UserDoesNotExist);
        }
    }
}
