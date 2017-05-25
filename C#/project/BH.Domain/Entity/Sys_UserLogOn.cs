using BH.Domain.Infrastructure;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BH.Domain.Entity
{
    public class Sys_UserLogOn : EntityBase
    {      
        [Key,ForeignKey("Sys_User")]
        [MaxLength(50)]
        [Column(TypeName = "varchar")]
        public new string F_Id { set; get; }
        [MaxLength(50)]
        [Column(TypeName = "varchar")]
        public string F_UserId { get; set; }
        [MaxLength(50)]
        [Column(TypeName = "varchar")]
        public string F_UserPassword { get; set; }
        [MaxLength(50)]
        [Column(TypeName = "varchar")]
        public string F_UserSecretkey { get; set; }
        public DateTime? F_AllowStartTime { get; set; }
        public DateTime? F_AllowEndTime { get; set; }
        public DateTime? F_LockStartDate { get; set; }
        public DateTime? F_LockEndDate { get; set; }
        public DateTime? F_FirstVisitTime { get; set; }
        public DateTime? F_PreviousVisitTime { get; set; }
        public DateTime? F_LastVisitTime { get; set; }
        public DateTime? F_ChangePasswordDate { get; set; }
        public bool? F_MultiUserLogin { get; set; }
        public int? F_LogOnCount { get; set; }
        public bool? F_UserOnLine { get; set; }
        [MaxLength(50)]
        [Column(TypeName = "varchar")]
        public string F_Question { get; set; }
        [MaxLength(50)]
        [Column(TypeName = "varchar")]
        public string F_AnswerQuestion { get; set; }
        public bool? F_CheckIPAddress { get; set; }
        [MaxLength(50)]
        [Column(TypeName = "varchar")]
        public string F_Language { get; set; }
        [MaxLength(50)]
        [Column(TypeName = "varchar")]
        public string F_Theme { get; set; }
       
        public Sys_User Sys_User { get; set; }
    }
}
