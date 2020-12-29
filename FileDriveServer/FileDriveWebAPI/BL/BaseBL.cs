using FileDriveWebAPI.DAL;
using FileDriveWebAPI.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileDriveWebAPI.BL
{
    public abstract class BaseBL : IDisposable
    {
        protected UnitOfWork unitOfWork;

        public BaseBL(FileDriveContext context) 
        {
            unitOfWork = new UnitOfWork(context);
        }

        public void Dispose()
        {
            unitOfWork.Dispose();
        }
    }
}
