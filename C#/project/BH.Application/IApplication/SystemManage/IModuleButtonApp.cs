using System.Collections.Generic;
using BH.Domain.Entity;

namespace BH.IApplication
{
    public interface IModuleButtonApp
    {
        void DeleteForm(string keyValue);
        Sys_ModuleButton GetForm(string keyValue);
        List<Sys_ModuleButton> GetList(string moduleId = "");
        void SubmitCloneButton(string moduleId, string Ids);
        void SubmitForm(Sys_ModuleButton Sys_ModuleButton, string keyValue);
    }
}