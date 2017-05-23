/*******************************************************************************
 * Copyright © 2016 BH.Framework 版权所有
 * Author: BH
 * Description: BH快速开发平台
 * Website：http://www.BH.cn
*********************************************************************************/
using System;

namespace BH.Domain.Infrastructure
{

    public interface ICreationAudited<KeyType> : IEntityBase<KeyType>
    {
        string F_CreatorUserId { get; set; }
        DateTime? F_CreatorTime { get; set; }
    }

    public interface ICreationAudited : ICreationAudited<string>
    {
    }
}