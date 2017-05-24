using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BH.Domain.Infrastructure
{
    public class FullAudited<KeyType> : EntityBase<KeyType>, IFullAudited<KeyType>
    {
        [MaxLength(50)]
        [Column(TypeName = "varchar")]
        public string F_CreatorUserId { get; set; }
        public DateTime? F_CreatorTime { get; set; }
        [MaxLength(50)]
        [Column(TypeName = "varchar")]
        public string F_LastModifyUserId { get; set; }
        public DateTime? F_LastModifyTime { get; set; }
        public bool? F_DeleteMark { get; set; }
        [MaxLength(50)]
        [Column(TypeName = "varchar")]
        public string F_DeleteUserId { get; set; }
        public DateTime? F_DeleteTime { get; set; }
    }

    public class FullAudited : FullAudited<string>
    {

    }
}
