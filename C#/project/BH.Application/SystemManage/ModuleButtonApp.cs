using BH.Code;
using BH.Data;
using BH.Domain.Entity.SystemManage;
using BH.IApplication;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BH.Application.SystemManage
{
    public class ModuleButtonApp : IModuleButtonApp
    {
        private readonly IRepository<ModuleButtonEntity> _repository;
        public ModuleButtonApp(IRepository<ModuleButtonEntity> repository)
        {
            _repository = repository;
        }

        public List<ModuleButtonEntity> GetList(string moduleId = "")
        {
            var expression = ExtLinq.True<ModuleButtonEntity>();
            if (!string.IsNullOrEmpty(moduleId))
            {
                expression = expression.And(t => t.F_ModuleId == moduleId);
            }
            return _repository.IQueryable(expression).OrderBy(t => t.F_SortCode).ToList();
        }
        public ModuleButtonEntity GetForm(string keyValue)
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
        public void SubmitForm(ModuleButtonEntity moduleButtonEntity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                moduleButtonEntity.Modify(keyValue);
                _repository.Update(moduleButtonEntity);
            }
            else
            {
                moduleButtonEntity.Create();
                _repository.Insert(moduleButtonEntity);
            }
        }
        public void SubmitCloneButton(string moduleId, string Ids)
        {
            string[] ArrayId = Ids.Split(',');
            var data = this.GetList();
            List<ModuleButtonEntity> entitys = new List<ModuleButtonEntity>();
            foreach (string item in ArrayId)
            {
                ModuleButtonEntity moduleButtonEntity = data.Find(t => t.F_Id == item);
                moduleButtonEntity.F_Id = Common.GuId();
                moduleButtonEntity.F_ModuleId = moduleId;
                moduleButtonEntity.Create();
                entitys.Add(moduleButtonEntity);
            }
            _repository.Insert(entitys);
        }
    }
}
