using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileDriveWebAPI.Models
{
    public class Response<T>
    {
        public T Data { get; set; }
        public Exception Exception { get; set; }

        public Response() { }
        public Response(T data) 
        {
            this.Data = data;
        }

        public Response(Exception exception) 
        {
            this.Exception = exception;
        }

        public Response(T data, Exception exception) 
        {
            this.Data = data;
            this.Exception = exception;
        }
    }
}
