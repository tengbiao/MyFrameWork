/*******************************************************************************
 * Copyright © 2016 BH.Framework 版权所有
 * Author: BH
 * Description: BH快速开发平台
 * Website：http://www.BH.cn
*********************************************************************************/
using System;

namespace BH.Domain.Infrastructure
{
    public interface IModificationAudited<KeyType> : IEntityBase<KeyType>
    {
        string F_LastModifyUserId { get; set; }
        DateTime? F_LastModifyTime { get; set; }
    }

    public interface IModificationAudited : IModificationAudited<string>
    {

    }
}