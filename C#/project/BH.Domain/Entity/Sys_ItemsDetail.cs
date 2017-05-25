using BH.Domain.Infrastructure;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BH.Domain.Entity
{
    public class Sys_ItemsDetail : FullAudited
    {
        [MaxLength(50)]
        [Column(TypeName = "varchar")]
        public string F_ItemId { get; set; }
        [MaxLength(50)]
        [Column(TypeName = "varchar")]
        public string F_ParentId { get; set; }
        [MaxLength(50)]
        [Column(TypeName = "varchar")]
        public string F_ItemCode { get; set; }
        [MaxLength(50)]
        [Column(TypeName = "varchar")]
        public string F_ItemName { get; set; }
        [MaxLength(500)]
        [Column(TypeName = "varchar")]
        public string F_SimpleSpelling { get; set; }
        public bool? F_IsDefault { get; set; }
        public int? F_Layers { get; set; }
        public int? F_SortCode { get; set; }
        public bool? F_EnabledMark { get; set; }
        [MaxLength(500)]
        [Column(TypeName = "varchar")]
        public string F_Description { get; set; }
        [ForeignKey("F_ItemId")]
        public virtual Sys_Items Sys_Items { get; set; }
    }
}
