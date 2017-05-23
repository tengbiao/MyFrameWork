using System.Collections.Generic;
using BH.Domain.Entity.SystemManage;

namespace BH.IApplication
{
    public interface IModuleButtonApp
    {
        void DeleteForm(string keyValue);
        ModuleButtonEntity GetForm(string keyValue);
        List<ModuleButtonEntity> GetList(string moduleId = "");
        void SubmitCloneButton(string moduleId, string Ids);
        void SubmitForm(ModuleButtonEntity moduleButtonEntity, string keyValue);
    }
}