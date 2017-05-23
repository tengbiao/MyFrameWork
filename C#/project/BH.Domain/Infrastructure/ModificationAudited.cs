/*******************************************************************************
 * Copyright © 2016 BH.Framework 版权所有
 * Author: BH
 * Description: BH快速开发平台
 * Website：http://www.BH.cn
*********************************************************************************/
using System;

namespace BH.Domain.Infrastructure
{
    public class ModificationAudited<KeyType> : EntityBase<KeyType>, IModificationAudited<KeyType>
    {
        public string F_LastModifyUserId { get; set; }
        public DateTime? F_LastModifyTime { get; set; }
    }

    public class ModificationAudited : ModificationAudited<string>
    {

    }
}