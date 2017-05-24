using BH.Domain.Infrastructure;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BH.Domain.Entity
{
    public class Sys_DbBackup : FullAudited
    {
        [MaxLength(50)]
        [Column(TypeName = "varchar")]
        public string F_BackupType { get; set; }
        [MaxLength(50)]
        [Column(TypeName = "varchar")]
        public string F_DbName { get; set; }
        [MaxLength(50)]
        [Column(TypeName = "varchar")]
        public string F_FileName { get; set; }
        [MaxLength(50)]
        [Column(TypeName = "varchar")]
        public string F_FileSize { get; set; }
        [MaxLength(500)]
        [Column(TypeName = "varchar")]
        public string F_FilePath { get; set; }
        public DateTime? F_BackupTime { get; set; }
        public int? F_SortCode { get; set; }
        public bool? F_EnabledMark { get; set; }
        [MaxLength(500)]
        [Column(TypeName = "varchar")]
        public string F_Description { get; set; }
    }
}
