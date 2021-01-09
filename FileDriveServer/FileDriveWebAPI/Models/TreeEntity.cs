using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FileDriveWebAPI.Models
{
    public class TreeEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //public byte[] File { get; set; }
        public List<TreeEntity>? Children { get; set; }
        public TreeEntity? parent { get; set; }
        public int? ParentId { get; set; }
        //[NotMapped]
        //public bool IsFile
        //{
        //    get
        //    {
        //        return this.File != null;
        //    }
        //}

        //[NotMapped]
        //public bool IsDirectory
        //{
        //    get
        //    {
        //        return this.File == null;
        //    }
        //}
    }
}
