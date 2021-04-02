using System;
namespace FileDriveWebAPI.Models
{
    public class RenameEntityDTO
    {
        public string newName { get; set; }
        public int entityId { get; set; }
    }
}
