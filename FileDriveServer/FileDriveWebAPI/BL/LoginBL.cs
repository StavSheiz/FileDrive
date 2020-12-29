using FileDriveWebAPI.Data;
using FileDriveWebAPI.Models;
using FileDriveWebAPI.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileDriveWebAPI.BL
{
    public class LoginBL: BaseBL
    {
        public LoginBL(FileDriveContext context) : base(context)
        {
        }

        public User GetUser(string name, string password)
        {
            return this.unitOfWork.UserRepository.GetUser(name, Crypto.Encrypt(password, name));
        }
    }
}
