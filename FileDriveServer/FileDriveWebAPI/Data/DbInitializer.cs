using FileDriveWebAPI.Models;
using FileDriveWebAPI.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileDriveWebAPI.Data
{
    public static class DbInitializer
    {
        public static void Initialize(FileDriveContext context)
        {
            context.Database.EnsureCreated();

            // Init db
            initUsers(context);
            initTreeEntities(context);

        }

        private static void initTreeEntities(FileDriveContext context)
        {
            if (context.TreeEntities.Any())
            {
                return;
            }
        }

            private static void initUsers(FileDriveContext context) 
        {

            // If there are no users, init users data
            if (context.Users.Any())
            {
                return;
            }


            var users = new User[]
            {
                new User{ Name="Admin", UserType=Enums.ENUMUserType.Admin, Password=Crypto.Encrypt("password", "Admin") },
                new User{ Name="ShStav", UserType=Enums.ENUMUserType.Normal, Password=Crypto.Encrypt("password", "ShStav") },
                new User{ Name="ZeKoren", UserType=Enums.ENUMUserType.Normal, Password=Crypto.Encrypt("password", "ZeKoren") },
                new User{ Name="DuDonald", UserType=Enums.ENUMUserType.Normal, Password=Crypto.Encrypt("password", "DuDonald") }
            };

            foreach (User user in users)
            {
                context.Users.Add(user);
            }

            context.SaveChanges();
        }
    }
}
