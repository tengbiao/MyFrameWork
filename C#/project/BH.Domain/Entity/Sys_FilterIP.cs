using BH.Domain.Infrastructure;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BH.Domain.Entity
{
    public class Sys_FilterIP : FullAudited
    {
        public bool? F_Type { get; set; }
        [MaxLength(50)]
        [Column(TypeName = "varchar")]
        public string F_StartIP { get; set; }
        [MaxLength(50)]
        [Column(TypeName = "varchar")]
        public string F_EndIP { get; set; }
        public int? F_SortCode { get; set; }
        public bool? F_EnabledMark { get; set; }
        [MaxLength(500)]
        [Column(TypeName = "varchar")]
        public string F_Description { get; set; }
    }
}
