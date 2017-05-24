/*******************************************************************************
 * Copyright © 2016 BH.Framework 版权所有
 * Author: BH
 * Description: BH快速开发平台
 * Website：http://www.BH.cn
*********************************************************************************/
using BH.Domain.Entity.SystemManage;
using System.Data.Entity.ModelConfiguration;

namespace BH.Mapping.SystemManage
{
    public class RoleMap : EntityTypeConfiguration<Sys_Role>
    {
        public RoleMap()
        {
            this.ToTable("Sys_Role");
            this.HasKey(t => t.F_Id);
        }
    }
}
