/**
 * 字段，表名映射  可参考
 * http://www.cnblogs.com/libingql/p/3352058.html
 *  
 * */
using BH.Domain.Infrastructure;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BH.Domain.Entity
{
    public class Sys_Area : FullAudited
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
        public string F_SimpleSpelling { get; set; }
        public int? F_SortCode { get; set; }
        public bool? F_EnabledMark { get; set; }
        [MaxLength(500)]
        [Column(TypeName = "varchar")]
        public string F_Description { get; set; }
    }
}
