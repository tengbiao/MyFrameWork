using BH.Application.Dto;
using BH.Code;
using BH.Data;
using BH.Domain.Entity;
using BH.IApplication;
using System.Collections.Generic;
using System.Linq;

namespace BH.Application.SystemManage
{
    public class ItemsDetailApp : IItemsDetailApp
    {
        private readonly IRepository<Sys_ItemsDetail> _repository;
        private readonly IRepository<Sys_Items> _itemRepository;
        public ItemsDetailApp(IRepository<Sys_ItemsDetail> repository, IRepository<Sys_Items> itemRepository)
        {
            _repository = repository;
            _itemRepository = itemRepository;
        }

        public List<ItemsDetailDto> GetList(string itemId = "", string keyword = "")
        {
            var expression = ExtLinq.True<Sys_ItemsDetail>();
            if (!string.IsNullOrEmpty(itemId))
            {
                expression = expression.And(t => t.F_ItemId == itemId);
            }
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.F_ItemName.Contains(keyword));
                expression = expression.Or(t => t.F_ItemCode.Contains(keyword));
            }
            return _repository.IQueryable(expression).OrderBy(t => t.F_SortCode).MapToList<ItemsDetailDto>();
        }
        public List<ItemsDetailDto> GetItemList(string enCode)
        {
            var query = (from detail in _repository.IQueryable()
                         join item in _itemRepository.IQueryable()
                         on detail.F_ItemId equals item.F_Id
                         where item.F_EnCode == enCode && detail.F_EnabledMark.Value && !detail.F_DeleteMark.Value
                         orderby detail.F_SortCode ascending
                         select detail
            );
            return query.MapToList<ItemsDetailDto>();
        }
        public ItemsDetailDto GetForm(string keyValue)
        {
            return _repository.FindKey(keyValue).MapTo<ItemsDetailDto>();
        }
        public void DeleteForm(string keyValue)
        {
            _repository.Delete(t => t.F_Id == keyValue);
        }
        public void SubmitForm(ItemsDetailDto itemsDetailInputDto, string keyValue)
        {
            var model = itemsDetailInputDto.MapTo<Sys_ItemsDetail>();
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
