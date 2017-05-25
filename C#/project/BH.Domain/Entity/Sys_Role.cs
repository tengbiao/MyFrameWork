using BH.Domain.Infrastructure;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BH.Domain.Entity
{
    public class Sys_Role : FullAudited
    {
        [MaxLength(50)]
        [Column(TypeName = "varchar")]
        public string F_OrganizeId { get; set; }
        public int? F_Category { get; set; }
        [MaxLength(50)]
        [Column(TypeName = "varchar")]
        public string F_EnCode { get; set; }
        [MaxLength(50)]
        [Column(TypeName = "varchar")]
        public string F_FullName { get; set; }
        [MaxLength(50)]
        [Column(TypeName = "varchar")]
        public string F_Type { get; set; }
        public bool? F_AllowEdit { get; set; }
        public bool? F_AllowDelete { get; set; }
        public int? F_SortCode { get; set; }
        public bool? F_EnabledMark { get; set; }
        [MaxLength(500)]
        [Column(TypeName = "varchar")]
        public string F_Description { get; set; }

    }
}
