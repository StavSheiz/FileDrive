using System;
namespace FileDriveWebAPI.Models
{
    public class AddFolderDTO
    {
        public string folderName { get; set; }
        public int parentId { get; set; }
    }
}
