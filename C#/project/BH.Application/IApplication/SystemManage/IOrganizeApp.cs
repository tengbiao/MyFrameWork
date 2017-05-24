using System.Collections.Generic;
using BH.Domain.Entity;

namespace BH.IApplication
{
    public interface IOrganizeApp
    {
        void DeleteForm(string keyValue);
        Sys_Organize GetForm(string keyValue);
        List<Sys_Organize> GetList();
        void SubmitForm(Sys_Organize Sys_Organize, string keyValue);
    }
}