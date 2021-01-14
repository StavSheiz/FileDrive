using FileDriveWebAPI.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace FileDriveWebAPI.Models
{
    [Serializable]
    public class User : ISerializable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public ENUMUserType UserType { get; set; }

        // We do not serialize password so it does not get sent to the client
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("id", this.Id);
            info.AddValue("name", this.Name);
            info.AddValue("userType", this.UserType);
        }
    }
}
