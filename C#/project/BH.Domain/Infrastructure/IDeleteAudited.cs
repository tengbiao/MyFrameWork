/*******************************************************************************
 * Copyright © 2016 BH.Framework 版权所有
 * Author: BH
 * Description: BH快速开发平台
 * Website：http://www.BH.cn
*********************************************************************************/
using System;

namespace BH.Domain.Infrastructure
{
    public interface IDeleteAudited<KeyType> : IEntityBase<KeyType>
    {
        /// <summary>
        /// 逻辑删除标记
        /// </summary>
        bool? F_DeleteMark { set; get; }

        /// <summary>
        /// 删除实体的用户
        /// </summary>
        string F_DeleteUserId { set; get; }

        /// <summary>
        /// 删除实体时间
        /// </summary>
        DateTime? F_DeleteTime { set; get; }
    }

    public interface IDeleteAudited : IDeleteAudited<string>
    {

    }

}