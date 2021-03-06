﻿using FileDriveWebAPI.Data;
using FileDriveWebAPI.Enums;
using FileDriveWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileDriveWebAPI.DAL
{
    public class UserRepository : BaseRepository<User>
    {
        public UserRepository(FileDriveContext context) : base(context) { }

        public User GetUser(string name, string password)
        {
            User user = dbSet.Where(user => user.Name == name && user.Password == password)
                .Select(user => new User { Name=user.Name, Id=user.Id, UserType=user.UserType})
                .FirstOrDefault();

            return user;
        }

        public User GetUser(string name) 
        {
            User user = dbSet.Where(user => user.Name == name)
                .Select(user => new User { Name = user.Name, Id = user.Id, UserType = user.UserType })
                .FirstOrDefault();

            return user;
        }

        public bool AddUser(string name, string password) 
        {
            dbSet.Add(new User { Name = name, Password = password, UserType = ENUMUserType.Normal });
            return true;
        }
    }
}
