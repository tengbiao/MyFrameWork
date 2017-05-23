/*******************************************************************************
 * Copyright © 2016 BH.Framework 版权所有
 * Author: BH * Description: BH快速开发平台
 * Website：http://www.BH.cn
*********************************************************************************/
using BH.Domain.Entity.SystemSecurity;
using System.Data.Entity.ModelConfiguration;

namespace BH.Mapping.SystemSecurity
{
    public class FilterIPMap : EntityTypeConfiguration<FilterIPEntity>
    {
        public FilterIPMap()
        {
            this.ToTable("Sys_FilterIP");
            this.HasKey(t => t.F_Id);
        }
    }
}
