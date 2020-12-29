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
                new User{ Id="12345678", Name="Admin", UserType=Enums.ENUMUserType.ADMIN, password=Crypto.Encrypt("password", "Admin") },
                new User{ Id="87654321", Name="ShStav", UserType=Enums.ENUMUserType.NORMAL, password=Crypto.Encrypt("password", "ShStav") },
                new User{ Id="83746582", Name="ZeKoren", UserType=Enums.ENUMUserType.NORMAL, password=Crypto.Encrypt("password", "ZeKoren") },
                new User{ Id="92837463", Name="DuDonald", UserType=Enums.ENUMUserType.NORMAL, password=Crypto.Encrypt("password", "DuDonald") }
            };

            foreach (User user in users)
            {
                context.Users.Add(user);
            }

            context.SaveChanges();
        }
    }
}
