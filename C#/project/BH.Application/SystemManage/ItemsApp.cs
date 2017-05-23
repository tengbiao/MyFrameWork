using BH.Data;
using BH.Domain.Entity.SystemManage;
using BH.IApplication;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BH.Application.SystemManage
{
    public class ItemsApp : IItemsApp
    {
        private readonly IRepository<ItemsEntity> _repository;
        public ItemsApp(IRepository<ItemsEntity> repository)
        {
            _repository = repository;
        }
        public List<ItemsEntity> GetList()
        {
            return _repository.IQueryable().ToList();
        }
        public ItemsEntity GetForm(string keyValue)
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
        public void SubmitForm(ItemsEntity itemsEntity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                itemsEntity.Modify(keyValue);
                _repository.Update(itemsEntity);
            }
            else
            {
                itemsEntity.Create();
                _repository.Insert(itemsEntity);
            }
        }
    }
}
