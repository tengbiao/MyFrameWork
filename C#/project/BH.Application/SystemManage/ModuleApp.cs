using BH.Code;
using BH.Data;
using BH.Domain.Entity;
using BH.IApplication;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BH.Application.SystemManage
{
    public class ModuleApp : IModuleApp
    {
        private readonly IRepository<Sys_Module> _repository;
        public ModuleApp(IRepository<Sys_Module> repository)
        {
            _repository = repository;
        }
        public List<Sys_Module> GetList()
        {
            return _repository.IQueryable().OrderBy(t => t.F_SortCode).ToList();
        }
        public Sys_Module GetForm(string keyValue)
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
        public void SubmitForm(Sys_Module Sys_Module, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                Sys_Module.Modify(keyValue);
                _repository.Update(Sys_Module);
            }
            else
            {
                Sys_Module.Create();
                _repository.Insert(Sys_Module);
            }
        }
    }
}
