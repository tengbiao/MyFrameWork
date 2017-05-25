using BH.Domain.Infrastructure;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BH.Domain.Entity
{
    public class Sys_ModuleButton : FullAudited
    {
        [ForeignKey("Sys_Module"),MaxLength(50),Column(TypeName = "varchar")]
        public string F_ModuleId { get; set; }
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
        public int? F_Location { get; set; }
        [MaxLength(50)]
        [Column(TypeName = "varchar")]
        public string F_JsEvent { get; set; }
        [MaxLength(500)]
        [Column(TypeName = "varchar")]
        public string F_UrlAddress { get; set; }
        public bool? F_Split { get; set; }
        public bool? F_IsPublic { get; set; }
        public bool? F_AllowEdit { get; set; }
        public bool? F_AllowDelete { get; set; }
        public int? F_SortCode { get; set; }
        public bool? F_EnabledMark { get; set; }
        [MaxLength(500)]
        [Column(TypeName = "varchar")]
        public string F_Description { get; set; }
        public virtual Sys_Module Sys_Module { get; set; }
    }
}
