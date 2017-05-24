﻿using BH.Code;
using BH.Data;
using BH.Domain.Entity;
using BH.IApplication;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BH.Application.SystemManage
{
    public class ModuleButtonApp : IModuleButtonApp
    {
        private readonly IRepository<Sys_ModuleButton> _repository;
        public ModuleButtonApp(IRepository<Sys_ModuleButton> repository)
        {
            _repository = repository;
        }

        public List<Sys_ModuleButton> GetList(string moduleId = "")
        {
            var expression = ExtLinq.True<Sys_ModuleButton>();
            if (!string.IsNullOrEmpty(moduleId))
            {
                expression = expression.And(t => t.F_ModuleId == moduleId);
            }
            return _repository.IQueryable(expression).OrderBy(t => t.F_SortCode).ToList();
        }
        public Sys_ModuleButton GetForm(string keyValue)
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
        public void SubmitForm(Sys_ModuleButton Sys_ModuleButton, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                Sys_ModuleButton.Modify(keyValue);
                _repository.Update(Sys_ModuleButton);
            }
            else
            {
                Sys_ModuleButton.Create();
                _repository.Insert(Sys_ModuleButton);
            }
        }
        public void SubmitCloneButton(string moduleId, string Ids)
        {
            string[] ArrayId = Ids.Split(',');
            var data = this.GetList();
            List<Sys_ModuleButton> entitys = new List<Sys_ModuleButton>();
            foreach (string item in ArrayId)
            {
                Sys_ModuleButton Sys_ModuleButton = data.Find(t => t.F_Id == item);
                Sys_ModuleButton.F_Id = Common.GuId();
                Sys_ModuleButton.F_ModuleId = moduleId;
                Sys_ModuleButton.Create();
                entitys.Add(Sys_ModuleButton);
            }
            _repository.Insert(entitys);
        }
    }
}
