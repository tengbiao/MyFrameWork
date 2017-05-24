using BH.Data;
using BH.Domain.Entity;
using BH.IApplication;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BH.Application.SystemManage
{
    public class ItemsApp : IItemsApp
    {
        private readonly IRepository<Sys_Items> _repository;
        public ItemsApp(IRepository<Sys_Items> repository)
        {
            _repository = repository;
        }
        public List<Sys_Items> GetList()
        {
            return _repository.IQueryable().ToList();
        }
        public Sys_Items GetForm(string keyValue)
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
        public void SubmitForm(Sys_Items Sys_Items, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                Sys_Items.Modify(keyValue);
                _repository.Update(Sys_Items);
            }
            else
            {
                Sys_Items.Create();
                _repository.Insert(Sys_Items);
            }
        }
    }
}
