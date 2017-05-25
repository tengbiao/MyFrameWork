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
    public class ItemsApp : IItemsApp
    {
        private readonly IRepository<Sys_Items> _repository;
        public ItemsApp(IRepository<Sys_Items> repository)
        {
            _repository = repository;
        }
        public List<ItemsDto> GetList()
        {
            return _repository.IQueryable().MapToList<ItemsDto>();
        }
        public ItemsDto GetForm(string keyValue)
        {
            return _repository.FindKey(keyValue).MapTo<ItemsDto>();
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
        public void SubmitForm(ItemsDto itemsInputDto, string keyValue)
        {
            var model = itemsInputDto.MapTo<Sys_Items>();
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
