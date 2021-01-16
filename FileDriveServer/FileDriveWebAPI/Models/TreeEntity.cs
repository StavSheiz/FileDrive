using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FileDriveWebAPI.Models
{
    public class TreeEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<TreeEntity>? Children { get; set; }
        public TreeEntity? Parent { get; set; }
        public int? ParentId { get; set; }
        public User Owner { get; set; }
        public byte[]? File { get; set; }
        public int? Size { get; set; }
    }
}
