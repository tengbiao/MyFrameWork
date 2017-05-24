using System.Collections.Generic;
using BH.Domain.Entity;

namespace BH.IApplication
{
    public interface IModuleApp
    {
        void DeleteForm(string keyValue);
        Sys_Module GetForm(string keyValue);
        List<Sys_Module> GetList();
        void SubmitForm(Sys_Module Sys_Module, string keyValue);
    }
}