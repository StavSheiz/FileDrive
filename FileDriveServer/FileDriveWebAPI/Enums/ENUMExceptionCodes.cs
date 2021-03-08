using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileDriveWebAPI.Enums
{
    public enum ENUMExceptionCodes
    {
        UserNameExists,
        InvalidPassword,
        ObjectDoesNotExist,
        InvalidParameters,
        PasswordNotMatching,
        ObjectAlreadyExists,
        InvalidEntityType,
        ConversionFailed
    }
}
