using BH.Application.Dto;
using BH.Application.IApplication;
using BH.Code;
using BH.Data;
using BH.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BH.Application.SystemManage
{
    public class AreaApp : IAreaApp
    {
        private IRepository<Sys_Area> _repository;
        public AreaApp(IRepository<Sys_Area> service)
        {
            this._repository = service;
        }

        public List<AreaDto> GetList()
        {
            var result = _repository.IQueryable().MapToList<AreaDto>();
            return result;
        }

        public async Task<AreaDto> GetForm(string keyValue)
        {
            var result = await _repository.FindKeyAsync(keyValue);
            return result.MapTo<AreaDto>();
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

        public void SubmitForm(AreaDto areaEntity, string keyValue)
        {
            var model = areaEntity.MapTo<Sys_Area>();
            if (!string.IsNullOrEmpty(keyValue))
            {
                model.Modify(keyValue);
                _repository.Update(model);
            }
            else
            {
                model.Create();
                _repository.Insert(model);
            }
        }
    }
}
