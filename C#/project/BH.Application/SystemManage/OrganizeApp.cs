/*******************************************************************************
 * Copyright © 2016 BH.Framework 版权所有
 * Author: BH
 * Description: BH快速开发平台
 * Website：http://www.BH.cn
*********************************************************************************/
using BH.Data;
using BH.Domain.Entity.SystemManage;
using BH.IApplication;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BH.Application.SystemManage
{
    public class OrganizeApp : IOrganizeApp
    {
        private readonly IRepository<OrganizeEntity> _repository;
        public OrganizeApp(IRepository<OrganizeEntity> repository)
        {
            _repository = repository;
        }

        public List<OrganizeEntity> GetList()
        {
            return _repository.IQueryable().OrderBy(t => t.F_CreatorTime).ToList();
        }
        public OrganizeEntity GetForm(string keyValue)
        {
            return _repository.FindKey(keyValue);
        }
        public void DeleteForm(string keyValue)
        {
            if (_repository.IQueryable().Count(t => t.F_ParentId.Equals(keyValue)) > 0)
            {
                throw new Exception("删除失败！操作的对象包含了下级数据。");
            }
            else
            {
                _repository.Delete(t => t.F_Id == keyValue);
            }
        }
        public void SubmitForm(OrganizeEntity organizeEntity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                organizeEntity.Modify(keyValue);
                _repository.Update(organizeEntity);
            }
            else
            {
                organizeEntity.Create();
                _repository.Insert(organizeEntity);
            }
        }
    }
}
