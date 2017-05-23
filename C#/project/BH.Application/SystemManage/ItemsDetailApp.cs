using BH.Code;
using BH.Data;
using BH.Domain.Entity.SystemManage;
using BH.IApplication;
using System.Collections.Generic;
using System.Linq;

namespace BH.Application.SystemManage
{
    public class ItemsDetailApp : IItemsDetailApp
    {
        private readonly IRepository<ItemsDetailEntity> _repository;
        private readonly IRepository<ItemsEntity> _itemRepository;
        public ItemsDetailApp(IRepository<ItemsDetailEntity> repository, IRepository<ItemsEntity> itemRepository)
        {
            _repository = repository;
            _itemRepository = itemRepository;
        }

        public List<ItemsDetailEntity> GetList(string itemId = "", string keyword = "")
        {
            var expression = ExtLinq.True<ItemsDetailEntity>();
            if (!string.IsNullOrEmpty(itemId))
            {
                expression = expression.And(t => t.F_ItemId == itemId);
            }
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.F_ItemName.Contains(keyword));
                expression = expression.Or(t => t.F_ItemCode.Contains(keyword));
            }
            return _repository.IQueryable(expression).OrderBy(t => t.F_SortCode).ToList();
        }
        public List<ItemsDetailEntity> GetItemList(string enCode)
        {
            var query = (from detail in _repository.IQueryable()
                         join item in _itemRepository.IQueryable()
                         on detail.F_ItemId equals item.F_Id
                         where item.F_EnCode == enCode && detail.F_EnabledMark.Value && !detail.F_DeleteMark.Value
                         orderby detail.F_SortCode ascending
                         select detail
            );
            return query.ToList();
        }
        public ItemsDetailEntity GetForm(string keyValue)
        {
            return _repository.FindKey(keyValue);
        }
        public void DeleteForm(string keyValue)
        {
            _repository.Delete(t => t.F_Id == keyValue);
        }
        public void SubmitForm(ItemsDetailEntity itemsDetailEntity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                itemsDetailEntity.Modify(keyValue);
                _repository.Update(itemsDetailEntity);
            }
            else
            {
                itemsDetailEntity.Create();
                _repository.Insert(itemsDetailEntity);
            }
        }
    }
}
