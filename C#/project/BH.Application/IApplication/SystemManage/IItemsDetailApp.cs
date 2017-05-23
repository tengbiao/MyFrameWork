using System.Collections.Generic;
using BH.Domain.Entity.SystemManage;

namespace BH.IApplication
{
    public interface IItemsDetailApp
    {
        void DeleteForm(string keyValue);
        ItemsDetailEntity GetForm(string keyValue);
        List<ItemsDetailEntity> GetItemList(string enCode);
        List<ItemsDetailEntity> GetList(string itemId = "", string keyword = "");
        void SubmitForm(ItemsDetailEntity itemsDetailEntity, string keyValue);
    }
}