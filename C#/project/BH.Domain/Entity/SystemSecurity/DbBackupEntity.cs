/*******************************************************************************
 * Copyright © 2016 BH.Framework 版权所有
 * Author: BH
 * Description: BH快速开发平台
 * Website：http://www.BH.cn
*********************************************************************************/
using BH.Domain.Infrastructure;
using System;

namespace BH.Domain.Entity.SystemSecurity
{
    public class FilterIPEntity : FullAudited
    {
        public bool? F_Type { get; set; }
        public string F_StartIP { get; set; }
        public string F_EndIP { get; set; }
        public int? F_SortCode { get; set; }
        public bool? F_EnabledMark { get; set; }
        public string F_Description { get; set; }
    }
}
