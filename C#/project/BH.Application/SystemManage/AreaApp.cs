using BH.Application.IApplication;
using BH.Data;
using BH.Domain.Entity.SystemManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BH.Application.SystemManage
{
    public class AreaApp : IAreaApp
    {
        private IRepository<AreaEntity> service;
        public AreaApp(IRepository<AreaEntity> service)
        {
            this.service = service;
        }

        public List<AreaEntity> GetList()
        {
            var result = service.IQueryable().ToList();
            return result;
        }

        public async Task<AreaEntity> GetForm(string keyValue)
        {
            return await service.FindKeyAsync(keyValue);
        }

        public void DeleteForm(string keyValue)
        {
            if (service.IQueryable().Count(t => t.F_ParentId.Equals(keyValue)) > 0)
            {
                throw new Exception("删除失败！操作的对象包含了下级数据。");
            }
            else
            {
                service.Delete(t => t.F_Id == keyValue);
            }
        }

        public void SubmitForm(AreaEntity areaEntity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                areaEntity.Modify(keyValue);
                service.Update(areaEntity);
            }
            else
            {
                areaEntity.Create();
                service.Insert(areaEntity);
            }
        }
    }
}
