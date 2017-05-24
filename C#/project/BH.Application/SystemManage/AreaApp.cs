using BH.Application.IApplication;
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

        public List<Sys_Area> GetList()
        {
            var result = _repository.IQueryable().ToList();
            return result;
        }

        public async Task<Sys_Area> GetForm(string keyValue)
        {
            return await _repository.FindKeyAsync(keyValue);
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

        public void SubmitForm(Sys_Area areaEntity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                areaEntity.Modify(keyValue);
                _repository.Update(areaEntity);
            }
            else
            {
                areaEntity.Create();
                _repository.Insert(areaEntity);
            }
        }
    }
}
