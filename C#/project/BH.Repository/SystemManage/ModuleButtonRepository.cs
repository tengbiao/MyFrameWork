/*******************************************************************************
 * Copyright © 2016 BH.Framework 版权所有
 * Author: BH
 * Description: BH快速开发平台
 * Website：http://www.BH.cn
*********************************************************************************/
using BH.Data;
using BH.Domain.Entity.SystemManage;
using BH.Repository.IRepository.SystemManage;
using BH.Repository.SystemManage;
using System.Collections.Generic;

namespace BH.Repository.SystemManage
{
    public class ModuleButtonRepository : Repository<ModuleButtonEntity>, IModuleButtonRepository
    {
        public void SubmitCloneButton(List<ModuleButtonEntity> entitys)
        {
            Insert(entitys);

            //using (var db = new RepositoryBase().BeginTrans())
            //{
            //    foreach (var item in entitys)
            //    {
            //        db.Insert(item);
            //    }
            //    db.Commit();
            //}
        }
    }
}
