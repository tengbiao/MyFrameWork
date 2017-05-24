using BH.Domain.Infrastructure;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BH.Domain.Entity
{
    public class Sys_User : FullAudited
    {
        [MaxLength(50)]
        [Column(TypeName = "varchar")]
        public string F_Account { get; set; }
        [MaxLength(50)]
        [Column(TypeName = "varchar")]
        public string F_RealName { get; set; }
        [MaxLength(50)]
        [Column(TypeName = "varchar")]
        public string F_NickName { get; set; }
        [MaxLength(500)]
        [Column(TypeName = "varchar")]
        public string F_HeadIcon { get; set; }
        public bool? F_Gender { get; set; }
        public DateTime? F_Birthday { get; set; }
        [MaxLength(20)]
        [Column(TypeName = "varchar")]
        public string F_MobilePhone { get; set; }
        [MaxLength(50)]
        [Column(TypeName = "varchar")]
        public string F_Email { get; set; }
        [MaxLength(50)]
        [Column(TypeName = "varchar")]
        public string F_WeChat { get; set; }
        [MaxLength(50)]
        [Column(TypeName = "varchar")]
        public string F_ManagerId { get; set; }
        public int? F_SecurityLevel { get; set; }
        [MaxLength(500)]
        [Column(TypeName = "varchar")]
        public string F_Signature { get; set; }
        [MaxLength(50)]
        [Column(TypeName = "varchar")]
        public string F_OrganizeId { get; set; }
        [MaxLength(500)]
        [Column(TypeName = "varchar")]
        public string F_DepartmentId { get; set; }
        [MaxLength(500)]
        [Column(TypeName = "varchar")]
        public string F_RoleId { get; set; }
        [MaxLength(500)]
        [Column(TypeName = "varchar")]
        public string F_DutyId { get; set; }
        public bool? F_IsAdministrator { get; set; }
        public int? F_SortCode { get; set; }
        public bool? F_EnabledMark { get; set; }
        [MaxLength(500)]
        [Column(TypeName = "varchar")]
        public string F_Description { get; set; }
    }
}
