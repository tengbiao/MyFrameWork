using BH.Domain.Infrastructure;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BH.Domain.Entity
{
    public class Sys_Organize : FullAudited
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
        public string F_ShortName { get; set; }
        [MaxLength(50)]
        [Column(TypeName = "varchar")]
        public string F_CategoryId { get; set; }
        [MaxLength(50)]
        [Column(TypeName = "varchar")]
        public string F_ManagerId { get; set; }
        [MaxLength(50)]
        [Column(TypeName = "varchar")]
        public string F_TelePhone { get; set; }
        [MaxLength(20)]
        [Column(TypeName = "varchar")]
        public string F_MobilePhone { get; set; }
        [MaxLength(50)]
        [Column(TypeName = "varchar")]
        public string F_WeChat { get; set; }
        [MaxLength(20)]
        [Column(TypeName = "varchar")]
        public string F_Fax { get; set; }
        [MaxLength(50)]
        [Column(TypeName = "varchar")]
        public string F_Email { get; set; }
        [MaxLength(50)]
        [Column(TypeName = "varchar")]
        public string F_AreaId { get; set; }
        [MaxLength(500)]
        [Column(TypeName = "varchar")]
        public string F_Address { get; set; }
        public bool? F_AllowEdit { get; set; }
        public bool? F_AllowDelete { get; set; }
        public int? F_SortCode { get; set; }
        public bool? F_EnabledMark { get; set; }
        [MaxLength(500)]
        [Column(TypeName = "varchar")]
        public string F_Description { get; set; }
    }
}
