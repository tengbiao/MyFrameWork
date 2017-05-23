using System.Collections.Generic;
using BH.Domain.Entity.SystemManage;

namespace BH.IApplication
{
    public interface IItemsApp
    {
        void DeleteForm(string keyValue);
        ItemsEntity GetForm(string keyValue);
        List<ItemsEntity> GetList();
        void SubmitForm(ItemsEntity itemsEntity, string keyValue);
    }
}