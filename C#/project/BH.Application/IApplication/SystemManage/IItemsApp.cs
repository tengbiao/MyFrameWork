using System.Collections.Generic;
using BH.Domain.Entity;

namespace BH.IApplication
{
    public interface IItemsApp
    {
        void DeleteForm(string keyValue);
        Sys_Items GetForm(string keyValue);
        List<Sys_Items> GetList();
        void SubmitForm(Sys_Items Sys_Items, string keyValue);
    }
}