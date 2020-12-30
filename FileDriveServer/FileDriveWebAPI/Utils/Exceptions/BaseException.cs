using FileDriveWebAPI.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace FileDriveWebAPI.Utils.Exceptions
{
    [Serializable]
    public class BaseException: Exception
    {
        private ENUMExceptionCodes exceptionCode;
        public ENUMExceptionCodes ExceptionCode { get { return this.exceptionCode; } }

        public BaseException() : base() { }
        public BaseException(string message) : base(message) { }

        protected void setExceptionCode(ENUMExceptionCodes code) 
        {
            this.exceptionCode = code;
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("message", this.Message);
            info.AddValue("exceptionCode", this.ExceptionCode);
        }
    }
}
