using FileDriveWebAPI.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileDriveWebAPI.Utils.Exceptions
{
    [Serializable]
    public class PasswordNotMatchingException: BaseException
    {
        public PasswordNotMatchingException() : base("Password does not match the confirmation password", ENUMExceptionCodes.PasswordNotMatching) { }
    }
}
