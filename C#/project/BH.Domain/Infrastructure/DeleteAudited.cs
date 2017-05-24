/*******************************************************************************
 * Copyright © 2016 BH.Framework 版权所有
 * Author: BH
 * Description: BH快速开发平台
 * Website：http://www.BH.cn
*********************************************************************************/
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BH.Domain.Infrastructure
{
    public class DeleteAudited<KeyType> : EntityBase<KeyType>, IDeleteAudited<KeyType>
    {
        /// <summary>
        /// 逻辑删除标记
        /// </summary>
        public bool? F_DeleteMark { get; set; }

        /// <summary>
        /// 删除实体的用户
        /// </summary>
        [MaxLength(50)]
        [Column(TypeName = "varchar")]
        public string F_DeleteUserId { get; set; }

        /// <summary>
        /// 删除实体时间
        /// </summary>
        public DateTime? F_DeleteTime { get; set; }
    }

    public class DeleteAudited : DeleteAudited<string>
    {

    }
}