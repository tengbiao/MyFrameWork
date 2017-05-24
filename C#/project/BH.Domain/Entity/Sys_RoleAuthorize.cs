using BH.Domain.Infrastructure;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BH.Domain.Entity
{
    public class Sys_RoleAuthorize : CreationAudited
    {
        public int? F_ItemType { get; set; }
        [MaxLength(50)]
        [Column(TypeName = "varchar")]
        public string F_ItemId { get; set; }
        public int? F_ObjectType { get; set; }
        [MaxLength(50)]
        [Column(TypeName = "varchar")]
        public string F_ObjectId { get; set; }
        public int? F_SortCode { get; set; }
    }
}
