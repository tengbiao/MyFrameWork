using BH.Code;
using BH.Data;
using BH.Domain.Entity.SystemManage;
using BH.IApplication;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BH.Application.SystemManage
{
    public class ModuleApp : IModuleApp
    {
        private readonly IRepository<ModuleEntity> _repository;
        public ModuleApp(IRepository<ModuleEntity> repository)
        {
            _repository = repository;
        }
        public List<ModuleEntity> GetList()
        {
            return _repository.IQueryable().OrderBy(t => t.F_SortCode).ToList();
        }
        public ModuleEntity GetForm(string keyValue)
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
        public void SubmitForm(ModuleEntity moduleEntity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                moduleEntity.Modify(keyValue);
                _repository.Update(moduleEntity);
            }
            else
            {
                moduleEntity.Create();
                _repository.Insert(moduleEntity);
            }
        }
    }
}
