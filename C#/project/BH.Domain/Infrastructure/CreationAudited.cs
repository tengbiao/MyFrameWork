/*******************************************************************************
 * Copyright © 2016 BH.Framework 版权所有
 * Author: BH
 * Description: BH快速开发平台
 * Website：http://www.BH.cn
*********************************************************************************/
using System;

namespace BH.Domain.Infrastructure
{
    public class CreationAudited<KeyType> : EntityBase<KeyType>, ICreationAudited<KeyType>
    {
        public string F_CreatorUserId { get; set; }
        public DateTime? F_CreatorTime { get; set; }
    }

    public class CreationAudited : CreationAudited<string>
    {

    }
}