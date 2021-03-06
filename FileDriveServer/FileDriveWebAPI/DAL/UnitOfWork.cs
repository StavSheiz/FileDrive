﻿using FileDriveWebAPI.Data;
using FileDriveWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileDriveWebAPI.DAL
{
    public class UnitOfWork: IDisposable
    {
        private FileDriveContext context;
        private UserRepository userRepository;
        private TreeRepository treeRepository;
        private PermissionRepository permissionRepository;

        public UnitOfWork(FileDriveContext context)
        {
            this.context = context;
        }

        public UserRepository UserRepository
        {
            get
            {

                if (this.userRepository == null)
                {
                    this.userRepository = new UserRepository(context);
                }
                return userRepository;
            }
        }

        public PermissionRepository PermissionRepository 
        {
            get 
            {
                if (this.permissionRepository == null)
                {
                    this.permissionRepository = new PermissionRepository(context);
                }
                return permissionRepository;
            }
        }

        public TreeRepository TreeRepository
        {
            get
            {
                if (this.treeRepository == null)
                {
                    this.treeRepository = new TreeRepository(this.context);
                }

                return this.treeRepository;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
