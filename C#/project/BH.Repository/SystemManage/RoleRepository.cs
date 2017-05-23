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
using System.Data.Entity;
using System.Linq;

namespace BH.Repository.SystemManage
{
    public class RoleRepository : Repository<RoleEntity>, IRoleRepository
    {
        public void DeleteForm(string keyValue)
        {
            //using (var db = new RepositoryBase().BeginTrans())
            //{
            //    db.Delete<RoleEntity>(t => t.F_Id == keyValue);
            //    db.Delete<RoleAuthorizeEntity>(t => t.F_ObjectId == keyValue);
            //    db.Commit();
            //}           
        }
        public void SubmitForm(RoleEntity roleEntity, List<RoleAuthorizeEntity> roleAuthorizeEntitys, string keyValue)
        {
            //using (var db = new RepositoryBase().BeginTrans())
            //{
            //    if (!string.IsNullOrEmpty(keyValue))
            //    {
            //        db.Update(roleEntity);
            //    }
            //    else
            //    {
            //        roleEntity.F_Category = 1;
            //        db.Insert(roleEntity);
            //    }
            //    db.Delete<RoleAuthorizeEntity>(t => t.F_ObjectId == roleEntity.F_Id);
            //    db.Insert(roleAuthorizeEntitys);
            //    db.Commit();
            //}
        }
    }
}
