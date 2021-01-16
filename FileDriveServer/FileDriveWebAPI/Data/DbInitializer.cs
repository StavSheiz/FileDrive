using FileDriveWebAPI.Enums;
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

            // Init db data
            initUsers(context);
            initTreeEntities(context);
            initPermissions(context);
        }

        private static void initTreeEntities(FileDriveContext context)
        {
            if (context.TreeEntities.Any())
            {
                return;
            }

            var entities = new TreeEntity[]
            {
                new TreeEntity{ Name="Photos", ParentId=null, Owner=context.Users.First(x => x.Id == 3)},
                new TreeEntity{ Name="Documents", ParentId=null, Owner=context.Users.First(x => x.Id == 3) },
                new TreeEntity{ Name="Pdf Docs", ParentId=2, Owner=context.Users.First(x => x.Id == 3) },
                new TreeEntity{ Name="Word Docs", ParentId=2, Owner=context.Users.First(x => x.Id == 3) },
                new TreeEntity{ Name="Wedding", ParentId=1, Owner=context.Users.First(x => x.Id == 3) },
                new TreeEntity{ Name="Birthday", ParentId=1, Owner=context.Users.First(x => x.Id == 3) },
                new TreeEntity{ Name="Bar Mitzvah", ParentId=1, Owner=context.Users.First(x => x.Id == 3) },
            };

            foreach (TreeEntity entity in entities) 
            {
                context.TreeEntities.Add(entity);
            }

            context.SaveChanges();
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
                new User{ Name="Admin", UserType=ENUMUserType.Admin, Password=Crypto.Encrypt("password", "Admin") },
                new User{ Name="ShStav", UserType=ENUMUserType.Normal, Password=Crypto.Encrypt("password", "ShStav") },
                new User{ Name="ZeKoren", UserType=ENUMUserType.Normal, Password=Crypto.Encrypt("password", "ZeKoren") },
                new User{ Name="DuDonald", UserType=ENUMUserType.Normal, Password=Crypto.Encrypt("password", "DuDonald") }
            };

            foreach (User user in users)
            {
                context.Users.Add(user);
            }

            context.SaveChanges();
        }


        private static void initPermissions(FileDriveContext context) 
        {
            if (context.Permissions.Any()) 
            {
                return;
            }

            

            var Permissions = new Permission[]
            {
                new Permission { User=getUser(context, 2), Entity=getEntity(context, 1), PermissionType=ENUMPermissionType.Edit },
                new Permission { User=getUser(context, 3), Entity=getEntity(context, 1), PermissionType=ENUMPermissionType.Edit }
            };

            foreach (Permission permission in Permissions)
            {
                context.Permissions.Add(permission);
            }

            context.SaveChanges();
        }

        private static User getUser(FileDriveContext context, int id) 
        {
            return context.Users.FirstOrDefault(u => u.Id == 2);
        }

        private static TreeEntity getEntity(FileDriveContext context, int id)
        {
            return context.TreeEntities.FirstOrDefault(u => u.Id == 2);
        }

    }
}
