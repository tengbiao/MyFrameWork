using System.Collections.Generic;
using BH.Domain.Entity;

namespace BH.IApplication
{
    public interface IItemsDetailApp
    {
        void DeleteForm(string keyValue);
        Sys_ItemsDetail GetForm(string keyValue);
        List<Sys_ItemsDetail> GetItemList(string enCode);
        List<Sys_ItemsDetail> GetList(string itemId = "", string keyword = "");
        void SubmitForm(Sys_ItemsDetail Sys_ItemsDetail, string keyValue);
    }
}