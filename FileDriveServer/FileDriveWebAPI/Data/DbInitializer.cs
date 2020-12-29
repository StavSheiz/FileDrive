using FileDriveWebAPI.Models;
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

            // If there are no users, init db data
            if (context.Users.Any())
            {
                return;   // DB has been seeded
            }

            // Init db
            initUsers(context);

        }

        private static void initUsers(FileDriveContext context) 
        {
            var users = new User[]
            {
                new User{ Id="12345678", Name="Admin", UserType=Enums.ENUMUserType.ADMIN },
                new User{ Id="87654321", Name="ShStav", UserType=Enums.ENUMUserType.NORMAL },
                new User{ Id="83746582", Name="ZeKoren", UserType=Enums.ENUMUserType.NORMAL },
                new User{ Id="92837463", Name="DuDonald", UserType=Enums.ENUMUserType.NORMAL }
            };

            foreach (User user in users)
            {
                context.Users.Add(user);
            }
        }
    }
}
