/*******************************************************************************
 * Copyright © 2016 BH.Framework 版权所有
 * Author: BH
 * Description: BH快速开发平台
 * Website：http://www.BH.cn
*********************************************************************************/
using BH.Data;
using BH.Domain.Entity;
using BH.IApplication;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BH.Application.SystemManage
{
    public class OrganizeApp : IOrganizeApp
    {
        private readonly IRepository<Sys_Organize> _repository;
        public OrganizeApp(IRepository<Sys_Organize> repository)
        {
            _repository = repository;
        }

        public List<Sys_Organize> GetList()
        {
            return _repository.IQueryable().OrderBy(t => t.F_CreatorTime).ToList();
        }
        public Sys_Organize GetForm(string keyValue)
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
        public void SubmitForm(Sys_Organize Sys_Organize, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                Sys_Organize.Modify(keyValue);
                _repository.Update(Sys_Organize);
            }
            else
            {
                Sys_Organize.Create();
                _repository.Insert(Sys_Organize);
            }
        }
    }
}
