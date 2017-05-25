using System.Collections.Generic;
using BH.Application.Dto;

namespace BH.IApplication
{
    public interface IItemsApp
    {
        void DeleteForm(string keyValue);
        ItemsDto GetForm(string keyValue);
        List<ItemsDto> GetList();
        void SubmitForm(ItemsDto Sys_Items, string keyValue);
    }
}