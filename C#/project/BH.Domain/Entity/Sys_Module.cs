using System;
using BH.Domain.Infrastructure;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BH.Domain.Entity
{
    public class Sys_Module : FullAudited
    {
        [MaxLength(50)]
        [Column(TypeName = "varchar")]
        public string F_ParentId { get; set; }
        public int? F_Layers { get; set; }
        [MaxLength(50)]
        [Column(TypeName = "varchar")]
        public string F_EnCode { get; set; }
        [MaxLength(50)]
        [Column(TypeName = "varchar")]
        public string F_FullName { get; set; }
        [MaxLength(50)]
        [Column(TypeName = "varchar")]
        public string F_Icon { get; set; }
        [MaxLength(500)]
        [Column(TypeName = "varchar")]
        public string F_UrlAddress { get; set; }
        [MaxLength(50)]
        [Column(TypeName = "varchar")]
        public string F_Target { get; set; }
        public bool? F_IsMenu { get; set; }
        public bool? F_IsExpand { get; set; }
        public bool? F_IsPublic { get; set; }
        public bool? F_AllowEdit { get; set; }
        public bool? F_AllowDelete { get; set; }
        public int? F_SortCode { get; set; }
        public bool? F_EnabledMark { get; set; }
        [MaxLength(500)]
        [Column(TypeName = "varchar")]
        public string F_Description { get; set; }
    }
}
