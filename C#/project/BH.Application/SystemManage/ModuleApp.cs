using BH.Application.Dto;
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
        public List<ModuleDto> GetList()
        {
            return _repository.IQueryable().OrderBy(t => t.F_SortCode).MapToList<ModuleDto>();//.ToList().MapTo<List<ModuleDto>>();
        }
        public ModuleDto GetForm(string keyValue)
        {
            return _repository.FindKey(keyValue).MapTo<ModuleDto>();
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
        public void SubmitForm(ModuleDto moduleDto, string keyValue)
        {
            var module = moduleDto.MapTo<Sys_Module>();
            if (!string.IsNullOrEmpty(keyValue))
            {
                module.Modify(keyValue);
                _repository.Update(module);
            }
            else
            {
                module.Create();
                _repository.Insert(module);
            }
        }
    }
}
