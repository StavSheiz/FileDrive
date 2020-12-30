using FileDriveWebAPI.Data;
using FileDriveWebAPI.Models;
using FileDriveWebAPI.Utils;
using FileDriveWebAPI.Utils.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileDriveWebAPI.BL
{
    public class LoginBL: BaseBL
    {
        public LoginBL(FileDriveContext context) : base(context) { }

        public User GetUser(string name, string password)
        {
            User user =  this.unitOfWork.UserRepository.GetUser(name, Crypto.Encrypt(password, name));

            if (user == null) 
            {
                throw new UserDoesNotExistException();
            }

            return user;
        }

        public bool AddUser(string name, string password) 
        {
            bool isUniqueName = this.unitOfWork.UserRepository.GetUser(name) == null;
            bool isValidPassword = validatePassword(password);

            if (!isUniqueName) 
            {
                throw new UserNameExistsException();
            }

            if (!isValidPassword) 
            {
                throw new InvalidPasswordException();
            }

            this.unitOfWork.UserRepository.AddUser(name, password);
            this.unitOfWork.Save();

            return true;
        }

        private bool validatePassword(string password) 
        {
            return password.Length >= 8;
        }
    }
}
