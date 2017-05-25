using System.Collections.Generic;
using BH.Domain.Entity;
using BH.Application.Dto;

namespace BH.IApplication
{
    public interface IItemsDetailApp
    {
        void DeleteForm(string keyValue);
        ItemsDetailDto GetForm(string keyValue);
        List<ItemsDetailDto> GetItemList(string enCode);
        List<ItemsDetailDto> GetList(string itemId = "", string keyword = "");
        void SubmitForm(ItemsDetailDto itemsDetailInputDto, string keyValue);
    }
}