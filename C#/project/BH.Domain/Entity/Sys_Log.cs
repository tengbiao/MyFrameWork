using BH.Domain.Infrastructure;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BH.Domain.Entity
{
    public class Sys_Log : CreationAudited
    {
        public DateTime? F_Date { get; set; }
        [MaxLength(50)]
        [Column(TypeName = "varchar")]
        public string F_Account { get; set; }
        [MaxLength(50)]
        [Column(TypeName = "varchar")]
        public string F_NickName { get; set; }
        [MaxLength(50)]
        [Column(TypeName = "varchar")]
        public string F_Type { get; set; }
        [MaxLength(50)]
        [Column(TypeName = "varchar")]
        public string F_IPAddress { get; set; }
        [MaxLength(50)]
        [Column(TypeName = "varchar")]
        public string F_IPAddressName { get; set; }
        [MaxLength(50)]
        [Column(TypeName = "varchar")]
        public string F_ModuleId { get; set; }
        [MaxLength(50)]
        [Column(TypeName = "varchar")]
        public string F_ModuleName { get; set; }
        public bool? F_Result { get; set; }
        [MaxLength(500)]
        [Column(TypeName = "varchar")]
        public string F_Description { get; set; }
    }
}
